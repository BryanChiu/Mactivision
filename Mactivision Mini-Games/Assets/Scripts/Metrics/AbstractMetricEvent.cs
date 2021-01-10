using System;

abstract class AbstractMetricEvent {

    private DateTime eventTime;

    protected AbstractMetricEvent(DateTime eventTime) {
        this.eventTime = eventTime;
    }

    public DateTime getEventTime() {
        return this.eventTime;
    }
} 