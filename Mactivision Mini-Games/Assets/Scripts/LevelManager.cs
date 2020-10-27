using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
    public ChestAnimator chest;
    public PostProcessVolume postprocess;
    public Text intro;
    public Text outro;
    
    List<KeyCode> keysDownArray; // List of keys currently held down (not full history)
    InputRecorder recorder; // input recorder (this will record full history)

    int lvlState;

    // Start is called before the first frame update
    void Start()
    {
        keysDownArray = new List<KeyCode>();
        recorder = new InputRecorder();

        outro.enabled = false;
        lvlState = 0;
        ChangeBlur(2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==0 && Input.GetKeyDown("b")) {
            recorder.StartRec();
            lvlState++;
            ChangeBlur(10f);
            intro.enabled = false;
        }
        if (lvlState==1 && chest.opened) {
            recorder.EndRec();
            lvlState++;
            Invoke("LevelComplete", 5);
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
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

    void LevelComplete()
    {
        ChangeBlur(2f);
        outro.enabled = true;
    }

    void ChangeBlur(float dist)
    {
        if (postprocess) {
            DepthOfField pr;
            
            if (postprocess.profile.TryGetSettings<DepthOfField>(out pr)){
                pr.focusDistance.value = dist;
            }
         }
    }
}
