using System.Collections.Generic;
using UnityEngine;

public class PositionEvent : AbstractMetricEvent {
    
    public List<Vector2> positions { get; }
    PositionEvent(System.DateTime eventTime, List<Vector2> positions) : base(eventTime) {
        this.positions = positions;
    }
}