using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform spawnPoint;      // Punto di spawn della palla
    [SerializeField] private Rigidbody ballPrefab;        // Prefab della palla da calciare

    [Header("Kick Settings")]
    [SerializeField] private float cooldown = 0.25f;          // Tempo di cooldown tra i tiri
    [SerializeField] private float shootForce = 12f;         // Forza con cui calciare la palla
    [SerializeField] private float upwardForce = 1.2f;   // Forza verso l'alto applicata alla palla

    private float nextShotTime = 0f;

    private void Update()
    {
        if (Time.time < nextShotTime) return;

        // Premi space per calciare
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
            nextShotTime = Time.time + cooldown;
        }
    }


    private void ShootBall()
    {
        if (spawnPoint == null || ballPrefab == null)
        {
            Debug.LogError("Assegnare la palla nell'inspector!");
            return;

        }

        // Istanzia la palla al punto di spawn
        Rigidbody ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        // Impulso nella direzione della camera
        Vector3 dir = spawnPoint.forward;
        Vector3 force = (dir * shootForce) + (Vector3.up * upwardForce);

        ball.AddForce(force, ForceMode.Impulse);

        // Logica per calciare la palla
        Debug.Log("Palla calciata!");
    }
}
