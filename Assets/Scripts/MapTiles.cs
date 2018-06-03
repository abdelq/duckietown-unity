using System;
using UnityEngine;

class MapTiles : MonoBehaviour {
    private int width = 512, height = 512;
    public Texture2D[] asphalt, curveLeft, curveRight, empty, floor, fourWay,
                       grass, straight, threeWayLeft, threeWayRight;

    public void Instantiate (string[][] tiles) {
        int sizeX = tiles[0].Length, sizeY = tiles.Length;

        transform.position = new Vector3((float)sizeX/2, 0, (float)sizeY/2);
        transform.localScale = new Vector3(sizeX, sizeY, 1);

        var texture = new Texture2D(sizeX * width, sizeY * height);
        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                texture.SetPixels(x * width, y * height, width, height,
                                  GetPixels(tiles[y][x]));
            }
        }
        texture.Apply();

        GetComponent<MeshRenderer>().material.mainTexture = texture;
    }

    private Color[] GetPixels(string tile) {
        var pixels = new Color[width * height];

        var tileDetails = tile.Split('/'); // XXX
        switch (tileDetails[0]) {
            case "asphalt":
                pixels = asphalt.PickRandom().GetPixels();
                break;
            case "curve_left":
                pixels = curveLeft.PickRandom().GetPixels();
                if (tileDetails[1] == "N")
                    pixels = RotatePixels(pixels, true);
                else if (tileDetails[1] == "S")
                    pixels = RotatePixels(pixels, false);
                else if (tileDetails[1] == "W")
                    Array.Reverse(pixels);
                break;
            case "curve_right":
                pixels = curveRight.PickRandom().GetPixels();
                if (tileDetails[1] == "N")
                    pixels = RotatePixels(pixels, false);
                else if (tileDetails[1] == "S")
                    pixels = RotatePixels(pixels, true);
                else if (tileDetails[1] == "E") // XXX
                    Array.Reverse(pixels);
                break;
            case "empty":
                //pixels = empty.PickRandom().GetPixels();
                break;
            case "floor":
                pixels = floor.PickRandom().GetPixels();
                break;
            case "4way":
                pixels = fourWay.PickRandom().GetPixels();
                break;
            case "grass":
                pixels = grass.PickRandom().GetPixels();
                break;
            case "straight":
                pixels = straight.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, false);
                else if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, true);
                break;
            case "3way_left":
                pixels = threeWayLeft.PickRandom().GetPixels();
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                else if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, false);
                else if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, true);
                break;
            case "3way_right":
                pixels = threeWayRight.PickRandom().GetPixels();
                // XXX N or S or E or W
                break;
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
        if (clockwise)
            Array.Reverse(rotated);
        return rotated;
    }
}
