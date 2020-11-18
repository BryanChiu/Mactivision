using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerLevelManager : LevelManager
{
    public PlayerController player;
    public ChestAnimator chest;
    
    KeyCode digKey;
    int digAmount; // must be a +ve int (>0) rounds up to nearest 10

    List<KeyCode> keysDown; // List of keys currently held down (not full history)
    InputRecorder recorder; // input recorder (this will record full history)
    bool recording;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        countDown0Text = "Dig!";
        digKey = KeyCode.B;
        digAmount = 100;
        keysDown = new List<KeyCode>();
        recorder = new InputRecorder();
        recording = false;
    }

    // Update is called once per frame
    void Update()
    {
        LvlMngr();
        if (lvlState==2) {
            if (!recording) {
                recording = true;
                recorder.StartRec();
                SetDigKeyForGround();
                SetDigAmountForGround();
            }
            if (chest.opened) {
                recorder.EndRec();
                EndLevel();
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        if (lvlState==0 && e.isKey && countDown>4) {
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

    void SetDigKeyForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetDigKey(digKey);
        }
    }

    void SetDigAmountForGround() {
        GameObject[] groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        foreach (GameObject block in groundBlocks) {
            block.GetComponent<GroundBreaker>().SetHitsToBreak(Mathf.CeilToInt(digAmount/10f));
        }
    }
}
