using Newtonsoft.Json.Linq;

public class PositionMetric : AbstractMetric<PositionEvent> {

    public PositionMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["positions"] = JToken.FromObject(this.eventList);
        return json;
    }
}