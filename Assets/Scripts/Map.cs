public class Map {
    public string[][] tiles { get; set; }
    public MapObject[] objects { get; set; }
    public int[] startTile { get; set; }

    public class MapObject {
        public string kind { get; set; }
        public float[] pos { get; set; }
        public int rotate { get; set; }
        public float height { get; set; }
        public bool optional { get; set; }
    }

    public int sizeX() => tiles[0].Length;
    public int sizeY() => tiles.Length;
    public string[] getStartTile() => tiles[startTile[1]][startTile[0]].Split('/');
    public bool isDrivable(int x, int y) => tiles[y][x].IndexOf('/') > 0;
    public int[] findDrivable() {
        for (int y = 0; y < sizeY(); y++)
            for (int x = 0; x < sizeX(); x++)
                if (isDrivable(x, y))
                    return new int[] {x, y};

        return new int[] {0, 0};
    }
}
