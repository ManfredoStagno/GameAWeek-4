using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    void FixedUpdate()
    {
       objectPooler.SpawnFromPool("TestCube", transform.position, Quaternion.identity);
    }
}
