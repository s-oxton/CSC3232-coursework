using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
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
            } else
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
    }

}
