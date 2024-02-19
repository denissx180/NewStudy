using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movementDirection;
    private Animator animator;
    private Rigidbody rigidBody;
    public float turnSpeed = 21;
    private Quaternion rotation = Quaternion.identity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection.Set(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementDirection, turnSpeed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        rigidBody.MovePosition(rigidBody.position + movementDirection * animator.deltaPosition.magnitude);
        rigidBody.MoveRotation(rotation);
    }
}
