using UnityEngine;

public class TriggerTextUpdate : MonoBehaviour
{

    [SerializeField]
    private PopupController popupController;

    [TextArea(minLines: 1, maxLines: 4)]
    [SerializeField]
    private string text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            popupController.UpdateText(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        popupController.ClearText();
    }

}
