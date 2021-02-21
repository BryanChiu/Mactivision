using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

// MetricJSONWriter class is used to collect game data from metrics after a game has finished and output it to a file
public class MetricJSONWriter {

    private string gameName;
    private System.DateTime gameStartTime;
    private string seed;

    public MetricJSONWriter(string gameName, System.DateTime gameStartTime) {
        this.gameName = gameName;
        this.gameStartTime = gameStartTime;
    }

    public MetricJSONWriter(string gameName, System.DateTime gameStartTime, string seed) {
        this.gameName = gameName;
        this.gameStartTime = gameStartTime;
        this.seed = seed;
    }

    // logMetrics takes 3 parameters:
    //   * fileName: Path and file to output JSON to (from the root of the unity project)
    //   * gameEndTime: timestamp when the game finished
    //   * metrics: list of metrics using in the recently finished game to collect and output data from
    public void logMetrics(string fileName, System.DateTime gameEndTime, List<AbstractMetric> metrics) {
        
        // Setup file and json text writer
        using (StreamWriter file = File.CreateText(fileName)) {
            JsonTextWriter writer = new JsonTextWriter(file);
            writer.Formatting = Formatting.Indented;

            // build a json object with game information, followed by a list of metric data
            JObject json = new JObject();
            json["game"] = new JValue(gameName);
            json["startTime"] = new JValue(gameStartTime);
            json["endTime"] = new JValue(gameEndTime);
            json["seed"] = new JValue(seed);

            JArray jsonMetrics = new JArray();
            foreach (AbstractMetric m in metrics) {

                // metric data is collected from each metric
                jsonMetrics.Add(m.getJSON());
            }
            json["metrics"] = jsonMetrics;

            // write json to file
            json.WriteTo(writer);
        }
    }

    public string GetLogMetrics(System.DateTime gameEndTime, List<AbstractMetric> metrics) {

        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        // Setup file and json text writer
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            writer.Formatting = Formatting.Indented;

            // build a json object with game information, followed by a list of metric data
            JObject json = new JObject();
            json["game"] = new JValue(gameName);
            json["startTime"] = new JValue(gameStartTime);
            json["endTime"] = new JValue(gameEndTime);
            json["seed"] = new JValue(seed);

            JArray jsonMetrics = new JArray();
            foreach (AbstractMetric m in metrics) {

                // metric data is collected from each metric
                jsonMetrics.Add(m.getJSON());
            }
            json["metrics"] = jsonMetrics;

            // write json to file
            json.WriteTo(writer);
        }

        return sw.ToString();
    }

}
