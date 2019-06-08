using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, GameManager.instance.playerSpeed * Time.deltaTime); //PLAYER SPEED
    }
}
