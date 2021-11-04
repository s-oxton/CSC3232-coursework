using UnityEngine;
using TMPro;
using System.Collections;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textPro;
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private FadeOut fadeOut;

    public void ChangeLevelName(int levelNumber)
    {
        //set the text to the thing in the array depending on the current pin
        textPro.text = levelManager.GetLevelNames()[levelNumber];
    }
    

}
