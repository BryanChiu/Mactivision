using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiggerLevelManager : LevelManager
{
    public PlayerController player; // the player object in Unity
    public ChestAnimator chest; // the chest object in Unity
    
    KeyCode digKey; // keyboard key used to dig
    int digAmount; // total amount of presses required; must be > 0, rounds up to nearest 10

    List<KeyCode> keysDown; // List of keys currently held down (not full history)

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        countDoneText = "Dig!";
        digKey = KeyCode.B;
       
        // Example of getting configuration data.
        DiggerConfig diggerConfig = (DiggerConfig)Battery.Instance.GetCurrentConfig();
        digAmount = diggerConfig.DigAmount;
        
        keysDown = new List<KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
            if (!recording) { // begin recording 
                recording = true;
                recorder.StartRec();
                SetDigKeyForGround(); // not called at Start() because this script could load before the blocks
                SetDigAmountForGround();
            }
            if (chest.opened) { // the player landing on chest triggers the end of the game
                recorder.EndRec();
                EndLevel(5f);
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        if (lvlState==0 && e.isKey && e.keyCode==digKey) {
            StartLevel();
        }
        if (lvlState==2 && e.isKey && e.keyCode!=KeyCode.None) {
            // When a keyboard key is initially pressed down, add it to list
            // We don't want to record when a key is HELD down
            if (e.type == EventType.KeyDown && !keysDown.Contains(e.keyCode)) {
                keysDown.Add(e.keyCode);
                recorder.AddEvent(e.keyCode, true);
                if (e.keyCode==digKey) player.DigDown();
            // Remove key from list
            } else if (e.type == EventType.KeyUp) {
                keysDown.Remove(e.keyCode);
                recorder.AddEvent(e.keyCode, false);
                if (e.keyCode==digKey) player.DigUp();
            }
        }
    }

    // for all blocks to be dug, set the key that will break it
    void SetDigKeyForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetDigKey(digKey);
        }
    }

    // for all blocks to be dug, set the amount of "digs"/key-presses it will take to completely break
    void SetDigAmountForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetHitsToBreak(Mathf.CeilToInt(digAmount/10f));
        }
    }
}
