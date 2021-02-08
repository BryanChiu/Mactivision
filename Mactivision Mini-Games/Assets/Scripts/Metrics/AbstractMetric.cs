using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public interface AbstractMetric { 
    void startRecording();
    void finishRecording();
    JObject getJSON();
}

public abstract class AbstractMetric<T> : AbstractMetric where T : AbstractMetricEvent {
    public  bool isRecording { protected set; get; }
    public List<T> eventList { get; }

    protected AbstractMetric() {
        this.isRecording = false;
        this.eventList = new List<T>();
    }

    public void startRecording() {
        this.isRecording = true;
    }

    public void finishRecording() {
        this.isRecording = false;
    }

    public void recordEvent(T metricEvent) {
        if (this.isRecording && metricEvent != null) {
            this.eventList.Add(metricEvent);
        }
    }
    
    public abstract JObject getJSON();

}