using UnityEngine;

class MapObjects : MonoBehaviour {
    public GameObject[] buses, duckies, houses, signs, trees, trucks;

    public void Instantiate (Map.MapObject[] objects) {
        foreach (var obj in objects) {
            GameObject original;
            switch (obj.kind)
            {
                //barrier
                //cone
                //building
                case "bus":        original = buses.PickRandom();   break;
                case "duckie":     original = duckies.PickRandom(); break;
                case "house":      original = houses.PickRandom();  break;
                case "sign_blank": original = signs.PickRandom();   break;
                case "tree":       original = trees.PickRandom();   break;
                case "truck":      original = trucks.PickRandom();  break;
                default:
                    Debug.LogError($"Unknown map object kind: {obj.kind}");
                    continue;
            }

            var position = new Vector3(obj.pos[0], 0, obj.pos[1]);
            var rotation = Quaternion.Euler(0, obj.rotate, 0);

            GameObject gameObj = GameObject.Instantiate(original, position,
                rotation, GetComponentInParent<Transform>());

            // XXX Temporary
            gameObj.transform.localScale /= 4;
            gameObj.transform.localScale *= obj.height/0.61f;

            // TODO obj.height and obj.optional
            // XXX Colliders?
        }
    }
}
