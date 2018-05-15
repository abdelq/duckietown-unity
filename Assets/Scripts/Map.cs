using YamlDotNet.Serialization;

public class Map {
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
