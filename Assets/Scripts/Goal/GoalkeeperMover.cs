using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float offsetX = Mathf.Sin(Time.time * speed) * range;

        transform.position = startPos + new Vector3(offsetX, 0f, 0f);
    }
}
