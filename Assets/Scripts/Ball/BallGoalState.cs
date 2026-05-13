using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGoalState : MonoBehaviour
{
    // get = leggible da tutti
    // private set = modificabile solo da questa classe
    // false = quando la palla nasce, non ha ancora segnato
    public bool HasScored { get; private set; } = false;

    public void MarkScored()
    {
        HasScored = true;
    }
}
