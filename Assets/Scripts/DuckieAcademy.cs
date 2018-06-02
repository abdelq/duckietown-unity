using UnityEngine;
using YamlDotNet.Serialization;

class DuckieAcademy : Academy {
    public TextAsset mapAsset;
    private Map map;

    public override void InitializeAcademy () {
        map = new Deserializer().Deserialize<Map>(mapAsset.text);

        var mapGameObject = GameObject.Find("Map");
        mapGameObject.GetComponentInChildren<MapTiles>()
                     .Instantiate(map.tiles);
        if (map.objects != null)
            mapGameObject.GetComponentInChildren<MapObjects>()
                         .Instantiate(map.objects);

        // TODO Good main camera position of the map
        // TODO Good light
        //Monitor.Log(key, value, displayType, target);
        //https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Feature-Monitor.md
        // TODO Put duckiebot(s) in drivable tiles randomly
    }
}
