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

    private Rigidbody rb;
    private Camera cam;
    private BallSpawner spawner;

    private Vector2 dragStart;
    private bool dragging;
    private bool launched;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // palla pronta: non cade e non si muove
        rb.isKinematic = true;
        rb.useGravity = false;

        // migliore per alte velocità
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

        Vector2 clamped = Vector2.ClampMagnitude(drag, maxDragPixels);

        float force = clamped.magnitude * forceMultiplier;
        if (force < minForce) return;


        Vector2 dir2D = clamped.normalized;

        Vector3 dirWorld =
            cam.transform.forward +
            cam.transform.right * (dir2D.x * sideFactor) +
            cam.transform.up * (dir2D.y * upFactor);

        Launch(dirWorld.normalized, force);
    }

    private void Launch(Vector3 direction, float force)
    {
        launched = true;

        // stacca dal BallHolder ma mantiene la posizione nel mondo
        transform.SetParent(null, true);

        rb.isKinematic = false;
        rb.useGravity = true;

        rb.AddForce(direction * force, ForceMode.Impulse);

        if (spawner != null)
            spawner.RequestRespawn();
    }






}

