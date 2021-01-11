using Newtonsoft.Json.Linq;

// ButtonPressingMetric class records ButtonPressingEvents which occur during a game.
public class ButtonPressingMetric : AbstractMetric<ButtonPressingEvent> {

    public ButtonPressingMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        // all json data from this metric should be stored under "buttonPressing"
        json["buttonPressing"] = JToken.FromObject(this.eventList);
        return json;
    }
}