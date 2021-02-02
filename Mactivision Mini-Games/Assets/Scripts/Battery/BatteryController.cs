using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

public class EmptyConfigException : Exception
{

}

public class BadConfigException : Exception
{

}

public class InvalidScenesException : Exception
{

}

public class BatteryController
{   
    static private Dictionary<string,GameConfig> GameList = new Dictionary<string,GameConfig>
    {
        {"Digger", new DiggerConfig()},
        {"Feeder", new FeederConfig()},
        {"RockStar", new RockStarConfig()}
    };
    
    private BatteryConfig Config;
    private bool ConfigLoaded;

    public Dictionary<string,GameConfig>GetGameList()
    {
        return GameList;
    }

    public BatteryController()
    {
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

    public string StartTime()
    {
        return Config.StartTime;
    }

    public string EndTime()
    {
        return Config.EndTime;
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

    public string GenerateConfig()
    {
        var config = new BatteryConfig(); 
        config.Games = new List<GameConfig>();
   
        foreach (var game in GameList)
        {
            var g = game.Value;
            g.Scene = game.Key;
            config.Games.Add(game.Value);
        }

        string json = JsonConvert.SerializeObject(config, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        return json;
    }

    public string FolderTimeStamp()
    {
        return System.DateTime.Now.ToString("yyyyMMddhhmm");
    }

    public void LoadConfig(string json)
    {
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        BatteryConfig result = null;
       
        // catch all the JsonConvert exceptions into one exception.
        try
        {
            result = JsonConvert.DeserializeObject<BatteryConfig>(json, settings);
        }
        catch
        {
            throw new BadConfigException();
        }

        // empty json will return a null object
        if (result == null)
        {
            throw new EmptyConfigException();
        }

        // check scenes exist
        int num_of_scenes = result.Games.Count;
        int valid = 0;
        List<string> valid_scenes = new List<string>(GameList.Keys);

        foreach (GameConfig game in result.Games)
        {
            if (valid_scenes.Contains(game.Scene))
            {
                // Checks to make sure the correct scene is matched with it's
                // correct config class.
                var a = GameList[game.Scene].GetType();
                var b = game.GetType();
                if (a == b)
                {
                    valid++;
                }
            }
        }

        if (valid != num_of_scenes)
        {
            throw new InvalidScenesException();
        }

        Config = result;
    }
}
