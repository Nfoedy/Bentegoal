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

        Debug.Log("Goal scored!");
        //GameManager.Instance.AddScore(pointsPerGol);

    }
}
