using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class MapLoader : MonoBehaviour {
    void Awake () {
        var filePath = Application.dataPath + "/Maps/" + "udem1.yaml"; // XXX
        using (var reader = new StreamReader(filePath)) {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new UnderscoredNamingConvention())
                .Build();
            var map = deserializer.Deserialize<Map>(reader.ReadToEnd());
        }
    }

    public class Map
    {
        public string[][] Tiles { get; set; }
        public MapObject[] Objects { get; set; }

        public class MapObject {
            public string MeshFile { get; set; }
            [YamlMember(Alias = "pos")]
            public float[] Position { get; set; }
            public float Height { get; set; }
            public int Rotate { get; set; }
        }
    }
}
