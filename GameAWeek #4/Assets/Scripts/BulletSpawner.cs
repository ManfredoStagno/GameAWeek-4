using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public int spawnTimer = 25;

    private int counter;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        counter = spawnTimer;
    }

    private void FixedUpdate()
    {

    }

    public void ShootBullets()
    {
        counter--;
        if (counter == 0)
        {
            objectPooler.SpawnFromPool("Bullet", transform.position, Quaternion.Euler(transform.eulerAngles));
            counter = spawnTimer;
        }
    }
}
