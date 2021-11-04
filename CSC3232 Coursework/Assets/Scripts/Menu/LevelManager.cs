using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private string[] levelNames;
    //leveloffset is the scene id of the first level
    [SerializeField]
    private int levelOffset;
    [SerializeField]
    private bool[] levelUnlocked;

    public string[] GetLevelNames()
    {
        return levelNames;
    }

    public void EnterLevel(int levelNumber)
    {
        //if level unlocked
        if (levelUnlocked[levelNumber])
        {
            //load the level
            Debug.Log("Loading level");
            SceneManager.LoadScene(levelOffset + levelNumber);
        }
        //else
        else
        {
            //make a popup
            Debug.Log("Level not unlocked!!!");
        }
        
    }

    private void Start()
    {
        //for each level
        for (int i = 0; i < transform.childCount; i++)
        {
            //if the level has been unlocked
            if (levelUnlocked[i])
            {
                //update the picture
                gameObject.transform.GetChild(i).GetComponent<UpdateLevelPin>().UpdatePin();
            }
        }
    }

}
