using UnityEngine;

public class EnemyStatTracker : MonoBehaviour
{

    [SerializeField]
    private GameObject bloodSplatter;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    private bool hittable;
    private bool isHit;
    private bool isAttacking;

    #region getters/setters
    public void EndHit()
    {
        isHit = false;
    }

    public bool GetIsHit()
    {
        return isHit;
    }

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
    }

    public void SetHittable(bool tempHittable)
    {
        hittable = tempHittable;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hittable = true;

    }

    //when the monster takes damage
    public void TakeDamage(int damageAmount)
    {
        //if the monster is hittable (not doing an uncancelable animation)
        if (hittable)
        {
            //stop the attack animation
            isHit = true;
            isAttacking = false;
        }
        //always reduce the health of the monster
        currentHealth -= damageAmount;

        //creates a blood splatter
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);

    }

    //hittable is false when the combat animation is active
    private void UpdateHittable(int tempHittable)
    {
        //if hittable is 0 then the monster can't be hit
        if (tempHittable == 0)
        {
            hittable = false;
        }
        else
        {
            hittable = true;
        }

    }

}
