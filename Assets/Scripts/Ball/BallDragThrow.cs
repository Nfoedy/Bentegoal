using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BallDragThrow : MonoBehaviour
{

    [Header("Drag -> Force")]
    [SerializeField] private float maxDragPixels = 250f;        // Per non sparare la palla sulla luna
    [SerializeField] private float forceMultiplier = 0.06f;     // bilancia la forza applicata
    [SerializeField] private float minForce = 4f;               // forza minima per il lancio

    [Header("Aim")]
    [SerializeField] private float sideFactor = 0.55f;          // bilancia la forza laterale
    [SerializeField] private float upFactor = 0.3f;             // bilancia la forza verso alto e basso


    private Rigidbody rb;               // Attiva fisica per lanciare
    private Camera cam;                 // per convertire il drag 2D in direzione 3D
    private BallSpawner spawner;        // per richiamare RequestRespawn dopo il lancio

    private Vector2 dragStart;          // punto di partenza del mouse
    private bool dragging;              // stato per evitare il doppio tiro
    private bool launched;


    // Prepara la palla per essere lanciata
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.isKinematic = true;    // disabilita fisica finché non viene lanciata
        rb.useGravity = false;

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }


    public void Setup(Camera camera, BallSpawner ballSpawner)
    {
        cam = camera;
        spawner = ballSpawner;
    }


    private void OnMouseDown()
    {
        if(launched) return;

        if(cam == null) cam = Camera.main;

        dragging = true;
        dragStart = Input.mousePosition;
    }


    private void OnMouseUp()
    {
        if(!dragging || launched) return;
        dragging = false;

        Vector2 dragEnd = Input.mousePosition;
        Vector2 drag = dragEnd - dragStart;

        // Limita il drag massimo
        Vector2 clamped = Vector2.ClampMagnitude(drag, maxDragPixels);

        float force = clamped.magnitude * forceMultiplier;
        if(force < minForce) return;

        Vector2 dir2D = clamped.normalized;

        Vector3 dirWorld = cam.transform.forward + 
            cam.transform.right * (dir2D.x * sideFactor) + 
            cam.transform.up * (dir2D.y * upFactor);

        Launch(dirWorld.normalized, force);
    }

    private void Launch(Vector3 direction, float force)
    {
        launched = true;

        // Stacca la palla dal bollHolder ma mantiene la posizione nel mondo
        transform.SetParent(null, true);

        rb.isKinematic = false;
        rb.useGravity = true;

        rb.AddForce(direction * force, ForceMode.VelocityChange);

        if(spawner != null)
        {
            spawner.RequestRespawn();
        }
    }
}
