using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

// This class manages the majority of the game functionality
public class RockstarLevelManager : LevelManager
{
    public TMP_Text lrkeyText;      // text that contain instructions for leftKey and rightKey bind  
    public TMP_Text ukeyText;       // text that contain instructions for upKey bind 

    public Rockstar rockstar;       // responsible for moving the rockstar
    public Spotlight spotlight;     // used to move the spotlight
    public Meter meter;             // drops and raises the meters
    public Animator background;     // toggles the background animation on/off
    public AudioSource music;       // plays music
    public AudioClip[] tracks;      // music tracks

    float rockstarChangeFreq;       // how often the rockstar's destination changes
    float rockstarVelocity;         // speed the rockstar moves

    float spotlightVelocity;        // speed the spotlight can move
    
    float meterChangeFreq;          // how often the meter velocity changes
    float meterMinVel;              // minimum velocity the meter drops
    float meterMaxVel;              // maximum velocity the meter drops
    float meterUpVel;               // velocity the meter is raised by player
    float meterGoodRange;           // arbitrary "good" range for meter level

    KeyCode leftKey;                // key that moves the spotlight left
    KeyCode rightKey;               // key that moves the spotlight right
    KeyCode upKey;                  // key that raises the meter

    bool enableAnimations;          // (en/dis)able background animations
    int musicTrack;                 // set music track (-1 is no music, 0 is defaulted to 1, 1 is first track)

    PositionMetric pMetric;         // records position data during the game
    LinearVariableMetric lvMetric;  // records linear data during the game
    MetricJSONWriter metricWriter;  // outputs recording metric (pMetric and lvMetric) as a json file

    float currMeterVel;             // used to detect change in meter velocity for lvMetric
    float lastMeterLvl;             // used to determine delta level in between velocity changes
    float currRockstarVel;          // used to detect change in rockstar velocity for pMetric

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class

        InitConfigurable(); // initialize configurable values
        
        randomSeed = new System.Random(seed.GetHashCode());

        // set the leftKey for the intro instructions
        int tempIdx = lrkeyText.text.IndexOf("LKEY");
        lrkeyText.text = lrkeyText.text.Substring(0, tempIdx) + KeyCodeDict.toString[leftKey] + lrkeyText.text.Substring(tempIdx+4);

        // set the rightKey for the intro instructions
        tempIdx = lrkeyText.text.IndexOf("RKEY");
        lrkeyText.text = lrkeyText.text.Substring(0, tempIdx) + KeyCodeDict.toString[rightKey] + lrkeyText.text.Substring(tempIdx+4);

        // set the upKey for the intro instructions
        tempIdx = ukeyText.text.IndexOf("UKEY");
        ukeyText.text = ukeyText.text.Substring(0, tempIdx) + KeyCodeDict.toString[upKey] + ukeyText.text.Substring(tempIdx+4);

        countDoneText = "Rock!";

        pMetric = new PositionMetric(new List<string>{"rockstar", "spotlight"}); // initialize position metric recorder
        lvMetric = new LinearVariableMetric(0f, 100f, 75f, new List<string>{"gameDrop", "playerRaise"}); // initialize linear variable metric recorder
        metricWriter = new MetricJSONWriter("Rockstar", DateTime.Now, seed); // initialize metric data writer

        rockstar.Init(seed, rockstarChangeFreq, rockstarVelocity);
        spotlight.Init(spotlightVelocity);
        meter.Init(seed, meterChangeFreq, meterMinVel, meterMaxVel, meterUpVel, 0f, 100f, meterGoodRange, 50f);
        rockstar.enabled = false;
        spotlight.enabled = false;
        meter.enabled = false;

        currMeterVel = 0f;
        lastMeterLvl = 50f;
        currRockstarVel = 0f;
    }

    // Initialize values using config file, or default values if config values not specified
    void InitConfigurable()
    {
        RockstarConfig rockstarConfig = new RockstarConfig();

        // if running the game from the battery, override `rockstarConfig` with the config class from Battery
        RockstarConfig tempConfig = (RockstarConfig)Battery.Instance.GetCurrentConfig();
        if (tempConfig!=null) {
            rockstarConfig = tempConfig;
        } else {
            Debug.Log("Battery not found, using default values");
        }

        // use battery's config values, or default values if running game by itself
        seed = !String.IsNullOrEmpty(rockstarConfig.Seed) ? rockstarConfig.Seed : DateTime.Now.ToString(); // if no seed provided, use current DateTime
        maxGameTime = rockstarConfig.MaxGameTime > 0 ? rockstarConfig.MaxGameTime : Default(90f, "MaxGameTime");
        rockstarChangeFreq = rockstarConfig.RockstarChangeFreq > 0 ? rockstarConfig.RockstarChangeFreq : Default(1f, "RockstarChangeFreq");
        bool dependencyCheck = 0 < rockstarConfig.RockstarVelocity && rockstarConfig.RockstarVelocity <= rockstarConfig.SpotlightVelocity;
        rockstarVelocity = dependencyCheck ? rockstarConfig.RockstarVelocity : Default(2.5f, "RockstarVelocity");
        spotlightVelocity = dependencyCheck ? rockstarConfig.SpotlightVelocity : Default(3f, "SpotlightVelocity");
        meterChangeFreq = rockstarConfig.MeterChangeFreq > 0 ? rockstarConfig.MeterChangeFreq : Default(2f, "MeterChangeFreq");
        dependencyCheck = 0 < rockstarConfig.MeterMinVel && rockstarConfig.MeterMinVel <= rockstarConfig.MeterMaxVel && rockstarConfig.MeterMaxVel < rockstarConfig.MeterUpVel;
        meterMinVel = dependencyCheck ? rockstarConfig.MeterMinVel : Default(5f, "MeterMinVel");
        meterMaxVel = dependencyCheck ? rockstarConfig.MeterMaxVel : Default(30f, "MeterMaxVel");
        meterUpVel = dependencyCheck ? rockstarConfig.MeterUpVel : Default(60f, "MeterUpVel");
        meterGoodRange = rockstarConfig.MeterGoodRange > 0 && rockstarConfig.MeterGoodRange < 100 ? rockstarConfig.MeterGoodRange : 60f;
        // use default key if string cannot be parsed to keycode (or is null)
        dependencyCheck = rockstarConfig.LeftKey != rockstarConfig.RightKey && rockstarConfig.LeftKey != rockstarConfig.UpKey && rockstarConfig.RightKey != rockstarConfig.UpKey;
        try {
            leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), rockstarConfig.LeftKey);
            if (!KeyCodeDict.toString.ContainsKey(leftKey) || !dependencyCheck) throw new Exception();
        } catch (Exception) {
            leftKey = Default(KeyCode.A, "LeftKey");
        }
        try {
            rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), rockstarConfig.RightKey);
            if (!KeyCodeDict.toString.ContainsKey(rightKey) || !dependencyCheck) throw new Exception();
        } catch (Exception) {
            rightKey = Default(KeyCode.D, "RightKey");
        }
        try {
            upKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), rockstarConfig.UpKey);
            if (!KeyCodeDict.toString.ContainsKey(upKey) || !dependencyCheck) throw new Exception();
        } catch (Exception) {
            upKey = Default(KeyCode.L, "UpKey");
        }
        enableAnimations = tempConfig!=null ? rockstarConfig.EnableAnimations : Default(true, "EnableAnimations");
        musicTrack = rockstarConfig.MusicTrack == -1 || (rockstarConfig.MusicTrack > 0 && rockstarConfig.MusicTrack <= tracks.Length) ? rockstarConfig.MusicTrack : Default(1, "MusicTrack");

        // udpate battery config with actual/final values being used
        rockstarConfig.Seed = seed;
        rockstarConfig.MaxGameTime = maxGameTime;
        rockstarConfig.RockstarChangeFreq = rockstarChangeFreq;
        rockstarConfig.RockstarVelocity = rockstarVelocity;
        rockstarConfig.SpotlightVelocity = spotlightVelocity;
        rockstarConfig.MeterChangeFreq = meterChangeFreq;
        rockstarConfig.MeterMinVel = meterMinVel;
        rockstarConfig.MeterMaxVel = meterMaxVel;
        rockstarConfig.MeterUpVel = meterUpVel;
        rockstarConfig.MeterGoodRange = meterGoodRange;
        rockstarConfig.LeftKey = leftKey.ToString();
        rockstarConfig.RightKey = rightKey.ToString();
        rockstarConfig.UpKey = upKey.ToString();
        rockstarConfig.EnableAnimations = enableAnimations;
        rockstarConfig.MusicTrack = musicTrack;
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        // navigate through the instructions before the game starts
        if (lvlState==0 && e.type == EventType.KeyUp) {
            if (e.keyCode == KeyCode.B && instructionCount>0) {
                ShowInstruction(--instructionCount);
            } else if (e.keyCode == KeyCode.N && instructionCount<instructions.Length) {
                ShowInstruction(++instructionCount);
            }
        }

        // game is over, go to next game/finish battery
        if (lvlState==4 && e.type == EventType.KeyUp) {
            Battery.Instance.LoadNextScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
            // begin game, begin recording 
            if (!pMetric.isRecording) StartGame(); 
                
            // game automatically ends after maxGameTime seconds
            if (Time.time-gameStartTime >= maxGameTime) { 
                EndGame();
                return;
            }

            BackgroundAnimation();
            InputHandler();
            RecordMetricEvents();
        }
    }

    // Begin the actual game, start recording metrics
    void StartGame()
    {
        pMetric.startRecording();
        lvMetric.startRecording();
        metricWriter = new MetricJSONWriter("Rockstar", DateTime.Now, seed); // initialize metric data writer
        gameStartTime = Time.time;
        rockstar.enabled = true;
        spotlight.enabled = true;
        meter.enabled = true;
        if (enableAnimations) background.Play("Base Layer.rockstarbg_1");
        if (musicTrack > -1) {
            music.clip = tracks[musicTrack-1]; 
            music.Play();
        }
    }

    // End game, stop animations, sounds. Finish recording metrics
    void EndGame()
    {
        pMetric.finishRecording();
        lvMetric.finishRecording();
        var str = metricWriter.GetLogMetrics(
            DateTime.Now, 
            new List<AbstractMetric>(){pMetric, lvMetric}
        );
        StartCoroutine(Post("rockstar_"+DateTime.Now.ToFileTime()+".json", str));
        EndLevel(0f);

        rockstar.enabled = false;
        spotlight.enabled = false;
        meter.enabled = false;
        background.speed = 0f;
        music.Stop();
    }

    void BackgroundAnimation()
    {
        if (enableAnimations) {
            if (Math.Abs(rockstar.GetPosition().x - spotlight.GetPosition().x) < 1) {
                background.speed = Mathf.Clamp(background.speed + Time.deltaTime, 0f, 1f);
            } else {
                background.speed = Mathf.Clamp(background.speed - Time.deltaTime, 0f, 1f);
            }
        }
    }

    // Handles player keyboard input
    void InputHandler()
    {
        if (Input.GetKey(upKey) || Input.GetKeyDown(upKey)) MeterUp();
        if (Input.GetKey(leftKey) || Input.GetKeyDown(leftKey)) SpotlightLeft();
        if (Input.GetKey(rightKey) || Input.GetKeyDown(rightKey)) SpotlightRight();
    }

    // Raise the meter and add a linear variable event
    void MeterUp()
    {
        meter.Raise();
    }

    // Move the spotlight left
    void SpotlightLeft()
    {
        spotlight.Move(false);
    }

    // Move the spotlight right
    void SpotlightRight()
    {
        spotlight.Move(true);
    }

    // Called each frame to add metric event
    void RecordMetricEvents()
    {
        if (Input.GetKeyDown(leftKey) || Input.GetKeyUp(leftKey) || Input.GetKeyDown(rightKey) || Input.GetKeyUp(rightKey) || !Mathf.Approximately(currRockstarVel, rockstar.currVelocity)) {
            pMetric.recordEvent(new PositionEvent(DateTime.Now, new List<Vector2>{rockstar.GetPosition(), spotlight.GetPosition()}));
            currRockstarVel = rockstar.currVelocity;
        }
        if (Input.GetKeyDown(upKey) || Input.GetKeyUp(upKey)) {
            lvMetric.recordEvent(new LinearVariableEvent(DateTime.Now, meter.level, meter.level-lastMeterLvl, 1));
            lastMeterLvl = meter.level;
        }
        if (!Mathf.Approximately(currMeterVel, meter.velocity)) {
            lvMetric.recordEvent(new LinearVariableEvent(DateTime.Now, meter.level, meter.level-lastMeterLvl, 0));
            currMeterVel = meter.velocity;
            lastMeterLvl = meter.level;
        }
    }
}
