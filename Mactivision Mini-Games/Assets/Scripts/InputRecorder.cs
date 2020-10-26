using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This class records (keyboard) inputs and outputs them to a JSON file
// "Time start" :
// "Time end" :
// "Events" : 
// [
//  "TimeStamp" :
//  "Key" :
//  "Value" :
// ]
public class InputRecorder
{
    string outputPath; // path to output log file
    StreamWriter writer;
    bool recording;

    List<(float TimeStamp, KeyCode Key, bool Value)> keyEvents; // list of input events, represented as a tuple
    float startTime;
    float endTime;

    // Constructor
    public InputRecorder() {
        outputPath = "Logs/" + System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".json";
        writer = new StreamWriter(outputPath, true);
        recording = false;

        keyEvents = new List<(float, KeyCode, bool)>();
    }

    // Start the recording
    // Write the start time 
    public void StartRec() {
        if (recording) return;
        writer.WriteLine("{");
        writer.WriteLine("\"Time start\" : \"" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\",");

        startTime = Time.time;
        recording = true;
        Debug.Log("Input recording started");
    }

    // End the recording
    // Write the end time, and write each event
    // End the json structure, close the file
    public void EndRec() {
        if (!recording) return;
        writer.WriteLine("\"Time end\" : \"" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\",");
        writer.WriteLine("\"Events\" : ");
        writer.WriteLine("[");
        keyEvents.ForEach(LogToFile); // for each event, write to file
        writer.WriteLine("]");
        writer.WriteLine("}");
        writer.Close();

        endTime = Time.time;
        recording = false;
        Debug.Log("Input recording ended");
        Debug.Log("Log file output to " + outputPath);
    }

    // Add an event to the list
    public void AddEvent(KeyCode key, bool val) {
        if (!recording) return;
        keyEvents.Add((Time.time, key, val));
    }

    // Write an event using proper JSON formatting
    private void LogToFile((float time, KeyCode key, bool val) e) {
        writer.WriteLine("{ \"TimeStamp\" : " + System.String.Format("{0:0.000}", e.time) + ", \"Key\" : \"" + e.key + "\", \"Value\" : " + e.val.ToString().ToLower() + " },");
    }
}
