using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{

    public Transform player;

    void FixedUpdate()
    {
        transform.position = player.position;
    }
}
