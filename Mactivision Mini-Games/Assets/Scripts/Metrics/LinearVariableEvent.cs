using UnityEngine;
using System;

public class LinearVariableEvent : AbstractMetricEvent {

    public long currentValue { get; }

    public long valueChange { get; }

    public int reasonIndex { get; }

    public LinearVariableEvent(System.DateTime eventTime, long currentValue, long valueChange, int reasonIndex) : base(eventTime) {
        this.currentValue = currentValue;
        this.valueChange = valueChange;
        this.reasonIndex = reasonIndex;
    }
}
