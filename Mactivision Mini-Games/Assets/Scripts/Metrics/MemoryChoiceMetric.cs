using Newtonsoft.Json.Linq;

// MemoryChoiceMetric class records MemoryChoiceEvents which occur during a game.
public class MemoryChoiceMetric : AbstractMetric<MemoryChoiceEvent> {

    public MemoryChoiceMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        // all json data from this metric should be stored under "memoryChoice"
        json["memoryChoice"] = JToken.FromObject(this.eventList);
        return json;
    }
}