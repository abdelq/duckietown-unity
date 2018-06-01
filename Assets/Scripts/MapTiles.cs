using System;
using UnityEngine;

class MapTiles : MonoBehaviour {
    private int width = 512, height = 512;
    public Texture2D[] asphalt, diagonalLeft, diagonalRight, floor, grass,
           linear, linearStop, linearStopBoth, linearStopLeft,
           threeWayLeft, threeWayRight;

    public void Instantiate (string[][] tiles) {
        int sizeX = tiles[0].Length, sizeY = tiles.Length;

        transform.position = new Vector3((float)sizeX/2, 0, (float)sizeY/2);
        transform.localScale = new Vector3(sizeX, sizeY, 1);

        var texture = new Texture2D(sizeX * width, sizeY * height);
        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                texture.SetPixels(x * width, y * height, width, height,
                        GetTilePixels(tiles[y][x]));
            }
        }
        texture.Apply();

        GetComponent<MeshRenderer>().material.mainTexture = texture;

        // FIXME Might need to update the box collider too
        // https://docs.unity3d.com/ScriptReference/BoxCollider-size.html
        // https://answers.unity.com/questions/32618/changing-box-collider-size.html
    }

    private Color[] GetTilePixels(string tile) {
        var pixels = new Color[width * height];

        // FIXME Rotat e images so that you have a uniform if/else for rotation
        string[] tileDetails = tile.Split('/'); // XXX Rename
        switch (tileDetails[0])
        {
            case "asphalt":
                pixels = asphalt.PickRandom().GetPixels();
                break;
            case "diag_left":
                pixels = diagonalLeft.PickRandom().GetPixels();
                if (tileDetails[1] == "W")
                    Array.Reverse(pixels);
                if (tileDetails[1] == "N")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "S")
                    pixels = RotatePixels(pixels, false);
                break;
            case "diag_right":
                pixels = diagonalRight.PickRandom().GetPixels();
                if (tileDetails[1] == "N")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "S")
                    pixels = RotatePixels(pixels, true);
                // TODO West or East
                break;
            case "floor":
                pixels = floor.PickRandom().GetPixels();
                break;
            case "grass":
                pixels = grass.PickRandom().GetPixels();
                break;
            case "linear":
                pixels = linear.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                break;
            case "linear_stop":
                pixels = linearStop.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                break;
            case "linear_stop_both":
                pixels = linearStopBoth.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                break;
            case "linear_stop_left":
                pixels = linearStopLeft.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                break;
            case "3way_left":
                pixels = threeWayLeft.PickRandom().GetPixels();
                if (tileDetails[1] == "E")
                    pixels = RotatePixels(pixels, false);
                if (tileDetails[1] == "W")
                    pixels = RotatePixels(pixels, true);
                if (tileDetails[1] == "S")
                    Array.Reverse(pixels);
                break;
            case "3way_right":
                pixels = threeWayRight.PickRandom().GetPixels();
                // TODO All 3 out of 4 positions
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
