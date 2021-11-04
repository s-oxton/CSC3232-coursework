
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    [SerializeField]
    private GhostMovement ghostMovement;

    [SerializeField]
    private PlayerCombat playerCombat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if it hits the player
        if (collision.transform.tag == "Player")
        {
            //kill the player
            playerCombat.TakeDamage(5);
            Debug.Log("Player ded");
            ghostMovement.SetPlayerHit(true);
        }
    }

}
