using System;
using UnityEngine;

class MapTiles : MonoBehaviour {
    private int width = 512, height = 512;
    public Texture2D[] asphalt, curveLeft, curveRight, empty, floor, fourWay,
           grass, straight, threeWayLeft, threeWayRight;

    public void Instantiate (string[][] tiles, bool randomize) {
        int sizeX = tiles[0].Length, sizeY = tiles.Length;

        transform.position = new Vector3((float)sizeX/2, 0, (float)sizeY/2);
        transform.localScale = new Vector3(sizeX, sizeY, 1);

        var texture = new Texture2D(sizeX * width, sizeY * height);
        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                texture.SetPixels(x * width, y * height, width, height,
                        GetPixels(tiles[y][x], randomize));
            }
        }
        texture.Apply();

        GetComponent<MeshRenderer>().material.mainTexture = texture;
    }

    private Color[] GetPixels(string tile, bool randomize) {
        var pixels = new Color[width * height];

        var tileDetails = tile.Split('/');
        switch (tileDetails[0]) {
            case "asphalt":
                pixels = (randomize ? asphalt.PickRandom() : asphalt[0]).GetPixels();
                break;
            case "curve_left":
                pixels = (randomize ? curveLeft.PickRandom() : curveLeft[0]).GetPixels();
                break;
            case "curve_right":
                pixels = (randomize ? curveRight.PickRandom() : curveRight[0]).GetPixels();
                break;
            case "empty":
                pixels = (randomize ? empty.PickRandom() : empty[0]).GetPixels();
                break;
            case "floor":
                pixels = (randomize ? floor.PickRandom() : floor[0]).GetPixels();
                break;
            case "4way":
                pixels = (randomize ? fourWay.PickRandom() : fourWay[0]).GetPixels();
                break;
            case "grass":
                pixels = (randomize ? grass.PickRandom() : grass[0]).GetPixels();
                break;
            case "straight":
                pixels = (randomize ? straight.PickRandom() : straight[0]).GetPixels();
                break;
            case "3way_left":
                pixels = (randomize ? threeWayLeft.PickRandom() : threeWayLeft[0]).GetPixels();
                break;
            case "3way_right":
                pixels = (randomize ? threeWayRight.PickRandom() : threeWayRight[0]).GetPixels(); // XXX
                break;
        }

        if (tileDetails.Length > 1) {
            if (tileDetails[1] == "S")
                Array.Reverse(pixels);
            else if (tileDetails[1] == "E")
                pixels = RotatePixels(pixels, true);
            else if (tileDetails[1] == "W")
                pixels = RotatePixels(pixels, false);
        }

        return pixels;
    }

    private Color[] RotatePixels(Color[] original, bool clockwise) {
        var rotated = new Color[width * height];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                rotated[i*height + j] = original[(width - j - 1) * width + i]; // XXX
            }
        }
        if (!clockwise)
            Array.Reverse(rotated);
        return rotated;
    }
}
