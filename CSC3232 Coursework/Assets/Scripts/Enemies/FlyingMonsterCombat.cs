using UnityEngine;

public class FlyingMonsterCombat : MonoBehaviour
{

    [SerializeField]
    private EnemyStatTracker statTracker;
    [SerializeField]
    private PlayerCombat playerCombat;
    [SerializeField]
    private FlyingMonsterMovement movement;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private float attackCooldown;
    private float timer;


    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (movement.GetPlayerAttackable() && timer < 0)
        {
            //attack the player
            statTracker.SetAttacking(true);
            timer = attackCooldown;
        }
        if (timer >= -1)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        //deal damage to player
        Collider2D player = Physics2D.OverlapCircle(attackPosition.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    //temp testing for seeing range of attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

}
