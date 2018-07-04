using MLAgents;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

class DuckieAcademy : Academy {
    private Map map;
    public TextAsset mapAsset;
    public bool randomize;

    public override void InitializeAcademy () {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new UnderscoredNamingConvention())
            .Build();
        map = deserializer.Deserialize<Map>(mapAsset.text);

        var mapObject = GameObject.Find("Map");
        mapObject.GetComponentInChildren<MapTiles>()
                 .Instantiate(map.tiles, randomize);
        if (map.objects != null)
            mapObject.GetComponentInChildren<MapObjects>()
                     .Instantiate(map.objects, randomize);
        mapObject.GetComponentInChildren<MapRobots>()
                 .Instantiate(map, randomize);
        mapObject.GetComponentInChildren<MapLights>()
                 .Instantiate(map.tiles, randomize);

        // TODO Intersection Signs

        GameObject.FindWithTag("MainCamera").transform.position = new Vector3(
            (float)map.sizeX()/2, (float)map.sizeY()/2, 0); // XXX
        GameObject.FindWithTag("MainLight").transform.position = new Vector3(
            (float)map.sizeX()/2, 1, (float)map.sizeY()/2);
    }
}
