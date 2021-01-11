using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class AbstractMetric {
    public  bool isRecording { protected set; get; }
    public List<AbstractMetricEvent> eventList { get; }

    protected AbstractMetric() {
        this.isRecording = false;
        this.eventList = new List<AbstractMetricEvent>();
    }

    public void startRecording() {
        this.isRecording = true;
    }

    public void finishRecording() {
        this.isRecording = false;
    }

    public void recordEvent(AbstractMetricEvent metricEvent) {
        if (this.isRecording) {
            this.eventList.Add(metricEvent);
        }
    }

    public abstract JObject getJSON();
}