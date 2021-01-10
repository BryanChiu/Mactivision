using System.Collections.Generic;
using Newtonsoft.Json;

abstract class AbstractMetric {
    private bool isRecording;
    private List<AbstractMetricEvent> eventList;

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
        this.eventList.Add(metricEvent);
    }

    public abstract JsonToken getJSON();
}