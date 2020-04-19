using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerArea : Area
{
    public override void AreaEvent()
    {
        Debug.Log("Area event fired!");
    }
}
