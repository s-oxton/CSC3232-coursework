using UnityEngine;

public class BossRoomBlockMovement : MonoBehaviour
{

    [SerializeField]
    private float moveAmount;

    //moves the obstacle down to stop the player leaving the room
    public void Block()
    {
        transform.position -= Vector3.up * moveAmount;
    }

    //moves the obstacle back up 
    public void UnBlock()
    {
        Destroy(gameObject);
    }

}
