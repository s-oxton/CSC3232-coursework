using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField]
    private BossRoomTrigger bossTrigger;
    [SerializeField]
    private EnemyStatTracker statTracker;

    private bool bossDead;

    private void Start()
    {
        bossDead = false;
    }

    private void Update()
    {
        //when the boss dies, tell the level ender to end the level.
        if (statTracker.GetCurrentHealth() <= 0 && !bossDead)
        {
            bossDead = true;
            bossTrigger.EndLevel();
        }
    }

}
