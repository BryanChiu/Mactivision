using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This class manages the majority of the game functionality
public class DiggerLevelManager : LevelManager
{
    public PlayerController player; // the player object in Unity
    public ChestAnimator chest;     // the chest object in Unity

    float maxGameTime;       // maximum length of the game
    float gameStartTime;
    
    int digAmount;                  // total amount of presses required; must be > 0, rounds up to nearest 10
    KeyCode digKey;                 // keyboard key used to dig

    List<KeyCode> keysDown;         // List of keys currently held down (not full history)
    ButtonPressingMetric bpMetric;  // records button pressing data during the game
    MetricJSONWriter metricWriter;  // outputs recording metric (bpMetric) as a json file

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        InitConfigurable(); // initialize configurable values

        // set the digKey for the intro instructions
        int tempIdx = introText.text.IndexOf("KEY");
        introText.text = introText.text.Substring(0, tempIdx) + KeyCodeDict.toString[digKey] + introText.text.Substring(tempIdx+3);

        countDoneText = "Dig!";
        keysDown = new List<KeyCode>();

        bpMetric = new ButtonPressingMetric(); // initialize metric recorder
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
        digAmount = diggerConfig.DigAmount > 0 ? Mathf.CeilToInt(diggerConfig.DigAmount/10f)*10 : 100;
        maxGameTime = diggerConfig.MaxGameTime > 0 ? diggerConfig.MaxGameTime : digAmount;
        try { // use default dig key if we cannot parse it from the config
            digKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), diggerConfig.DigKey);
            if (!KeyCodeDict.toString.ContainsKey(digKey)) throw new Exception();
        } catch (Exception) {
            Debug.Log("Invalid KeyCode, using default value");
            digKey = KeyCode.B;
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
    void StartGame()
    {
        bpMetric.startRecording();
        metricWriter = new MetricJSONWriter("Digger", DateTime.Now); // initialize metric data writer
        SetDigAmountForGround();
        gameStartTime = Time.time;
    }

    // End game, finish recording metrics
    void EndGame()
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
        if (lvlState==0 && e.keyCode==digKey) StartLevel();

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
            if (e.keyCode==digKey) player.DigDown();
        // Remove key from list
        } else if (e.type == EventType.KeyUp) {
            keysDown.Remove(e.keyCode);
            bpMetric.recordEvent(new ButtonPressingEvent(DateTime.Now, e.keyCode, false));
            if (e.keyCode==digKey) player.DigUp();
        }
    }

    // For all blocks to be dug, set the amount of "digs"/key-presses it will take to completely break.
    // Since there are 10 blocks to be dug, the dig amount for each block is a tenth of the `digAmount`,
    // rounded up. Will be at least 1, as `digAmount` must be > 0
    void SetDigAmountForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().hitsToBreak = digAmount/10;
        }
    }
}
