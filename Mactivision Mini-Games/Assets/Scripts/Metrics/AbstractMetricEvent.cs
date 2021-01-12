// AbstractMetricEvent is the base class of all metric events which are consumed by subclasses of AbstractMetric class
public abstract class AbstractMetricEvent {

    // All subclasses of AbstractMetricEvent have access to eventTime, which records the start time of a metric event.
    public System.DateTime eventTime { get; }

    protected AbstractMetricEvent(System.DateTime eventTime) {
        this.eventTime = eventTime;
    }
} 