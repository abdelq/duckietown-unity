using UnityEngine;

public class MapLights : MonoBehaviour {
	public GameObject prefab;

	public void Instantiate (string[][] tiles, bool randomize) {
		if (randomize)
			prefab.GetComponentInChildren<TrafficLight>().frequency += Random.Range(0, 4);

		for (int y = 0; y < tiles.Length; y++)
    		for (int x = 0; x < tiles[y].Length; x++)
				if (tiles[y][x] == "4way")
					GameObject.Instantiate(prefab,
						new Vector3(x + .5f, .3f, y + .5f),
						Quaternion.Euler(-90, 0, 0),
						transform);
	}
}
