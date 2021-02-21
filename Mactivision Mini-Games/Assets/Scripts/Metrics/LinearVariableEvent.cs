using UnityEngine;
using System;

// LinearVariableEvent class: designed to be consumed by LinearVeriableMetric class.
public class LinearVariableEvent : AbstractMetricEvent {

    // Current value of the linear variable being tracked. This is the value after the change recorded by this event
    public float currentValue { get; }

    // How much the linear variable changed since the previous recorded event
    public float valueChange { get; }

    // Index of the reason for the change in the linear variable (List of reasons defined by the LinearVariableMetric class which consumes this event)
    public float reasonIndex { get; }

    public LinearVariableEvent(System.DateTime eventTime, float currentValue, float valueChange, int reasonIndex) : base(eventTime) {
        this.currentValue = currentValue;
        this.valueChange = valueChange;
        this.reasonIndex = reasonIndex;
    }
}
