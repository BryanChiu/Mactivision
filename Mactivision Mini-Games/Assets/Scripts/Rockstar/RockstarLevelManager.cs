using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

// This class manages the majority of the game functionality
public class RockstarLevelManager : LevelManager
{

    // Start is called before the first frame update
    void Start()
    {
        Setup(); // run initial setup, inherited from parent class

        InitConfigurable(); // initialize configurable values
    }

    // Initialize values using config file, or default values if config values not specified
    void InitConfigurable()
    {
        RockStarConfig rockstarConfig = new RockStarConfig();

        // if running the game from the battery, override `feederConfig` with the config class from Battery
        RockStarConfig tempConfig = (RockStarConfig)Battery.Instance.GetCurrentConfig();
        if (tempConfig!=null) {
            rockstarConfig = tempConfig;
            outputPath = Battery.Instance.GetOutputPath();
        } else {
            Debug.Log("Battery not found, using default values");
        }
    }

    // Handles GUI events (keyboard, mouse, etc events)
    void OnGUI()
    {
        Event e = Event.current;
        // this is the "Press any key to continue" before the start of the game
        if (lvlState==0 && e.type == EventType.KeyUp) {

        }

        if (lvlState==4 && e.type == EventType.KeyUp) {
            Battery.Instance.LoadNextScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlState==2) {
           
        }
    }

    // Begin the actual game, start recording metrics
    void StartGame()
    {

    }

    // End game, finish recording metrics
    void EndGame()
    {

    }
}