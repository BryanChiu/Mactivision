using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    List<KeyCode> keysDownArray; // List of keys currently held down (not full history)
    InputRecorder recorder; // input recorder (this will record full history)
    public ChestAnimator chest;
    int presses = 0;

    // Start is called before the first frame update
    void Start()
    {
        keysDownArray = new List<KeyCode>();
        recorder = new InputRecorder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) recorder.StartRec();
        if (chest.opened) recorder.EndRec();
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI() {
        Event e = Event.current;
        if (e.isKey && e.keyCode!=KeyCode.None) {
            // When a keyboard key is initially pressed down, add it to list
            // We don't want to record when a key is HELD down
            if (e.type == EventType.KeyDown && !keysDownArray.Contains(e.keyCode)) {
                keysDownArray.Add(e.keyCode);
                recorder.AddEvent(e.keyCode, true);
            // Remove key from list
            } else if (e.type == EventType.KeyUp) {
                keysDownArray.Remove(e.keyCode);
                recorder.AddEvent(e.keyCode, false);
            }
        }
    }
}
