using UnityEngine;

public class FinishDetection : MonoBehaviour
{

    [SerializeField]
    private LevelCompleteManager levelCompleter;
    [SerializeField]
    private PauseManager pauseManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player has contacted the level end
        if (collision.transform.tag == "Player")
        {
            //end the level
            levelCompleter.FinishLevel();
            //pause the game
            pauseManager.ChangePauseState(0);
        }
    }

}
