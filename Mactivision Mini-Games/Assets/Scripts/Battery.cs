using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Battery : MonoBehaviour
{
    // Singleton instance of battery
    public static Battery Instance { get; private set; }
   
    // Managing Mini Games
    private int SceneIndex = -1;
    public Object StartScene;
    public Object EndScene;
    public BatteryConfig Config;

    // Config File
    public TextAsset ConfigFile;

    void Start()
    {
        LoadScene(GetCurrentScene());
    }

    public GameConfig GetCurrentConfig()
    {
        return Config.Games[SceneIndex];
    }

    public void LoadBattery(string BatteryConfig)
    {
        TextAsset json = Resources.Load<TextAsset>(BatteryConfig);
        Config = JsonConvert.DeserializeObject<BatteryConfig>(json.text, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        }); 
    }

    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Single); 
    }

    public void StartBattery()
    {
        Config.StartTime = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
    }
    
    public void EndBattery()
    {
        Config.EndTime = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"); 

        var json = JsonConvert.SerializeObject(Config);
        
        WriteJSONToFile("Assets/Output/", "BatteryOutput.json", json);
    }

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

    public void SetPlayerName(string Name)
    {
        Config.PlayerName = Name;
    }

    public void LoadNextScene()
    {
        SceneIndex++;
        LoadScene(GetCurrentScene());
        Debug.Log("Load Next Scene");
    }

    private string GetCurrentScene()
    {  
        Debug.Log("SceneIndex = " + SceneIndex);

        if (SceneIndex < 0)
        {
            return StartScene.name;
        }
        
        if (SceneIndex < Config.Games.Count)
        {
            return Config.Games[SceneIndex].Scene;
        }

        return EndScene.name;
    }

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

    public void WriteExampleConfig()
    {
        var example = new BatteryConfig(); 
        var digger = new DiggerConfig();
        var feeder = new FeederConfig();

        example.Games = new List<GameConfig>();
        
        example.Games.Add(digger);
        example.Games.Add(feeder);

        string json = JsonConvert.SerializeObject(example, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        WriteJSONToFile("Assets/Resources/", "Example.json", json);

        Debug.Log("Write Example Config Completed.");
    }

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
