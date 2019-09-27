using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBase : AnimalBase
{
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
        SetAnimation("isWalking", true);
        SetAnimation("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
