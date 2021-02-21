using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

// PositionMetric class records PositionEvents which occur during a game.
public class PositionMetric : AbstractMetric<PositionEvent> {

    // List of the (arbitrary) names of objects to track positions of
    public List<string> gameObjectKeys { get; }

    public PositionMetric(List<string> gameObjectKeys) {
        this.gameObjectKeys = gameObjectKeys;
    }

    public override JObject getJSON() {
        JObject json = new JObject();

        json["metricName"] = JToken.FromObject("position");
        json["gameObjectKeys"] = JToken.FromObject(this.gameObjectKeys);
        json["eventList"] = JToken.FromObject(this.eventList);
        return json;
    }
}