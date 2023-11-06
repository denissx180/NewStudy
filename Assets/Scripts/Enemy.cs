using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody rigidbody;
    private GameObject player;
    private float lowBound = -5;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (player.transform.position - transform.position).normalized;
        rigidbody.AddForce(moveDirection * speed);
        if (transform.position.y < lowBound)
        {
            Destroy(gameObject);
        }
    }
}
