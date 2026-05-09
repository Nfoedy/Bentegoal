using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ballHolder;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Camera customCamera;

    [Header("Timing")]
    [SerializeField] private float respawnDelay = 1f;

    private GameObject currentBall;

    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        if (ballHolder == null || ballPrefab == null)
        {
            Debug.LogError("BallSpawner: assegna ballHolder e ballPrefab nell'Inspector.");
            return;
        }

        currentBall = Instantiate(ballPrefab);
        currentBall.transform.SetParent(ballHolder, false);
        currentBall.transform.localPosition = Vector3.zero;
        currentBall.transform.localRotation = Quaternion.identity;

        BallDragThrow drag = currentBall.GetComponent<BallDragThrow>();

        if (drag != null)
        {
            Camera camToUse = customCamera != null ? customCamera : Camera.main;
            drag.Setup(camToUse, this);
        }
    }

    public void RequestRespawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnBall();
    }
}