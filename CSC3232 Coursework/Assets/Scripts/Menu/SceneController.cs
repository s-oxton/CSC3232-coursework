using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private int worldMapIndex;

    public void RestartLevel()
    {
        //loads the current scene in again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitLevel()
    {
        //load the world map
        SceneManager.LoadScene(worldMapIndex);
    }

}
