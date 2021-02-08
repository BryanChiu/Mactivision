using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionEvent : AbstractMetricEvent {

    public List<List<Vector2>> positions { get; }

    public PositionEvent(System.DateTime eventTime, List<Vector2> position) : base(eventTime) {
        this.positions.Add(position);
    }
}
