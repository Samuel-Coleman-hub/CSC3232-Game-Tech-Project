using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FourInARowSlot : MonoBehaviour
{
    [SerializeField] FourInARow fourInARowGame;
    [SerializeField] TextMeshProUGUI dropText;
    [SerializeField] int position;

    public void OnChildLookAt()
    {
        //Enabled selected effect
        Debug.Log("LOOKING AT " + gameObject.name);
        dropText.enabled = true;
    }

    public void StoppedLookingAt()
    {
        dropText.enabled = false;
    }

    public void InteractWith()
    {
        Debug.Log("Interact AT "+ gameObject.name);
        fourInARowGame.PlacePuck(position);
    }
}
