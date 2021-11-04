using UnityEngine;

public class AxeCollision : MonoBehaviour
{

    [SerializeField]
    private PlayerCombat playerCombat;

    //if it hits the player, kill the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerCombat.TakeDamage(5);
        }
    }

}
