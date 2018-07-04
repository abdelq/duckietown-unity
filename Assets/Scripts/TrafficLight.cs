using System.Collections;
using UnityEngine;

public class TrafficLight : MonoBehaviour {
    public int frequency = 5;
    public Light[] lights1; // XXX
    public Light[] lights2; // XXX

    void Start () {
        StartCoroutine(FirstPattern());
    }

    IEnumerator FirstPattern() {
        foreach (var light in lights1)
            light.color = new Color(0, 1, 0, 1);
        foreach (var light in lights2)
            light.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(frequency);
        StartCoroutine(SecondPattern());
    }

    IEnumerator SecondPattern() {
        foreach (var light in lights2)
            light.color = new Color(0, 1, 0, 1);
        foreach (var light in lights1)
            light.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(frequency);
        StartCoroutine(FirstPattern());
    }
}
