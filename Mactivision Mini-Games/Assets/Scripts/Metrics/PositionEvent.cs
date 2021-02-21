using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

// PositionEvent class: designed to be consumed by PositionMetric class.
public class PositionEvent : AbstractMetricEvent {

    // List of Vector2 Objects representing the positions of multiple GameObjects on the game screen
    public List<Vector2> positions { get; }

    public PositionEvent(System.DateTime eventTime, List<Vector2> positions) : base(eventTime) {
        this.positions = positions;
    }
}
