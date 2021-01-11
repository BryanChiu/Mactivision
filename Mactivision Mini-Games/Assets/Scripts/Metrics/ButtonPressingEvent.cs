using UnityEngine;

// ButtonPressingEvent: designed to be consumed by ButtonPressingMetric class.
public class ButtonPressingEvent : AbstractMetricEvent {

    // keyCode stores the key pressed by the user for a given event
    public  KeyCode keyCode { get; }
    
    // If keyDown is true: button was pressed in this event. 
    // If keyDown is false: button was released in this event.
    public bool keyDown { get; }

    public ButtonPressingEvent(System.DateTime eventTime, KeyCode keyCode, bool keyDown) : base(eventTime) {
        this.keyCode = keyCode;
        this.keyDown = keyDown;
    }
}