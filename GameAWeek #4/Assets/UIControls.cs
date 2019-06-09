using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    ShipController player;
    Vector3 startScale;

    private void Start()
    {
        player = GameManager.instance.player.GetComponent<ShipController>();
        startScale = transform.localScale;
    }

    private void FixedUpdate()
    {

        float factor = player.remainingFuel / player.totalFuelTime;

        if (factor > 1)
            factor = 1;

        transform.localScale = new Vector3 (startScale.x * factor, startScale.y,startScale.z);
    }
}
