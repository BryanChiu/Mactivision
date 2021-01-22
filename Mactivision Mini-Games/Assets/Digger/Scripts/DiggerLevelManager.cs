using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This class manages the majority of the game functionality
public class DiggerLevelManager : LevelManager
{
    public PlayerController player; // the player object in Unity
    public ChestAnimator chest;     // the chest object in Unity
    
    public int digAmount;           // total amount of presses required; must be > 0, rounds up to nearest 10
    KeyCode digKey;                 // keyboard key used to dig

    List<KeyCode> keysDown;         // List of keys currently held down (not full history)
    ButtonPressingMetric bpMetric;  // records button pressing data during the game
    MetricJSONWriter metricWriter;  // outputs recording metric (bpMetric) as a json file

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        // set default values
        digKey = KeyCode.B;
        digAmount = 100;
        InitConfig(); // change values according to config

        // set the digKey for the intro instructions
        int tempIdx = introText.text.IndexOf("KEY");
        introText.text = introText.text.Substring(0, tempIdx) + digKey.ToString() + introText.text.Substring(tempIdx+3);

        countDoneText = "Dig!";
        keysDown = new List<KeyCode>();

        bpMetric = new ButtonPressingMetric(); // initialize metric recorder
        metricWriter = new MetricJSONWriter("Digger", DateTime.Now); // initialize metric data writer
    }

    void InitConfig()
    {
        try {
            DiggerConfig diggerConfig = (DiggerConfig)Battery.Instance.GetCurrentConfig();
            if (diggerConfig.DigAmount != 0) digAmount = diggerConfig.DigAmount;
            if (diggerConfig.DigKey != null) digKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), diggerConfig.DigKey);
        } catch (Exception) {
            Debug.Log("Battery not found, using default values");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
            // begin game, begin recording 
            if (!bpMetric.isRecording) {
                bpMetric.startRecording();
                SetDigKeyForGround();
                SetDigAmountForGround();
            }
            // the player landing on chest triggers the end of the game
            if (chest.opened) {
                bpMetric.finishRecording();
                metricWriter.logMetrics(
                    "Logs/digger_"+DateTime.Now.ToFileTime()+".json", 
                    DateTime.Now, 
                    new List<AbstractMetric>(){bpMetric}
                );
                EndLevel(5f);
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        // start level when user pressing `digKey`
        if (lvlState==0 && e.isKey && e.keyCode==digKey) {
            StartLevel();
        }
        // record every key press and key release, regardless if it's `digKey`
        if (lvlState==2 && e.isKey && e.keyCode!=KeyCode.None) {
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

        if (lvlState==4 && e.type == EventType.KeyUp) {
            Battery.Instance.LoadNextScene();
        }
    }

    // For all blocks to be dug, set the key that will break it
    void SetDigKeyForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetDigKey(digKey);
        }
    }

    // For all blocks to be dug, set the amount of "digs"/key-presses it will take to completely break.
    // Since there are 10 blocks to be dug, the dig amount for each block is a tenth of the `digAmount`,
    // rounded up. Will be at least 1, as `digAmount` must be > 0
    void SetDigAmountForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetHitsToBreak(Mathf.CeilToInt(digAmount/10f));
        }
    }
}
