using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gol : MonoBehaviour
{

    [SerializeField] private int pointsForGoal = 1;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Ball")) return;

        Debug.Log("Goal!");

        Destroy(other.gameObject);
    }
}
