using System.Collections.Generic;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

public class BatteryController
{   
    private BatteryConfig Config;
    private bool ConfigLoaded;

    public BatteryController(string config)
    {
        Config = LoadConfig(config);
    }

    public List<string> GameScenes()
    {   
        var list = new List<string>();
        foreach (GameConfig game in Config.Games)
        {
            list.Add(game.Scene);
        }
        return list;
    }

    public void Start()
    {
        Config.StartTime = TimeStamp();
    }

    public void End()
    {
        Config.EndTime = TimeStamp();
    }

    public GameConfig GetConfig(int index)
    {
        return Config.Games[index]; 
    }

    public string GetTestName(int index)
    {
        return GetConfig(index).TestName;
    }

    public string TimeStamp()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"); 
    }

    public string FolderTimeStamp()
    {
        return System.DateTime.Now.ToString("yyyyMMddhhmm");
    }

    public BatteryConfig LoadConfig(string json)
    {
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        return JsonConvert.DeserializeObject<BatteryConfig>(json, settings);
    }
    
}
