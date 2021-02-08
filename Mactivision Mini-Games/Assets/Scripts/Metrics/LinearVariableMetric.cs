using Newtonsoft.Json.Linq;

public class LinearVariableMetric : AbstractMetric<LinearVariableEvent> {

    public LinearVariableMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["currentValue"] = JToken.FromObject(this.currentValue);
        json["valueChange"] = JToken.FromObject(this.valueChange);
        json["initialValue"] = JToken.FromObject(this.reasonIndex);
        return json;
    }
}