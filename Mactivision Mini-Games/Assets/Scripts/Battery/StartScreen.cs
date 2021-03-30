using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StartScreen : MonoBehaviour
{
    // GUI Objects
    public Button StartButton;
    public Button GenerateButton;
    public Text   Console;

    // We can only start the battery if a configuration is loaded.
    private bool ConfigIsLoaded;

    // Start is called before the first frame update
    void Start()
    {
        // for stress testing
        //Application.targetFrameRate = 1;

        Debug.Log("Start Screen Loaded");
        
        ConfigIsLoaded = false;
        StartButton.interactable = false;
        
        StartButton.onClick.AddListener(StartButtonClicked);
        GenerateButton.onClick.AddListener(GenerateButtonClicked); 

        StartCoroutine(GetConfigFromServer(GetBatteryConfig));
        
        // helpful for developers but not needed for users
        if (!Application.isEditor)
        { 
            GenerateButton.gameObject.SetActive(false);
        }
    }

    IEnumerator GetConfigFromServer(Action<string> method)
    {
        ClientServer Client = new ClientServer();
        
        UnityWebRequest get = Client.UpdateServerCreateRequest();
        yield return get.SendWebRequest();

        if (get.result != UnityWebRequest.Result.Success)
        {
            Console.text = "Network Error\n" + get.error; 
        }
        else
        {
            method(get.downloadHandler.text);
        }
    }

    void GetBatteryConfig(string text)
    {
        try
        {
            Battery.Instance.LoadBattery(text);
        }
        catch (Exception e)
        {
            if (e is EmptyConfigException)
            {
                Console.text = "ERR: Config is empty.";
                return;
            }
            else if(e is InvalidScenesException)
            {
                Console.text = "ERR: Config has invalid scenes.";
                return;
            }
            else if(e is BadConfigException)
            {
                Console.text = "ERR: Config could not be parsed, check param types and json format."; 
                return;
            }
            else
            {
                Console.text = "ERR: Config raised " + e.Message;
                return;
            }
        }
        Debug.Log("Battery Config Loaded Successfully");
        ConfigIsLoaded = true;
        StartButton.interactable = true;
    }
    
    // Generate a configuration template if button is clicked.
    void GenerateButtonClicked()
    {
        Battery.Instance.WriteExampleConfig();
    }

    void ClearDevOuput()
    {
        Console.text = "";
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
