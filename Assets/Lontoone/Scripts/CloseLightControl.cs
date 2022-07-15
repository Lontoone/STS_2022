using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLightControl : MonoBehaviour
{
    public GameObject player;
    public float openDistance = 5;
    public int lightMaxCount = 5;
    private Light[] lights;

    WaitForFixedUpdate wait = new WaitForFixedUpdate();
    private IEnumerator Start()
    {

        lights = FindObjectsOfType<Light>();
        while (true) {
            int _count = 0;
            for (int i=0;i< lights.Length; i++) {
                float distance = Vector3.Distance(lights[i].gameObject.transform.position , player.transform.position);
                if (_count <lightMaxCount && distance < openDistance)
                {
                    lights[i].gameObject.SetActive(true);
                    _count++;
                }
                else {
                    lights[i].gameObject.SetActive(false);
                }
            }
            yield return wait;
        }
    }
}
