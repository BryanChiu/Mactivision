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
    private int SceneIndex = -1; // The starting scene is -1
    public Object StartScene;    // Allows you to use unity interface to drag in the scenes.
    public Object EndScene;
    public BatteryConfig Config; // The configuration settings for the battery.

    // Config File as JSON Text File.
    public TextAsset ConfigFile;

    // Files and Folder names
    // TODO: Check for missing / in folder names.
    private string OutputFolderPath = "Output/"; // Parent directory for all battery output files.
    private string OutputFolderName = ""; // Generated folder name which will be inside OutFolderPath

    // When scene is loaded by Unity this function is called first.
    void Start()
    {
        LoadScene(GetCurrentScene());
    }

    public string GetGameName()
    {
        // Game name is part of the GameConfig interface so does not require casting to the specific game config. Useful to generating log files by name. Name is not the name of the game but that specific test of a game. TODO: Better naming.
        return GetCurrentConfig().Name; 
    }

    // Gets the full path of current battery folder
    // TODO: Better naming.
    public string GetOutputPath()
    {
        return OutputFolderPath + OutputFolderName;
    }

    // Returns the GameConfig interface type. Specific games will have to cast the GameConfig to their respective Config class in order to child parameters. 
    public GameConfig GetCurrentConfig()
    {
        return Config.Games[SceneIndex];
    }

    // Load the BatteryConfig JSON file and deserialize it while maintaining type information. Currently uses TextAsset which is a Unity Resource type. This allows easier file reading but it may not be wise to clutter resource folder. 
    public void LoadBattery(string BatteryConfig)
    {
        TextAsset json = Resources.Load<TextAsset>(BatteryConfig);
        Config = JsonConvert.DeserializeObject<BatteryConfig>(json.text, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        }); 
    }

    // Scenes are loaded by name
    public void LoadScene(string Scene)
    {
        // LoadSceneMode.Single means that all other scenes are unloaded before new scene is loaded.
        SceneManager.LoadScene(Scene, LoadSceneMode.Single); 
    }

    public void StartBattery()
    {
        // Start time of the Battery.
        Config.StartTime = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

        // Timestamp in a folder name friendly format
        OutputFolderName = System.DateTime.Now.ToString("yyyyMMddhhmm") + "/";

        // Try to create a new directory to old the battery output config and metric logs.
        try
        {
            System.IO.Directory.CreateDirectory(GetOutputPath());
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
        }
    }
    
    public void EndBattery()
    {
        // End time of the Battery.
        Config.EndTime = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"); 

        var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
        
        WriteJSONToFile(GetOutputPath(), "BatteryConfig.json", json);
    }

    // Write JSON strings to files. Used to write Battery configuration log after Battery has ended and to generate Battery configuration template.
    private void WriteJSONToFile(string path, string filename, string json)
    {
        try 
        {
            System.IO.File.WriteAllText(path + filename, json);
        }
        catch (System.Exception e)
        {
            string ErrorMessage = "File Write Error\n" + e.Message;
            Debug.LogError(ErrorMessage);
        }
        Debug.Log("Wrote Battery File");
    }

    // Sets the players name, which is identifiy which Battery configuration and metrics belongs to which player.
    public void SetPlayerName(string Name)
    {
        Config.PlayerName = Name;
    }

    public void LoadNextScene()
    {
        // Scenes are indexed according to the order they appear in the battery config games list. The earlier in the list the earlier they will be loaded.
        SceneIndex++;
        LoadScene(GetCurrentScene());
        Debug.Log("Load Next Scene");
    }

    private string GetCurrentScene()
    {  
        Debug.Log("SceneIndex = " + SceneIndex);

        // If a negative SceneIndex means the battery hasn't started so show the start screen.
        if (SceneIndex < 0)
        {
            return StartScene.name;
        }
       
        // A SceneIndex that is not negative and in bounds means it's a game scene.
        if (SceneIndex < Config.Games.Count)
        {
            return Config.Games[SceneIndex].Scene;
        }

        // No more game scenes and the start screen has been already be seen so all that's left is the end screen.
        return EndScene.name;
    }

    // Lists the games that player will play during the battery session. Undecided if it will be a more than just useful for debugging.
    public List<string> GetGameList()
    {
        var list = new List<string>();
        foreach (GameConfig config in Config.Games)
        {
            list.Add(config.Scene);
            Debug.Log(config.Scene);
        }
        return list;
    }

    // As the configurable variables are added, deleted or renamed during development in order not have to constantly sync these names with the configuration files this function can be used to generate a blank configuration file based off those variables. 
    public void WriteExampleConfig()
    {
        // For each new game a configuration must be created and be added to the games list.

        var example = new BatteryConfig(); 
        var digger = new DiggerConfig();
        var feeder = new FeederConfig();

        example.Games = new List<GameConfig>();
      
        example.Games.Add(digger);
        example.Games.Add(feeder);

        // Convert objects to JSON while retaining type information.
        string json = JsonConvert.SerializeObject(example, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        // TODO: Abstract out path name and filenames.
        WriteJSONToFile("Assets/Resources/", "GeneratedTemplate.json", json);

        Debug.Log("Write Example Config Completed.");
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
