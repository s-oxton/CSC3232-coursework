using UnityEngine;

public class SpinningBladeCombat : MonoBehaviour
{

    [SerializeField]
    private PlayerCombat playerCombat;

    [SerializeField]
    private float damageInterval;
    [SerializeField]
    private int bladeDamage;
    private bool playerInBlade;
    private float timer;

    private void Start()
    {
        playerInBlade = false;
        timer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerInBlade = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerInBlade = false;
        }
    }

    private void Update()
    {
        //timer is reduced if it is above 0
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        //if the player is in the blade (we know the timer must be below 0)
        else if (playerInBlade)
        {
            //take damage
            playerCombat.TakeDamage(bladeDamage);
            //reset the timer
            timer = damageInterval;
        }
    }

}
