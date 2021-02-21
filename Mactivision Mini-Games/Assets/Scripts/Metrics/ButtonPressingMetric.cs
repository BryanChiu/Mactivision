using Newtonsoft.Json.Linq;

// ButtonPressingMetric class records ButtonPressingEvents which occur during a game.
public class ButtonPressingMetric : AbstractMetric<ButtonPressingEvent> {

    public ButtonPressingMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["metricName"] = JToken.FromObject("buttonPressing");
        json["eventList"] = JToken.FromObject(this.eventList);
        return json;
    }
}