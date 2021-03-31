using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

// This class manages the majority of the game functionality
public class DiggerLevelManager : LevelManager
{
    public TMP_Text digkeyText;     // text that contain instructions for digKey bind 

    public PlayerController player; // the player object in Unity
    public ChestAnimator chest;     // the chest object in Unity
    public Transform hole;          // reveals hole dug, and is bottom of the hole the player stands on
    public Transform holeTop;       // coordinates of the top of the hole to be dug
    public Transform holeBot;       // coordinates of the bottom of the hole to be dug

    int digAmount;                  // total amount of presses required; must be > 0, rounds up to nearest 10
    KeyCode digKey;                 // keyboard key used to dig

    ButtonPressingMetric bpMetric;  // records button pressing data during the game
    MetricJSONWriter metricWriter;  // outputs recording metric (bpMetric) as a json file
    
    List<KeyCode> keysDown;         // List of keys currently held down (not full history)
    int dugCount;                   // dug/presses counter
    float digDepth;                 // depth dug for each dig/press

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        InitConfigurable(); // initialize configurable values

        // set the digKey for the intro instructions
        int tempIdx = digkeyText.text.IndexOf("KEY");
        digkeyText.text = digkeyText.text.Substring(0, tempIdx) + KeyCodeDict.toString[digKey] + digkeyText.text.Substring(tempIdx+3);

        countDoneText = "Dig!";

        bpMetric = new ButtonPressingMetric(); // initialize metric recorder

        keysDown = new List<KeyCode>();
        dugCount = 0;
        digDepth = (holeTop.position.y - holeBot.position.y) / digAmount;
    }

    // Initialize values using config file, or default values if config values not specified
    void InitConfigurable()
    {
        DiggerConfig diggerConfig = new DiggerConfig();
     
        // if running the game from the battery, override `diggerConfig` with the config class from Battery
        DiggerConfig tempConfig = (DiggerConfig)Battery.Instance.GetCurrentConfig();
        if (tempConfig!=null) {
            diggerConfig = tempConfig;
        } else {
            Debug.Log("Battery not found, using default values");
        }

        // use battery's config values, or default values if running game by itself
        digAmount = diggerConfig.DigAmount > 0 ? diggerConfig.DigAmount : Default(100, "DigAmount");
        maxGameTime = diggerConfig.MaxGameTime > 0 ? diggerConfig.MaxGameTime : Default(100f, "MaxGameTime");;
        try { // use default dig key if we cannot parse it from the config
            digKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), diggerConfig.DigKey);
            if (!KeyCodeDict.toString.ContainsKey(digKey)) throw new Exception();
        } catch (Exception) {
            digKey = Default(KeyCode.B, "DigKey");
        }

        // udpate battery config with actual/final values being used
        diggerConfig.MaxGameTime = maxGameTime;
        diggerConfig.DigAmount = digAmount;
        diggerConfig.DigKey = digKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
            // begin game, begin recording 
            if (!bpMetric.isRecording) StartGame();

            // the player landing on chest triggers the end of the game
            if (chest.opened || Time.time-gameStartTime>=maxGameTime) EndGame();
        }
    }

    // Begin the actual game, start recording metrics
    public void StartGame()
    {
        bpMetric.startRecording();
        metricWriter = new MetricJSONWriter("Digger", DateTime.Now); // initialize metric data writer
        gameStartTime = Time.time;
    
    }

    // End game, finish recording metrics
    public void EndGame()
    {
        bpMetric.finishRecording();
        var str = metricWriter.GetLogMetrics( 
                    DateTime.Now, 
                    new List<AbstractMetric>(){bpMetric}
                );
        StartCoroutine(Post("digger_"+DateTime.Now.ToFileTime()+".json", str));
        EndLevel(4f);
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        // start level when user presses `digKey`
        if (lvlState==0 && e.keyCode==digKey) ShowInstruction(++instructionCount);

        // record every key press and key release, regardless if it's `digKey`
        if (lvlState==2 && e.keyCode!=KeyCode.None) KeyEvent(e);

        // advance to the next scene when the game is over and the user presses a key
        if (lvlState==4 && e.type==EventType.KeyUp) Battery.Instance.LoadNextScene();
    }

    // Record key presses during the actual game.
    // Call player to dig when the dig key is pressed.
    void KeyEvent(Event e) {
        // When a keyboard key is initially pressed down, add it to list
        // We don't want to record when a key is HELD down
        if (e.type == EventType.KeyDown && !keysDown.Contains(e.keyCode)) {
            keysDown.Add(e.keyCode);
            bpMetric.recordEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, true));
            if (e.keyCode==digKey) {
                player.DigDown();
                DigGround();
            }
        // Remove key from list
        } else if (e.type == EventType.KeyUp) {
            keysDown.Remove(e.keyCode);
            bpMetric.recordEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, false));
            if (e.keyCode==digKey) player.DigUp();
        }
    }

    void DigGround()
    {
        if (++dugCount<digAmount) {
            hole.Translate(Vector3.down*digDepth);
        } else if (dugCount==digAmount) {
            hole.position = Vector3.down*8.62f;
        }
        
    }
}
