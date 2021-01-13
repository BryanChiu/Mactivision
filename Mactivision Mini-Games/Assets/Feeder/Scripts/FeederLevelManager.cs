using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class FeederLevelManager : LevelManager
{
    public TMP_Text introText2;                 // a second text block b/c lots of info
    bool showIntro2;

    public Animator monster;
    public Transform plate;
    public Transform lever;
    public Dispenser dispenser;
    public AudioClip plate_up;
    public AudioClip plate_down;
    public AudioClip bite_sound;
    
    public string seed;                     // optional manually entered seed
    System.Random randomSeed;               // seed of the current game

    public int totalFoods = 6;              // number of foods to be used in the current game
    public float avgUpdateFreq = 3f;        // average number of foods dispensed between each food update
    public float stdDevUpdateFreq = 2.8f;   // standard deviation of `avgUpdateFreq`

    private float maxGameTime = 240f;

    KeyCode feedKey = KeyCode.RightArrow;
    KeyCode trashKey = KeyCode.LeftArrow;

    bool playerChoosing = false;
    bool animatingChoice = false;
    bool animatingDispense = false;
    float tiltPlateTo;

    MemoryChoiceMetric mcMetric; // records choice data during the game
    MetricJSONWriter metricWriter; // outputs recording metric (mcMetric) as a json file

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class
        
        introText2.enabled = false;
        showIntro2 = false;
        countDoneText = "Start!";

        if (seed=="") seed = DateTime.Now.ToString();
        randomSeed = new System.Random(seed.GetHashCode());

        mcMetric = new MemoryChoiceMetric();

        metricWriter = new MetricJSONWriter("Feeder", DateTime.Now);

        // Example of getting configuration data.
        FeederConfig config = (FeederConfig)Battery.Instance.GetCurrentConfig();
        maxGameTime = config.MaxTime;

        dispenser.Init(seed, totalFoods, avgUpdateFreq, stdDevUpdateFreq);
    }

    // Update is called once per frame
    void Update()
    {
        Event e = Event.current;
       
        maxGameTime = maxGameTime - Time.deltaTime;

        if (lvlState==2) {
            if (!mcMetric.isRecording) { // begin recording 
                mcMetric.startRecording();
                sound.clip = bite_sound;
            }
            if (maxGameTime < 0) { // game automatically ends after maxGameTime seconds
                mcMetric.finishRecording();
                metricWriter.logMetrics(
                    "Logs/feeder_"+DateTime.Now.ToFileTime()+".json", 
                    DateTime.Now, 
                    new List<AbstractMetric>(){mcMetric}
                );
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

                mcMetric.recordEvent(new MemoryChoiceEvent(
                    dispenser.choiceStartTime,
                    new List<String>(dispenser.goodFoods),
                    dispenser.currentFood,
                    Input.GetKeyDown(feedKey),
                    DateTime.Now
                ));

                sound.PlayOneShot(plate_up);
                StartCoroutine(WaitForChoiceAnimation(Input.GetKeyDown(feedKey) && dispenser.MakeChoice(Input.GetKeyDown(feedKey))));
            }
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
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

    IEnumerator WaitForChoiceAnimation(bool feed)
    {
        yield return new WaitForSeconds(1.3f);
        tiltPlateTo = 0f;
        sound.PlayOneShot(plate_down);

        yield return new WaitForSeconds(feed ? 1.33f : 2.5f);
        lever.localEulerAngles = Vector3.zero;
        plate.localEulerAngles = Vector3.zero;
        plate.GetChild(0).localEulerAngles = Vector3.zero;
        animatingChoice = false;
    }

    IEnumerator WaitForFoodDispense(float wait)
    {
        yield return new WaitForSeconds(wait);
        playerChoosing = true;
        animatingDispense = false;
    }
}
