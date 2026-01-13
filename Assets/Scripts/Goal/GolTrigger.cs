using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GolTrigger: MonoBehaviour
{
    [SerializeField] private int pointsPerGol = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        var state = other.GetComponent<BallGolState>();
        if (state != null && state.HasScored) return;       // Ha già segnato, non fare nulla
        
        if (state != null)         {
            state.MarkScored();  // Segna che ha segnato
        }


        Debug.Log("Goal scored!");
        //GameManager.Instance.AddScore(pointsPerGol);

    }
}
