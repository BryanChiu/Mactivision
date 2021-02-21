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

        StartCoroutine(Get("get", GetBatteryConfig));
        
        StartCoroutine(Get("new", CreateOutputFolder));
        
        StartCoroutine(Test("test.pancakes", "hello world"));

        // helpful for developers but not needed for users
        if (!Application.isEditor)
        {
            GenerateButton.gameObject.SetActive(false);
        }
    }

    IEnumerator Test(string filename, string data)
    {
        var post = new UnityWebRequest ("http://127.0.0.1:8000/post?filename=" + filename, "POST");
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        post.uploadHandler = new UploadHandlerRaw(bytes);
        post.downloadHandler = new DownloadHandlerBuffer();
        post.SetRequestHeader("Content-Type", "application/json");
        yield return post.SendWebRequest();

        if (post.result != UnityWebRequest.Result.Success)
        {
            Console.text = "Network Error\n" + post.error; 
        }
        else
        {
            Debug.Log("Post Success!");
        }
    }

    IEnumerator Get(string slug, Action<string> method)
    {
        UnityWebRequest get = UnityWebRequest.Get("http://127.0.0.1:8000/" + slug);
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
        Debug.Log("Battery Config Loaded Successfully");
        ConfigIsLoaded = true;
        StartButton.interactable = true;
    }

    void CreateOutputFolder(string text)
    {
        Debug.Log("Folder = " + text); 
    }
    
    // Generate a configuration template if button is clicked.
    void GenerateButtonClicked()
    {
        Battery.Instance.WriteExampleConfig();
    }

    void SetErrorMessage(string msg)
    {
        Console.text = "Error\n" + msg; 
    }

    void ClearDevOuput()
    {
        Console.text = "Debug Console";
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
