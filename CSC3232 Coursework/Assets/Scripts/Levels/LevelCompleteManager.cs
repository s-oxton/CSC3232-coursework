using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{

    [SerializeField]
    private GameObject completedMenu; 

    //when the player finishes the level
    public void FinishLevel()
    {
        completedMenu.SetActive(true);
    }


}
