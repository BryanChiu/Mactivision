using UnityEngine;
using System;

// ButtonPressingEvent: designed to be consumed by ButtonPressingMetric class.
public class ButtonPressingEvent : AbstractMetricEvent {

    // keyCode stores the key pressed by the user for a given event
    public KeyCode keyCode { get; }
    
    // If keyDown is true: button was pressed in this event. 
    // If keyDown is false: button was released in this event.
    public bool keyDown { get; }

    public ButtonPressingEvent(System.DateTime eventTime, KeyCode keyCode, bool keyDown) : base(eventTime) {
        if (keyCode == KeyCode.None) {
            throw new InvalidKeyCodeException("ButtonPressingEvent cannot be created with KeyCode.None");
        }

        this.keyCode = keyCode;
        this.keyDown = keyDown;
    }
}

[Serializable]
public class InvalidKeyCodeException : Exception {
    public InvalidKeyCodeException() : base() { }
    public InvalidKeyCodeException(string message) : base(message) { }
    public InvalidKeyCodeException(string message, Exception inner) : base(message, inner) { }
}