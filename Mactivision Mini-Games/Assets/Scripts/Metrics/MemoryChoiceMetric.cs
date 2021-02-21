using Newtonsoft.Json.Linq;

// MemoryChoiceMetric class records MemoryChoiceEvents which occur during a game.
public class MemoryChoiceMetric : AbstractMetric<MemoryChoiceEvent> {

    public MemoryChoiceMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["metricName"] = JToken.FromObject("memoryChoice");
        json["eventList"] = JToken.FromObject(this.eventList);
        return json;
    }
}