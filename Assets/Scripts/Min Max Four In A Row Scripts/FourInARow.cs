using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourInARow : MonoBehaviour
{
    [SerializeField] GameObject puckPrefab;
    [SerializeField] List<Transform> columnPositions = new List<Transform>();

    private void Awake()
    {

    }

    public void PlacePuck(int position)
    {
        Debug.Log("DROPPING AT POSITION " + position);
        GameObject puck = Instantiate(puckPrefab, columnPositions[position]);
        puck.transform.localScale = new Vector3(1, 1, 1);
    }
}
