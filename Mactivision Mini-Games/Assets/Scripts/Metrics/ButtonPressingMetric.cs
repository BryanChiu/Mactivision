using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ButtonPressingMetric : AbstractMetric {

    public ButtonPressingMetric() { }

    public override JObject getJSON() {
        JObject json = new JObject();
        json["buttonPressing"] = JToken.FromObject(this.eventList);
        return json;
    }
}