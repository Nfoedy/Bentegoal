using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKick : MonoBehaviour
{
    [Header("References")]
    // Campo davanti alla telecamera dove spawna la palla
    [SerializeField] private Transform ballHolder;
    // Campo del Prefab della palla da calciare 
    [SerializeField] private Rigidbody ballPrefab;  

    [Header("Kick Settings")]
    // Campo per impostare la forza del calcio
    [SerializeField] private float kickForce = 15f;
    // Campo per impostare la forza verso l'alto
    [SerializeField] private float upwardForce = 1.0f;

    [Header("Spawn Timing")]
    // Campo per impostare il ritardo iniziale prima di spawnare la palla
    [SerializeField] private float initialSpawnDelay = 0f;
    // Campo per impostare il ritardo prima di far respawnare la palla
    [SerializeField] private float respawnDelay = 1.0f;

    // Riferimento alla palla attualmente pronta per essere calciata
    private Rigidbody currentBall;
    // Flag per controllare se il giocatore può calciare
    private bool canKick = true;

    private void Start()
    {
        // Avvia una Coroutine per spawnare la palla dopo un ritardo iniziale
        StartCoroutine(SpawnBallAfterDelay(initialSpawnDelay));
    }

    private void Update()
    {
        // Se il giocatore ha appena calciato e non attende il respawn, esce dalla funzione
        if (!canKick) return;

        // Se il giocatore preme la barra
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Calcia la palla attuale
            KickCurrentBall();
            // Avvia una Coroutine per spawnare una nuova palla dopo un ritardo
            StartCoroutine(SpawnBallAfterDelay(respawnDelay));
        }
    }

    private void KickCurrentBall()
    {
        if (currentBall == null) return;

        canKick = false;

        // Stacca la palla dal ballHolder
        currentBall.transform.SetParent(null, true);

        // Attiva i campi di fisica
        currentBall.isKinematic = false;
        currentBall.useGravity = true;

        // Imposta la direzione e la forza del calcio
        Vector3 dir = ballHolder.forward;
        Vector3 force = (dir * kickForce) + (Vector3.up * upwardForce);

        // Applica la forza alla palla
        currentBall.AddForce(force, ForceMode.Impulse);

        // non è più “la palla pronta”
        currentBall = null;
    }

    // Coroutine per spawnare la palla dopo un ritardo specificato
    private IEnumerator SpawnBallAfterDelay(float delay)
    {
        // Attende il numero di secondi specificato
        yield return new WaitForSeconds(delay);

        // Check
        if (ballHolder == null || ballPrefab == null)
        {
            Debug.LogError("BallKick: assegna ballHolder e ballPrefab nell'Inspector.");
            yield break;
        }

        // Crea la nuova palla davanti alla telecamera 
        currentBall = Instantiate(ballPrefab);
        currentBall.transform.SetParent(ballHolder, false);
        currentBall.transform.localPosition = Vector3.zero;
        currentBall.transform.localRotation = Quaternion.identity;

        // La palla creata è ferma e visibile, pronta per essere calciata
        currentBall.isKinematic = true;
        currentBall.useGravity = false;
        currentBall.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Imposta il valore a true cosi il giocatore può calciare
        canKick = true;
    }
}

