using UnityEngine;

class MapObjects : MonoBehaviour {
    public GameObject[] barriers, buildings, buses, cones, duckies, houses,
           signs, trees, trucks;

    public void Instantiate (Map.MapObject[] objects, bool randomize) {
        foreach (var obj in objects) {
            GameObject original;
            switch (obj.kind) {
                case "barrier":
                    original = randomize ? barriers.PickRandom() : barriers[0];
                    break;
                case "building":
                    original = randomize ? buildings.PickRandom() : buildings[0];
                    break;
                case "bus":
                    original = randomize ? buses.PickRandom() : buses[0];
                    break;
                case "cone":
                    original = randomize ? cones.PickRandom() : cones[0];
                    break;
                case "duckie":
                    original = randomize ? duckies.PickRandom() : duckies[0];
                    break;
                case "house":
                    original = randomize ? houses.PickRandom() : houses[0];
                    break;
                case "sign_blank":
                    original = randomize ? signs.PickRandom() : signs[0];
                    break;
                case "tree":
                    original = randomize ? trees.PickRandom() : trees[0];
                    break;
                case "truck":
                    original = randomize ? trucks.PickRandom() : trucks[0];
                    break;
                default:
                    Debug.LogError($"Unknown map object: {obj.kind}");
                    continue;
            }

            //0.61cm -> height of 1
            //obj.height -> desired height
            // XXX
            var desiredHeight = obj.height/.61f;
            var objectHeight = original.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.y;
            var ratio = desiredHeight/objectHeight;
            //Debug.Log(obj.kind + ": " + original.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.y);
            original.transform.localScale = new Vector3(ratio, ratio, ratio);

            var position = new Vector3(obj.pos[0], 0, obj.pos[1]);
            var rotation = Quaternion.Euler(0, obj.rotate, 0);

            GameObject gameObj = null;
            if (randomize && obj.optional) {
                if (Random.Range(0, 2) == 1)
                    gameObj = GameObject.Instantiate(original, position, rotation,
                            GetComponentInParent<Transform>());
            } else {
                gameObj = GameObject.Instantiate(original, position, rotation,
                        GetComponentInParent<Transform>());
            }

            /*if (gameObj != null) {
              gameObj.transform.localScale /= 4;
              gameObj.transform.localScale *= obj.height/0.61f;
              }*/
        }
    }
}
