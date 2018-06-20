using MLAgents;
using UnityEngine;

public class MapRobots : MonoBehaviour {
    public GameObject prefab;

    public void Instantiate(Map map, bool randomize) {
        prefab.GetComponentInChildren<DuckieAgent>()
            .GiveBrain(GameObject.FindObjectOfType<Brain>());

        if (randomize) {
            do {
                map.startTile = new int[] {Random.Range(0, map.sizeX()),
                    Random.Range(0, map.sizeY())};
            } while (!map.isDrivable(map.startTile[0], map.startTile[1]));
        } else if (map.startTile == null) {
            map.startTile = map.findDrivable();
        }

        var position = new Vector3(map.startTile[0] + .5f, .1f, map.startTile[1] + .5f);
        var rotation = Quaternion.identity;
        /*if (map.getStartTile().Length < 2) {
          GameObject.Instantiate(prefab, position, rotation, transform);
          return;
          }*/

        switch (map.getStartTile()[1]) {
            case "N":
                position.x += .25f;
                position.z -= .25f;
                //rotation = Quaternion.identity;
                break;
            case "E":
                position.x -= .25f;
                position.z -= .25f;
                rotation = Quaternion.Euler(0, 90, 0);
                break;
            case "S":
                position.x -= .25f;
                position.z += .25f;
                rotation = Quaternion.Euler(0, 180, 0);
                break;
            case "W":
                position.x += .25f;
                position.z += .25f;
                rotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        GameObject.Instantiate(prefab, position, rotation, transform);
    }
}
