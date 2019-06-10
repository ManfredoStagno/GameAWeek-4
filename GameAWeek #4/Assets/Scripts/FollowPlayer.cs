using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    float distance;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        distance = transform.position.z - gm.player.position.z;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, gm.player.position.z + distance); //PLAYER SPEED
    }
}
