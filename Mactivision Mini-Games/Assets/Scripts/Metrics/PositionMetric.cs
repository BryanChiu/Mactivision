using Newtonsoft.Json.Linq;

public class PositionMetric : AbstractMetric<PositionEvent> {

    public PositionMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        // all json data from this metric should be stored under "memoryChoice"
        json["currentValue"] = JToken.FromObject(this.currentValue);
        json["valueChange"] = JToken.FromObject(this.valueChange);
        json["initialValue"] = JToken.FromObject(this.reasonIndex);
        return json;
    }
}