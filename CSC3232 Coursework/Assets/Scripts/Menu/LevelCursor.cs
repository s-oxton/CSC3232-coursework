using UnityEngine;

public class LevelCursor : MonoBehaviour
{

    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private CanvasManager canvasManager;

    [SerializeField]
    private float maxSize;
    [SerializeField]
    private float minSize;
    [SerializeField]
    private Vector3 scaleChange;

    private int currentPin;
    private int levelPinCount;
    private Vector3[] levelPins;

    // Start is called before the first frame update
    void Start()
    {
        //get the number of pins, and initialise the array for the pin locations
        levelPinCount = levelManager.transform.childCount;
        levelPins = new Vector3[levelPinCount];
        //for each level pin, add its position to the array
        for (int i = 0; i < levelPinCount; i++)
        {
            levelPins[i] = levelManager.transform.GetChild(i).transform.position;
        }
        //set the cursor to the first level pin
        currentPin = 0;
        SetTransform();
        canvasManager.ChangeLevelName(currentPin);
    }

    // Update is called once per frame
    void Update()
    {
        //if object has reached the bounds of it's scale, flip the scale type
        if (transform.localScale.x < minSize || transform.localScale.x > maxSize)
        {
            scaleChange = -scaleChange;
        }
        transform.localScale += scaleChange;

        if (Input.GetKeyDown("right") && currentPin < levelPinCount - 1)
        {
            //move to the next level on the right, update currentpin and change text
            currentPin++;
            canvasManager.ChangeLevelName(currentPin);
            SetTransform();
        }
        else if (Input.GetKeyDown("left") && currentPin > 0)
        {
            //move to the next level on the left, update currentpin and change text
            currentPin--;
            canvasManager.ChangeLevelName(currentPin);
            SetTransform();
        } else if (Input.GetKeyDown("return"))
        {
            //tell the level manager to enter the level
            levelManager.EnterLevel(currentPin);
        }
    }

    //set the transform of the cursor to the new level
    private void SetTransform()
    {
        transform.position = levelPins[currentPin];
    }
}
