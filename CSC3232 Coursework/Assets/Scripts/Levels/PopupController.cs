using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;

    //updates the text to the correct one depending on the code
    public void UpdateText(string newText)
    {
        text.SetText(newText);
    }

    public void ClearText()
    {
        text.SetText("");
    }

}
