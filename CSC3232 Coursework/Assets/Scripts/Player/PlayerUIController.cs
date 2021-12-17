using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerCombat playerCombat;
    [SerializeField]
    private GameObject healthPip;

    [Header("Variables")]
    [SerializeField]
    private Vector2 healthPipSpawnPoint;
    [SerializeField]
    private float healthPipSpacing;
    List<GameObject> healthPipList = new List<GameObject>();


    private void Start()
    {
        //spawn in a number of health pips equal to the max health.
        for (int i = 0; i < playerCombat.GetMaxHealth(); i++)
        {
            //instantiate a health pip and add it to the list of health pips, and set the parent to be the transform
            healthPipList.Add(Instantiate(healthPip, new Vector3(healthPipSpawnPoint.x + (i * healthPipSpacing), healthPipSpawnPoint.y, 0), Quaternion.identity, transform));
        }
    }

    public void LoseHealth(int health)
    {
        //for the amount of damage the player has taken, set each heart to be empty
        int maxHealth = playerCombat.GetMaxHealth();
        for (int i = 0; i < maxHealth - health; i++)
        {
            Debug.Log("Updating Health Pip");
            //sets the full health pip to be empty
            healthPipList[maxHealth - i - 1].GetComponent<HeartUpdater>().SetHeartEmpty();
        }

    }

    public void GainHealth(int currentHealth, int healthGain)
    {
        for (int i = 0; i < healthGain; i++)
        {
            Debug.Log("Updating Health Pip");
            //sets the full health pip to be full
            healthPipList[currentHealth + i].GetComponent<HeartUpdater>().SetHeartFull();

        }
    }

}
