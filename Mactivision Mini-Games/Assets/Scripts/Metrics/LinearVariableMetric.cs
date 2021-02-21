using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class LinearVariableMetric : AbstractMetric<LinearVariableEvent> {

    // Minimum value this linear variable can have
    public float minValue { get; }

    // Maximum value this linear variable can have
    public float maxValue { get; }

    // Initial value of this linear variable
    public float initialValue { get; }

    // List of reasons for the value to change
    public List<string> reasons { get; }

    public LinearVariableMetric(float minValue, float maxValue, float initialValue, List<string> reasons) {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.initialValue = initialValue;
        this.reasons = reasons;
    }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["metricName"] = JToken.FromObject("linearVariable");
        json["minValue"] = JToken.FromObject(this.minValue);
        json["maxValue"] = JToken.FromObject(this.maxValue);
        json["initialValue"] = JToken.FromObject(this.initialValue);
        json["reasons"] = JToken.FromObject(this.reasons);
        json["eventList"] = JToken.FromObject(this.eventList);
        return json;
    }
}