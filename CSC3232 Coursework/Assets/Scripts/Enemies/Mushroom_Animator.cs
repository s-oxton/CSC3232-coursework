using UnityEngine;

public class Mushroom_Animator : MonoBehaviour
{

    [SerializeField]
    private DumbMovement dumbMovement;
    [SerializeField]
    private Animator animator;

    private int mushroomOrientation;

    private void Start()
    {
        mushroomOrientation = dumbMovement.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {

        if (mushroomOrientation != dumbMovement.GetDirection())
        {
            mushroomOrientation = dumbMovement.GetDirection();
            SetXOrientation();
        }

        if (dumbMovement.GetIsMoving())
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }

    //flip the x orientation of the mushroom
    private void SetXOrientation()
    { 
        Vector2 tempVector = transform.localScale;

        if (mushroomOrientation == 1)
        {
            tempVector.x = Mathf.Abs(tempVector.x);
        }
        else if (mushroomOrientation == -1)
        {
            tempVector.x = -Mathf.Abs(tempVector.x);
        }

        transform.localScale = tempVector;
    }

}
