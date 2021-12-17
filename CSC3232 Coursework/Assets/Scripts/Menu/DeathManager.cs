using UnityEngine;

public class DeathManager : MonoBehaviour
{

    [SerializeField]
    private GameObject deadMenu;

    //when the player dies
    public void PlayerDeath()
    {

        deadMenu.SetActive(true);

    }

}
