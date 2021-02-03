using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        for (int i = 0; i <300; ++i) {
            Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(Random.Range(-35, 35), 0.45f, Random.Range(-35, 35)), Quaternion.identity).transform.Rotate(Random.Range(-10, 10), Random.Range(0, 4) * 90, Random.Range(-10, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
