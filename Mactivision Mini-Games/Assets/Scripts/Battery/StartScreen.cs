using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    // GUI Objects
    public Button StartButton;
    public Text DevOutput;

    // Developer GUI Objects
    public Button GenerateButton;
    public Dropdown ConfigDropdown;
    public Text ConfigDropdownSelected;

    // We can only start the battery if a configuration is loaded.
    private bool ConfigIsLoaded;
    static private string DefaultDropdownItemText = "SELECT BATTERY";

    // Start is called before the first frame update
    void Start()
    {
        // for stress testing
        //Application.targetFrameRate = 1;

        Debug.Log("Start Battery Start Scene");
        ConfigIsLoaded = false;

        StartButton.interactable = false;
        StartButton.onClick.AddListener(StartButtonClicked);

        GenerateButton.onClick.AddListener(GenerateButtonClicked); 

        ConfigDropdown.onValueChanged.AddListener(delegate {ConfigDropdownChange (); });

        // Creates a dropdown list of all the available battery configuration files. This is used for testing and debugging configurations.
        ConfigDropdown.ClearOptions();
        var options = new List<string>(Battery.Instance.ListConfigFiles());
        options.Insert(0, DefaultDropdownItemText);
        ConfigDropdown.AddOptions(options);
        ConfigDropdown.value = 0; // reset dropdown selection
    }

    // On change make sure it's an actual configuration file and not a heading. TODO: Better error handling.
    void ConfigDropdownChange()
    {
        ConfigIsLoaded = false;
        StartButton.interactable = false;
        string text = ConfigDropdownSelected.text;
        // If dropdown selection is not a heading.
        if (!text.Equals(DefaultDropdownItemText))
        {
            try
            {
                Battery.Instance.LoadBattery(text);
            }
            catch (Exception e)
            {
                if (e is EmptyConfigException)
                {
                    SetErrorMessage("Config is empty.");
                    return;
                }
                else if(e is InvalidScenesException)
                {
                    SetErrorMessage("Config has invalid scenes.");
                    return;
                }
                else if(e is BadConfigException)
                {
                    SetErrorMessage("Config could not be parsed, check param types and json format."); 
                    return;
                }
                else
                {
                    SetErrorMessage("Config raised " + e.Message);
                    return;
                }
            }
            Debug.Log(text);
            ConfigIsLoaded = true;
            StartButton.interactable = true;
            ListGames();
        }
        else{
            StartButton.interactable = false;
            ClearDevOuput();            
        }
    }

    // Generate a configuration template if button is clicked.
    void GenerateButtonClicked()
    {
        Battery.Instance.WriteExampleConfig();
    }

    void SetErrorMessage(string msg)
    {
        DevOutput.text = "Error\n" + msg; 
    }

    void ClearDevOuput()
    {
        DevOutput.text = "Developer Output";
    }

    // List games titles on the start screen. Useful for debugging. Not sure if it will remain for final version.
    void ListGames()
    {
        var games = Battery.Instance.GetGameList();
        DevOutput.text = "Battery Game List\n";
        foreach (string game in games)
        {
            DevOutput.text = DevOutput.text + "\t -" + game + "\n";  
        }
    }

    void StartButtonClicked()
    {
        // Start Battery and record playername for configuration output log.
        Battery.Instance.StartBattery();

        Debug.Log("Start Button Clicked.");
        if (ConfigIsLoaded)
        {
            // Start scene index -1 so next scene should be 0, the first game in the list under the configuration.
            Battery.Instance.LoadNextScene();
        }
    }
}
