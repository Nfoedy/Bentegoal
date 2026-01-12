using System.Collections;
using UnityEngine;


public class BallSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ballHolder;
    [SerializeField] private GameObject ballPrefab;

    [Header("Settings")]
    [SerializeField] private float respawnDelay = 2f;

    private GameObject currentBall;


    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        if(ballHolder == null || ballPrefab == null)
        {
            Debug.LogWarning("BallHolder or BallPrefab is not assigned.");
            return;
        }

        currentBall = Instantiate(ballPrefab);

        currentBall.transform.SetParent(ballHolder, false);
        currentBall.transform.localPosition = Vector3.zero;
        currentBall.transform.localRotation = Quaternion.identity;

        var drag = currentBall.GetComponent<BallDragThrow>();
        if(drag != null)
        {
            drag.Setup(Camera.main, this);
        }

    }


    public void RequestRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    
    private IEnumerator RespawnCoroutine()
    {   
        // Yield = 
        yield return new WaitForSeconds(respawnDelay);
        SpawnBall();

    }
}