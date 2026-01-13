using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGolState : MonoBehaviour
{
    public bool HasScored { get; private set; } = false;


    public void MarkScored()
    {
        HasScored = true;
    }
}
