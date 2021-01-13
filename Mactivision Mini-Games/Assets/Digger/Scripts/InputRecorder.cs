using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This is used as an example of how input recording works.
// TODO: Shoudl we push this into game levelmanagers rather than having a seperate class?
public class InputRecorder
{
    MetricJSONWriter MetricWriter;
    ButtonPressingMetric MetricButton;
    List<AbstractMetric> Metrics;
     
    string GameName;

    // Constructor
    public InputRecorder()
    {
        GameName = Battery.Instance.GetGameName();

        // Write the metrics to JSON
        MetricWriter = new MetricJSONWriter(GameName, System.DateTime.Now);
        
        // Records the button pressing metrics.
        MetricButton = new ButtonPressingMetric();

        // Holds a list of different kind of Metrics Recorders (Button, etc.).
        Metrics = new List<AbstractMetric>();
    }

    // Start the recording
    public void StartRec()
    {
        MetricButton.startRecording();
        Debug.Log("Button recording started");
    }

    // End the recording
    public void EndRec()
    {
        if (MetricButton.isRecording)
        {   
            MetricButton.finishRecording();
            Debug.Log("Input recording ended");
        }
    }

    // Write Recording
    public void WriteRec()
    {
        // Add button recordings to metric list.
        Metrics.Add(MetricButton);

        // Write all the recordings from the list to a JSON file.
        MetricWriter.logMetrics(
                Battery.Instance.GetOutputPath() + GameName + ".json",
                System.DateTime.Now, Metrics); 
    }

    // Add an event to the list
    public void AddEvent(KeyCode key, bool val)
    {
        if (MetricButton.isRecording)
        {
            // Record a button pressing event.
            MetricButton.recordEvent(
                    new ButtonPressingEvent(System.DateTime.Now, key, val));
        }
    }
}
