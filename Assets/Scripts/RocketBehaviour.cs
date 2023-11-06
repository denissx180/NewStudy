using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private float speed = 15;
    private float rocketStrength = 15;
    private float aliveTimer = 5;
    private Transform target;
    private bool isHoming;

    void Update()
    {
        if (isHoming == true && target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                enemyRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Transform enemy)
    {
        target = enemy;
        isHoming = true;
        Destroy(gameObject, aliveTimer);
    }
}
