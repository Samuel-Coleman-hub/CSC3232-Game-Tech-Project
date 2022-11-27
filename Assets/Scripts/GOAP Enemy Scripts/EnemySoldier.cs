using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : GameAgent
{
    [SerializeField] public bool waveOver;
    private float timer = 5;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("isAtBase", 1, true);
        goals.Add(s1, 3);
 
    }

    private void Update()
    {
       
    }


}
