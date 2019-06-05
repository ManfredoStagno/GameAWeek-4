using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject
{
    Rigidbody rb;

    public float maxTorque = 1;
    public float maxVelocity = 20;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawned()
    {
        //Randomize Torque
        Vector3 torque = new Vector3
            (Random.Range(-1, 1) * Random.Range(0f, maxTorque), 
            Random.Range(-1, 1) * Random.Range(0f, maxTorque), 
            Random.Range(-1, 1) * Random.Range(0f, maxTorque));

        //Randomize Velocity
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1) * Random.Range(0f, maxVelocity);
        GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Force);
    }
}
