using UnityEngine;

public class WeightTrigger : MonoBehaviour
{
    [SerializeField]
    private PopupController tutorialController;
    [SerializeField]
    private PlayerMovement playerMovement;

    [TextArea(minLines: 1, maxLines: 4)]
    [SerializeField]
    private string text;

    [SerializeField]
    private float newWeight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            tutorialController.UpdateText(text);
            playerMovement.UpdateWeight(newWeight);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tutorialController.ClearText();
        playerMovement.ResetWeight();
    }
}
