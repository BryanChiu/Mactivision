using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Battery : MonoBehaviour
{
    // Singleton instance of Battery
    public static Battery Instance { get; private set; }
   
    // Managing Mini Games
    public Object StartScene;    // Allows you to use unity interface to drag in the scenes.
    public Object EndScene;

    // Config File as JSON Text File.
    public TextAsset ConfigFile;

    // TODO: Check for missing / in folder names
    // Parent directory for all battery output files..
    private string OutputFolderPath = "Output/"; 

    // Controllers and Writers
    private BatteryController BatteryControl;
    private SceneController SceneControl;
    private BatteryJSONWriter Write;

    // When scene is loaded by Unity this function is called first.
    void Start()
    {
        Write = new BatteryJSONWriter();
        LoadBattery("Demo");
        LoadScene(SceneControl.Name());
    }

    public string GetGameName()
    {
        // Game name is part of the GameConfig interface so does not require casting to the specific game config. Useful to generating log files by name. Name is not the name of the game but that specific test of a game. TODO: Better naming.
        return BatteryControl.GetTestName(SceneControl.Current());
    }

    // Gets the full path of current battery folder
    // TODO: Better naming.
    public string GetOutputPath()
    {
        return OutputFolderPath;
    }

    // Returns the GameConfig interface type. Specific games will have to cast the GameConfig to their respective Config class in order to child parameters. 
    public GameConfig GetCurrentConfig()
    {
        return BatteryControl.GetConfig(SceneControl.Current());
    }

    // Load the BatteryConfig JSON file and deserialize it while maintaining type information. Currently uses TextAsset which is a Unity Resource type. This allows easier file reading but it may not be wise to clutter resource folder. 
    public void LoadBattery(string BatteryConfig)
    {
        TextAsset json = Resources.Load<TextAsset>(BatteryConfig);
        BatteryControl = new BatteryController(json.text);
        SceneControl = new SceneController(StartScene.name, EndScene.name, BatteryControl.GameScenes());
    }

    // Scenes are loaded by name
    public void LoadScene(string Scene)
    {
        // LoadSceneMode.Single means that all other scenes are unloaded before new scene is loaded.
        SceneManager.LoadScene(Scene, LoadSceneMode.Single); 
    }

    public void StartBattery()
    {
        BatteryControl.Start();
    }
    
    public void EndBattery()
    {
        BatteryControl.End();
    }

    public void LoadNextScene()
    {
        // Scenes are indexed according to the order they appear in the battery config games list. The earlier in the list the earlier they will be loaded.
        SceneControl.Next();
        LoadScene(SceneControl.Name());
    }

    private string GetCurrentScene()
    {  
        return SceneControl.Name();
    }

    // Lists the games that player will play during the battery session. Undecided if it will be a more than just useful for debugging.
    public List<string> GetGameList()
    {
        return BatteryControl.GameScenes();
    }

    // As the configurable variables are added, deleted or renamed during development in order not have to constantly sync these names with the configuration files this function can be used to generate a blank configuration file based off those variables. 
    public void WriteExampleConfig()
    {
        Write.Example();
    }

    // Prevents Battery from being destroyed when scenes are unloaded.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
