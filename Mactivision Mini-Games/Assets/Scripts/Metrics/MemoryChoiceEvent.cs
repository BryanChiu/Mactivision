using System.Collections.Generic;
using UnityEngine;
using System;

// MemoryChoiceEvent class: designed to be consumed by MemoryChoiceMetric class.
public class MemoryChoiceEvent : AbstractMetricEvent {
    
    // Set of objects that are currently accepted (ie. in Feeder game, set of foods that the monster accepts)
    public List<string> objectsSet { get; }
    
    // Current object presented to the user (ie. in the Feeder game, current food user must decide to feed to the monster or not).
    public string _object { get; }

    // Choice that the user made on _object. If _object is a member of objectsSet, the user wants choice should be true.
    public bool choice { get; }

    // Time the user makes the choice. the inherited variable eventTime records the time the choice is presented to the user. 
    // Subtracting the two will give the time it took for the user to decide.
    public System.DateTime choiceTime { get; }
    
    public MemoryChoiceEvent(System.DateTime eventTime, List<string> objectsSet, string _object, bool choice, System.DateTime choiceTime) : base(eventTime) {
        if (choiceTime < eventTime) {
            throw new InvalidChoiceTimeException("MemoryChoiceEvent cannot be created: choiceTime cannot be earlier than eventTime");
        }

        this.objectsSet = objectsSet;
        this._object = _object;
        this.choice = choice;
        this.choiceTime = choiceTime;
    }
}


[Serializable]
public class InvalidChoiceTimeException : Exception {
    public InvalidChoiceTimeException() : base() { }
    public InvalidChoiceTimeException(string message) : base(message) { }
    public InvalidChoiceTimeException(string message, Exception inner) : base(message, inner) { }
}