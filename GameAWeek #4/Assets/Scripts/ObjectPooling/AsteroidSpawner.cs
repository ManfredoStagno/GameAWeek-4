using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public float radius = 15f;
    

    void Start()
    {
        objectPooler = ObjectPooler.instance;        
    }

    int counter = 50;
    void FixedUpdate()
    {
        Vector3 point = RandomPointOnSpwaner();

        counter--;
        if (counter == 0)
        {
            if (point.magnitude < radius)
            {
                objectPooler.SpawnFromPool("Asteroid", transform.position + point, Random.rotation);
            }
            counter = 50;            
        }
        
    }

    Vector3 RandomPointOnSpwaner()
    {
        return new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
