using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private VolumeControl musicController;
    [SerializeField]
    private UISoundController uISoundController;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the user presses escape
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                ChangePauseState(1);
                pauseMenu.SetActive(false);
            }
            else
            {
                ChangePauseState(0);
                pauseMenu.SetActive(true);
            }


        }
    }

    public void ChangePauseState(int timeScale)
    {
        //change the boolean that controls the pause state
        isPaused = !isPaused;
        //set the new timescale
        Time.timeScale = timeScale;
        //play sounds depending on if the game is paused or not.
        if (timeScale == 1)
        {
            musicController.ChangeMusicPauseState(true);
            uISoundController.PlayPlaySound();
        }
        else
        {
            musicController.ChangeMusicPauseState(false);
            uISoundController.PlayPauseSound();
        }
    }

}
