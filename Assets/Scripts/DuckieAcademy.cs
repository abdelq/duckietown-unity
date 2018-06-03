using UnityEngine;
using YamlDotNet.Serialization;

class DuckieAcademy : Academy {
    private Map map;
    public TextAsset mapAsset;

    public override void InitializeAcademy () {
        map = new Deserializer().Deserialize<Map>(mapAsset.text);

        var mapObject = GameObject.Find("Map");
        mapObject.GetComponentInChildren<MapTiles>()
                 .Instantiate(map.tiles);
        if (map.objects != null)
            mapObject.GetComponentInChildren<MapObjects>()
                     .Instantiate(map.objects);

        var cameraObject = GameObject.FindWithTag("MainCamera");
        cameraObject.transform.position = new Vector3(
            (float)map.tiles[0].Length/2, (float)map.tiles.Length/2, .5f);

        var duckieObject = GameObject.FindWithTag("Duckiebot");
        // TODO Randomly place duckiebot in a drivable tile y=0.1
    }
}
