using UnityEngine;
using System;

public class PositionEvent : AbstractMetricEvent {

    public List<vector2> Positions { get; }

    public PositionEvent(System.DateTime eventTime, vector2 position) : base(eventTime) {
        this.positions.Add(position);
    }
}
