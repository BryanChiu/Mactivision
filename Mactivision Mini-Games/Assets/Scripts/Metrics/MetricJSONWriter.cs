using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

// MetricJSONWriter class is used to collect game data from metrics after a game has finished and output it to a file
public class MetricJSONWriter 
    // : MonoBehaviour 
    {

    private string gameName;
    private System.DateTime gameStartTime;

    public MetricJSONWriter(string gameName, System.DateTime gameStartTime) {
        this.gameName = gameName;
        this.gameStartTime = gameStartTime;
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

    // Testing purposes. Will remove before submission
    // void Start()
    // {
    //     Debug.Log("test123");
    //     MetricJSONWriter mjw = new MetricJSONWriter("digger", System.DateTime.Now);

    //     ButtonPressingMetric myBPMetric = new ButtonPressingMetric();
    //     myBPMetric.startRecording();
    //     myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.R, true));
    //     myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.A, true));
    //     myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.S, false));
    //     myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.R, true));
    //     myBPMetric.finishRecording();

    //     MemoryChoiceMetric myMCMetric = new MemoryChoiceMetric();
    //     myMCMetric.startRecording();
    //     myMCMetric.recordEvent(new MemoryChoiceEvent(System.DateTime.Now, new List<string> {"apple", "banana"}, "apple", false, System.DateTime.Now));
    //     myMCMetric.recordEvent(new MemoryChoiceEvent(System.DateTime.Now, new List<string> {"apple", "banana", "pear"}, "orange", false, System.DateTime.Now));
    //     myMCMetric.recordEvent(new MemoryChoiceEvent(System.DateTime.Now, new List<string> {"apple", "pear"}, "pear", true, System.DateTime.Now));
    //     myMCMetric.recordEvent(new MemoryChoiceEvent(System.DateTime.Now, new List<string> {"apple", "pear"}, "banana", true, System.DateTime.Now));
    //     myMCMetric.finishRecording();
        
    //     List<AbstractMetric> myList = new List<AbstractMetric>();
    //     myList.Add(myBPMetric);
    //     myList.Add(myMCMetric);
        
    //     mjw.logMetrics("test_out.json", System.DateTime.Now, myList);
    // }
}
