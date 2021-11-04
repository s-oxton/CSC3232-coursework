using UnityEngine;

public class PlayerUIController : MonoBehaviour
{

    public void UpdateHealthPips(int health)
    {
        //set some pips to be disabled
        //the pips to be disabled are starting at 5, going down.

        //for 5 - current health
        //if full pip is enabled then:
        //set full pip 6-i to be disabled
        //enable empty pip 6-i

        for (int i = 0; i < Mathf.Min(5 - health, 5); i++)
        {
            Debug.Log("Updating Health Pip");
            //sets the full health pip to be inactive
            this.transform.Find("HealthPip"+(5-i)).gameObject.SetActive(false);
            //sets the full health pip to be active
            this.transform.Find("HealthPipEmpty"+(5-i)).gameObject.SetActive(true);
        }

    }

}
