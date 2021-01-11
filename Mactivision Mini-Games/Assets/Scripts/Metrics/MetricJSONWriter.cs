using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MetricJSONWriter : MonoBehaviour {

    private string gameName;
    private System.DateTime gameStartTime;

    MetricJSONWriter(string gameName, System.DateTime gameStartTime) {
        this.gameName = gameName;
        this.gameStartTime = gameStartTime;
    }

    public void logMetrics(string fileName, System.DateTime gameEndTime, List<AbstractMetric> metrics) {
        using (StreamWriter file = File.CreateText(fileName)) {
            JsonTextWriter writer = new JsonTextWriter(file);
            writer.Formatting = Formatting.Indented;
            JObject json = new JObject();
            json["game"] = new JValue(gameName);
            json["startTime"] = new JValue(gameStartTime);
            json["endTime"] = new JValue(gameEndTime);

            JArray jsonMetrics = new JArray();
            foreach (AbstractMetric m in metrics) {
                jsonMetrics.Add(m.getJSON());
            }
            json["metrics"] = jsonMetrics;
            json.WriteTo(writer);
        }
    }

    void Start()
    {
        Debug.Log("test123");
        MetricJSONWriter mjw = new MetricJSONWriter("digger", System.DateTime.Now);

        ButtonPressingMetric myBPMetric = new ButtonPressingMetric();
        myBPMetric.startRecording();
        myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.R, true));
        myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.A, true));
        myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.S, false));
        myBPMetric.recordEvent(new ButtonPressingEvent(System.DateTime.Now, UnityEngine.KeyCode.R, true));
        
        List<AbstractMetric> myList = new List<AbstractMetric>();
        myList.Add(myBPMetric);
        
        mjw.logMetrics("test_out.json", System.DateTime.Now, myList);
    }
}