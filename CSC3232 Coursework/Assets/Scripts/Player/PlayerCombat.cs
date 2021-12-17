using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private PlayerUIController playerUI;
    [SerializeField]
    private DeathManager deathManager;
    [SerializeField]
    private PlayerCollision playerCollision;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerAnimation playerAnimation;
    [SerializeField]
    private PlayerSounds playerSounds;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private GameObject bloodSplatter;

    [Header("Combat Details")]

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    private bool isHurt;

    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private AttackType[] attackDamages;

    private bool isAttacking;

    //to show the names of the attacks in the inspector to avoid confusion
    [System.Serializable]
    public class AttackType
    {
        public string attackName;
        public int attackDamage;
    }

    #region Getters and Setters

    public bool GetAttacking()
    {
        return isAttacking;
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;

    }public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool GetIsHurt()
    {
        return isHurt;
    }

    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
        isHurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            //if player is dead
            //tell the deathmanager to open the menu
            //pause the game
            if (currentHealth <= 0)
            {
                Invoke("PlayerDied", 1.5f);
            }

            //if the player is not already attacking and on the ground and inputting attack button and not crouching
            if (!isAttacking && Input.GetButtonDown("Fire1") && playerCollision.GetGrounded() && !playerMovement.GetCrouching() && !isHurt)
            {
                isAttacking = true;
                //if the player is moving, play animation for skid attacking
                if (Mathf.Abs(playerMovement.GetXVelocity()) > 10f)
                {
                    playerAnimation.SkidAttack();
                }
                //if player is not moving, play normal combat animation
                else
                {
                    playerAnimation.RegularAttack();
                }
            }
        }

    }

    public void PlayerDied()
    {
        deathManager.PlayerDeath();
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Taken " + damageAmount + " Damage!");
        if (currentHealth > 0)
        {
            //play hurt sound
            playerSounds.PlayHurt();
            //reduce the player's health
            currentHealth -= damageAmount;
            //update the ui
            playerUI.LoseHealth(currentHealth);
            //if health is less than/equal to 0, they are dead
            if (currentHealth <= 0)
            {
                playerAnimation.Dead();
                //shake the screen
                StartCoroutine(cameraShake.ShakeCamera(8));
            }
            //else they are just hurt
            else
            {
                isHurt = true;
                //make sure the player is not attacking still
                isAttacking = false;
                //shake the screen
                StartCoroutine(cameraShake.ShakeCamera(damageAmount));
            }
            //creates a blood splatter on the player.
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        }


    }

    //attack the enemy
    //called with events during the attack animations
    private void Attack(int damageType)
    {
        //damage all enemies in circle equal to the damage in teh array of the type
        int damage = attackDamages[damageType].attackDamage;

        //set enemy to be an enemy in the attack circle
        Collider2D enemy = Physics2D.OverlapCircle(attackPosition.position, attackRange, enemyLayer);
        //if there is an enemy in range
        if (enemy != null)
        {
            //if you're about to kill the enemy and not max hp
            if (currentHealth < maxHealth && enemy.GetComponent<EnemyStatTracker>().GetCurrentHealth() <= damage)
            {
                //heal for one
                playerUI.GainHealth(currentHealth, 1);
                currentHealth++;
            }
            enemy.GetComponent<EnemyStatTracker>().TakeDamage(damage);
            //shake the screen
            StartCoroutine(cameraShake.ShakeCamera(damage));
        }

    }

    //temp testing for seeing range of attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    //called at the end of the regular attack animation
    public void EndAttack()
    {
        isAttacking = false;
    }

    //called at the end of the hurt animation
    public void EndHurt()
    {
        isHurt = false;
    }

}
