using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    Vector3 startPos;
   
    void Start()
    {
        startPos = transform.position;
    }

    private void OnBecameInvisible()
    {
        transform.position += new Vector3(-Mathf.Sign(transform.position.x) * 2 * Mathf.Abs(transform.position.x), 0, startPos.z);
        transform.rotation = Random.rotation;
    }
}
