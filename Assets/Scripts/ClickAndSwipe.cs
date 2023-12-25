using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(TrailRenderer))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCamera;
    private TrailRenderer trail;
    private BoxCollider collider;
    private Vector3 mousePosition;
    private bool isSwiping;

    private void Awake()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        collider = GetComponent<BoxCollider>();
        trail = GetComponent<TrailRenderer>();

        collider.enabled = false;
        trail.enabled = false;
    }

    private void UpdateMousePosition()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = mousePosition;
    }

    private void UpdateComponents()
    {
        collider.enabled = isSwiping;
        trail.enabled = isSwiping;
    }

    private void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isSwiping = false;
                UpdateComponents();
            }

            if (isSwiping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Target target))
        {
            target.DestroyTarget();
        }
    }
}
