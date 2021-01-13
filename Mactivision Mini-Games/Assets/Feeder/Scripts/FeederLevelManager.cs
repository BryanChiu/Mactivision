using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FeederLevelManager : LevelManager
{
    public TMP_Text introText2;
    bool showIntro2; // a second text block b/c lots of info

    public Animator monster;
    public Transform plate;
    public Transform lever;
    public Dispenser dispenser;
    public AudioClip plate_up;
    public AudioClip plate_down;
    public AudioClip bite_sound;
    
    List<KeyCode> keysDown; // List of keys currently held down (not full history)

    public string seed;
    System.Random randomSeed;

    public int totalFoods = 6;
    public int changeFreq = 3;

    private float maxGameTime = 20f;

    KeyCode feedKey = KeyCode.RightArrow;
    KeyCode trashKey = KeyCode.LeftArrow;

    bool playerChoosing = false;
    bool animatingChoice = false;
    bool animatingDispense = false;
    float tiltPlateTo;

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        introText2.enabled = false;
        showIntro2 = false;
        countDoneText = "Start!";
        keysDown = new List<KeyCode>();
        recording = false;
   
        // Example of getting configuration data.
        FeederConfig config = (FeederConfig)Battery.Instance.GetCurrentConfig();
        maxGameTime = config.MaxTime;

        if (seed=="") seed = System.DateTime.Now.ToString();
        randomSeed = new System.Random(seed.GetHashCode());

        dispenser.Init(seed, totalFoods, changeFreq);
    }

    // Update is called once per frame
    void Update()
    {
        Event e = Event.current;
       
        maxGameTime = maxGameTime - Time.deltaTime;

        if (lvlState==2) {
            if (!recording) { // begin recording 
                recording = true;
                sound.clip = bite_sound;
            }
            if (maxGameTime < 0) { // game automatically ends after maxGameTime seconds
                EndLevel(1f);
            }

            if (animatingChoice) {
                TiltPlate();
            } else if (!playerChoosing && !animatingDispense) {
                animatingDispense = true;
                if (dispenser.DispenseNext()) {
                    StartCoroutine(WaitForFoodDispense(2.55f));
                } else {
                    StartCoroutine(WaitForFoodDispense(0.8f));
                }
            } else if (playerChoosing && (Input.GetKeyDown(feedKey) || Input.GetKeyDown(trashKey))) {
                playerChoosing = false;
                animatingChoice = true;
                if (Input.GetKeyDown(feedKey)) {
                    monster.Play("Base Layer.monster_eat");
                    sound.PlayDelayed(0.85f);
                    tiltPlateTo = -33f;
                } else {
                    tiltPlateTo = 33f;
                }
                sound.PlayOneShot(plate_up);
                StartCoroutine(WaitForChoiceAnimation());
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        if (lvlState==0 && e.isKey && e.type == EventType.KeyUp) {
            if (!showIntro2) {
                showIntro2 = true;
                introText.enabled = false;
                introText2.enabled = true;
                ResizeTextBG(GetRect(introText2));
            } else {
                introText2.enabled = false;
                StartLevel();
            }
        }
        if (lvlState==2 && e.isKey && e.keyCode!=KeyCode.None) {
            // When a keyboard key is initially pressed down, add it to list
            // We don't want to record when a key is HELD down
            if (e.type == EventType.KeyDown && !keysDown.Contains(e.keyCode)) {
                keysDown.Add(e.keyCode);

            // Remove key from list
            } else if (e.type == EventType.KeyUp) {
                keysDown.Remove(e.keyCode);
            }
        }
    }

    void TiltPlate()
    {
        float tiltSpeed = 100f;
        Vector3 rot = lever.localEulerAngles;
        rot.z = rot.z<180f ? rot.z : rot.z-360f;
        float oldZ = rot.z;
        rot.z = Mathf.MoveTowards(rot.z, tiltPlateTo, tiltSpeed * Time.deltaTime);
        if (tiltPlateTo<0f || oldZ<0f) {
            plate.GetChild(0).localEulerAngles = rot;
        } else {
            plate.localEulerAngles = rot;
        }
        lever.localEulerAngles = rot;
    }

    IEnumerator WaitForChoiceAnimation()
    {
        Debug.Log(Time.frameCount.ToString() + ": WaitForChoiceAnimtion Start");
        yield return new WaitForSeconds(1.3f);
        tiltPlateTo = 0f;
        sound.PlayOneShot(plate_down);

        yield return new WaitForSeconds(2.5f);
        lever.localEulerAngles = Vector3.zero;
        plate.localEulerAngles = Vector3.zero;
        plate.GetChild(0).localEulerAngles = Vector3.zero;
        animatingChoice = false;
        Debug.Log(Time.frameCount.ToString() + ": WaitForChoiceAnimtion End");
    }

    IEnumerator WaitForFoodDispense(float wait)
    {
        Debug.Log(Time.frameCount.ToString() + ": WaitForFoodDispense Start");
        yield return new WaitForSeconds(wait);
        playerChoosing = true;
        animatingDispense = false;
        Debug.Log(Time.frameCount.ToString() + ": WaitForFoodDispense End");
    }
}
