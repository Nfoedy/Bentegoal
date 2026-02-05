using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BallDragThrow : MonoBehaviour
{
    [Header("Drag -> Force")]
    [SerializeField] private float maxDragPixels = 250f;
    [SerializeField] private float forceMultiplier = 0.06f;
    [SerializeField] private float minForce = 4f;

    [Header("Aim")]
    [SerializeField] private float sideFactor = 0.55f;
    [SerializeField] private float upFactor = 0.35f;

    // Campi tecnici
    private Rigidbody rb;
    private Camera cam;
    private BallSpawner spawner;

    // Campi di stato
    private Vector2 dragStart;
    private bool dragging;
    private bool launched;


    // Prova per github desktop


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;

        // Per collisioni pi� accurate durante il lancio veloce
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Setup(Camera camera, BallSpawner ballSpawner)
    {
        cam = camera;
        spawner = ballSpawner;
    }

    private void OnMouseDown()
    {
        if (launched) return;

        if (cam == null) cam = Camera.main;

        dragging = true;
        dragStart = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (!dragging || launched) return;
        dragging = false;

        Vector2 dragEnd = Input.mousePosition;
        Vector2 drag = dragEnd - dragStart;
        
        // Potenza del lancio basata sulla lunghezza del drag, limitata a maxDragPixels.
        Vector2 clamped = Vector2.ClampMagnitude(drag, maxDragPixels);

        // Forza
        float force = clamped.magnitude * forceMultiplier;
        if (force < minForce) return;

        // Direzione
        Vector2 dir2D = clamped.normalized;

        // Trasforma il gesto 2D del mouse in una direzione 3D basata sulla camera
        Vector3 dirWorld =
            cam.transform.forward +
            cam.transform.right * (dir2D.x * sideFactor) +
            cam.transform.up * (dir2D.y * upFactor);

        Launch(dirWorld.normalized, force);
    }

    private void Launch(Vector3 direction, float force)
    {
        launched = true;

        transform.SetParent(null, true);

        rb.isKinematic = false;
        rb.useGravity = true;

        rb.AddForce(direction * force, ForceMode.Impulse);
        Destroy(gameObject, 60f); 

        if (spawner != null) spawner.RequestRespawn();

    }

}

