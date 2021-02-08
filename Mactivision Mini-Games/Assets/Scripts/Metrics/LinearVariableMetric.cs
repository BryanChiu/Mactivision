using Newtonsoft.Json.Linq;

public class LinearVariableMetric : AbstractMetric<LinearVariableEvent> {

    public LinearVariableMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["currentValue"] = JToken.FromObject(eventList[0]);
        json["valueChange"] = JToken.FromObject(eventList[1]);
        json["valueChange"] = JToken.FromObject(eventList[2]);
        return json;
    }
}