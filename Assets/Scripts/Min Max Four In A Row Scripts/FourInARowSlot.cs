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
        dropText.enabled = true;
    }

    public void StoppedLookingAt()
    {
        dropText.enabled = false;
    }

    public void InteractWith()
    {
        fourInARowGame.PlacePuck(position);
    }
}
