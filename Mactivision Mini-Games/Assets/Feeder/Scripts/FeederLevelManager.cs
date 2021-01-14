using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

// This class manages the majority of the game functionality
public class FeederLevelManager : LevelManager
{
    public TMP_Text introText2;             // a second text block b/c lots of info
    bool showIntro2;

    public Animator monster;                // monster that eats food
    public Transform plate;                 // plate that the food GameObject falls onto, will be tilted
    public Transform lever;                 // purely for animation purposes
    public Dispenser dispenser;             // responsible for choosing and dispensing foods
    public AudioClip plate_up;              // BRRRRRR
    public AudioClip plate_down;            // brrrrrr
    public AudioClip bite_sound;            // nom nom nom
    
    public string seed;                     // optional manually entered seed
    System.Random randomSeed;               // seed of the current game

    public int totalFoods = 6;              // number of foods to be used in the current game
    public float avgUpdateFreq = 3f;        // average number of foods dispensed between each food update
    public float stdDevUpdateFreq = 2.8f;   // standard deviation of `avgUpdateFreq`

    public int maxGameTime = 180;           // length of the game
    float gameStartTime;

    KeyCode feedKey = KeyCode.RightArrow;   // press to feed monster
    KeyCode trashKey = KeyCode.LeftArrow;   // press to throw away

    bool playerChoosing = false;            // true when food has been dispensed and waiting for player to make decision
    bool animatingChoice = false;           // true when player has made choice and stuff is animating
    bool animatingDispense = false;         // true when stuff animating is finished and the next food is being dispensed
    
    float tiltPlateTo;                      // angle to tilt the plate (food will slide into trash or monster's mouth)

    MemoryChoiceMetric mcMetric;            // records choice data during the game
    MetricJSONWriter metricWriter;          // outputs recording metric (mcMetric) as a json file

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        introText2.enabled = false;
        showIntro2 = false;
        countDoneText = "Start!";

        // if no seed is provided, use the current date and time
        if (seed=="") seed = DateTime.Now.ToString();
        randomSeed = new System.Random(seed.GetHashCode());

        mcMetric = new MemoryChoiceMetric(); // initialize metric recorder
        metricWriter = new MetricJSONWriter("Feeder", DateTime.Now); // initialize metric data writer

        dispenser.Init(seed, totalFoods, avgUpdateFreq, stdDevUpdateFreq); // initialize the dispenser
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
            // begin game, begin recording 
            if (!mcMetric.isRecording) { 
                mcMetric.startRecording();
                gameStartTime = Time.time;
                sound.clip = bite_sound;
            }
            // game automatically ends after maxGameTime seconds
            if (Time.time-gameStartTime > maxGameTime) { 
                mcMetric.finishRecording();
                metricWriter.logMetrics(
                    "Logs/feeder_"+DateTime.Now.ToFileTime()+".json", 
                    DateTime.Now, 
                    new List<AbstractMetric>(){mcMetric}
                );
                EndLevel(1f);
            }

            // animate the plate tilting food into trash/monster
            if (animatingChoice) { 
                TiltPlate();
            // animate a possible food update, and food dispensing
            } else if (!playerChoosing && !animatingDispense) {
                animatingDispense = true;
                if (dispenser.DispenseNext()) {
                    StartCoroutine(WaitForFoodDispense(2.55f));
                } else {
                    StartCoroutine(WaitForFoodDispense(0.8f));
                }
            // does nothing until player makes a choice
            } else if (playerChoosing && (Input.GetKeyDown(feedKey) || Input.GetKeyDown(trashKey))) {
                playerChoosing = false;
                animatingChoice = true;

                // set the angle the plate should to and play plate sound. Play monster eating animation if applicable
                if (Input.GetKeyDown(feedKey)) {
                    monster.Play("Base Layer.monster_eat");
                    sound.PlayDelayed(0.85f);
                    tiltPlateTo = -33f;
                } else {
                    tiltPlateTo = 33f;
                }

                // record the choice made
                mcMetric.recordEvent(new MemoryChoiceEvent(
                    dispenser.choiceStartTime,
                    new List<String>(dispenser.goodFoods),
                    dispenser.currentFood,
                    Input.GetKeyDown(feedKey),
                    DateTime.Now
                ));

                // animate choice and play plate sound
                sound.PlayOneShot(plate_up);
                StartCoroutine(WaitForChoiceAnimation(Input.GetKeyDown(feedKey) && !dispenser.MakeChoice(Input.GetKeyDown(feedKey))));
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        // this is the "Press any key to continue" before the start of the game
        if (lvlState==0 && e.type == EventType.KeyUp) {
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
    }

    // This function tilts the plate by a small increment. When called over multiple
    // successive frames, it animates smoothly. If the plate reaches the intended
    // tilt amount, it stays there. 
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

    // Wait for the choice animation to finish. If the player feeds the monster
    // incorrectly, wait longer for the monster spit animation.
    IEnumerator WaitForChoiceAnimation(bool spit)
    {
        // return the plate to the original resting position
        yield return new WaitForSeconds(1.3f);
        tiltPlateTo = 0f;
        sound.PlayOneShot(plate_down);

        // Make sure angles are zeroed, and finish animation
        yield return new WaitForSeconds(spit ? 2.5f : 1.33f);
        lever.localEulerAngles = Vector3.zero;
        plate.localEulerAngles = Vector3.zero;
        plate.GetChild(0).localEulerAngles = Vector3.zero;
        animatingChoice = false;
    }

    // Wait for the food dispensing animation
    IEnumerator WaitForFoodDispense(float wait)
    {
        yield return new WaitForSeconds(wait);
        playerChoosing = true;
        animatingDispense = false;
    }
}
