using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void NewGame()
    {
        //load the world map
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        Debug.Log("load ze game :)");
    }

    public void QuitGame()
    {
        Debug.Log("quit the game :(");
        Application.Quit();
    }

}
