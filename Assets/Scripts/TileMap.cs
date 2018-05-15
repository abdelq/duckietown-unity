using System;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer),
                  typeof(MeshCollider))]
public class TileMap : MonoBehaviour {
	private Map map;
	public TextAsset mapFile; // TODO Move to academy
	private int sizeX, sizeY;
	public float tileSize = 1;

	private Mesh mesh;

	private Texture2D texture;
	public int tileResolution = 512; // XXX Variable type
	public Texture2D[] textures;

	public void Generate () {
		GenerateMap();

		GenerateMesh();
		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;

		GenerateTexture();
		GetComponent<MeshRenderer>().material.mainTexture = texture;
	}

	private void GenerateMap() {
        var deserializer = new DeserializerBuilder()
			.WithNamingConvention(new UnderscoredNamingConvention())
            .Build();

		map = deserializer.Deserialize<Map>(mapFile.text);
		sizeY = map.Tiles.Length;
		if (sizeY > 0)
			sizeX = map.Tiles[0].Length;
	}

	// Source: catlikecoding.com/unity/tutorials/procedural-grid
    private void GenerateMesh() {
		// XXX Declare the arrays inside
		mesh = new Mesh {
			name = "Tile Map"
		};

		var vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
		var uv = new Vector2[vertices.Length];
		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {
				vertices[i] = new Vector3(x * tileSize, y * tileSize);
				uv[i] = new Vector2((float)x / sizeX, (float)y / sizeY);
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;

		var triangles = new int[sizeX * sizeY * 6];
		for (int ti = 0, vi = 0, y = 0; y < sizeY; y++, vi++) {
			for (int x = 0; x < sizeX; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + sizeX + 1;
				triangles[ti + 5] = vi + sizeX + 2;
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
    }

	// Source: quill18.com/unity_tutorials
	private void GenerateTexture() {
		texture = new Texture2D(sizeX * tileResolution, sizeY * tileResolution);

		for (int y = 0; y < sizeY; y++) {
			for (int x = 0; x < sizeX; x++) {
				texture.SetPixels(x * tileResolution, y * tileResolution,
								  tileResolution, tileResolution,
								  GetTilePixels(x, y));
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
	}

    private Color[] GetTilePixels(int x, int y)
    {
		string[] tile = map.Tiles[y][x].Split('/');

		// FIXME Expecting an array of 10 elements w/ proper placement
		switch (tile[0])
        {
            case "linear":           return textures[0].GetPixels();
            case "linear_stop":      return textures[1].GetPixels();
            case "linear_stop_left": return textures[2].GetPixels();
            case "linear_stop_both": return textures[3].GetPixels();
            case "diag_left":        return textures[4].GetPixels();
            case "diag_right":       return textures[5].GetPixels();
            case "3way_left":        return textures[6].GetPixels();
            case "asphalt":          return textures[7].GetPixels();
            case "floor":            return textures[8].GetPixels();
            case "grass":            return textures[9].GetPixels();
        }

		return new Color[tileResolution * tileResolution];
    }
}
