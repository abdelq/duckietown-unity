using UnityEngine;

public class DuckieAcademy : Academy {
    public override void InitializeAcademy () {
        // XXX
        var tileMap = (TileMap) GameObject.Find("Tile Map").GetComponent(typeof(TileMap));
        tileMap.Generate();
    }
}
