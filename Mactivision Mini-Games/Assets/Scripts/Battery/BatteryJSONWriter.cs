using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BatteryJSONWriter
{

    public BatteryJSONWriter()
    {

    }
   
    public void Config()
    {
        var folder = System.DateTime.Now.ToString("yyyyMMddhhmm") + "/";

        // Try to create a new directory to old the battery output config and metric logs.
        /*
        try
        {
            System.IO.Directory.CreateDirectory(GetOutputPath());
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
        }
        */
    }

    public void Example()
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

        File("Assets/Resources/", "GeneratedTemplate.json", json);
    }

    private void File(string path, string filename, string json)
    {
        try 
        {
            System.IO.File.WriteAllText(path + filename, json);
        }
        catch (System.Exception e)
        {
            string ErrorMessage = "File Write Error\n" + e.Message;
        }
    }
}
