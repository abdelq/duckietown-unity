class Map {
    public string[][] tiles { get; set; }
    public MapObject[] objects { get; set; }

    public class MapObject {
        public string kind { get; set; }
        public float[] pos { get; set; }
        public int rotate { get; set; }
        public float height { get; set; }
        public bool optional { get; set; }
    }
}
