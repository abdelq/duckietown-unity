using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class MapLoader : MonoBehaviour {
    public TextAsset MapFile;

    /* Tiles */
    public Transform Linear;
    public Transform LinearStop;
    public Transform LinearStopLeft;
    public Transform LinearStopBoth;
    public Transform DiagonalLeft;
    public Transform DiagonalRight;
    public Transform ThreeWayLeft;
    public Transform Asphalt;
    public Transform Floor;
    public Transform Grass;

    void Awake () {
        var path = $"{Application.dataPath}/Maps/udem1.yaml";
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new UnderscoredNamingConvention())
            .Build();

        using (var reader = new StreamReader(path))
            Load(deserializer.Deserialize<Map>(reader.ReadToEnd()));
    }

    void Load (Map map) {
        var tiles = new Dictionary<string, Transform> {
            {"linear", Linear},
            {"linear_stop", LinearStop},
            {"linear_stop_left", LinearStopLeft},
            {"linear_stop_both", LinearStopBoth},
            {"diag_left", DiagonalLeft},
            {"diag_right", DiagonalRight},
            {"3way_left", ThreeWayLeft},
            {"asphalt", Asphalt},
            {"floor", Floor},
            {"grass", Grass},
        };
        var rotations = new Dictionary<string, Quaternion> {
            {"N", Quaternion.Euler(0, 180, 0)},
            {"E", Quaternion.Euler(0, 270, 0)},
            {"S", Quaternion.Euler(0, 0, 0)},
            {"W", Quaternion.Euler(0, 90, 0)},
        };

        for (int i = 0; i < map.Tiles.Length; i++) {
            for (int j = 0; j < map.Tiles[i].Length; j++) {
                var tile = map.Tiles[i][j].Split('/');
                Instantiate(tiles[tile[0]], new Vector3(10*j, 0, -10*i),
                    tile.Length > 1 ? rotations[tile[1]] : Quaternion.identity);
            }
        }
    }
}
