using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue;
    public ParticleSystem explosionParticle;

    private GameManager gameManager;
    private Rigidbody rigidbody;
    private float minHeight = 12;
    private float maxHeight = 18;
    private float torqueRange = 10;
    private float xRangePosition = 4;
    private float yPosition = -6;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        transform.position = new Vector3(RandomValue(xRangePosition), yPosition);
        rigidbody.AddForce(Vector3.up * RandomValue(minHeight, maxHeight), ForceMode.Impulse);
        rigidbody.AddTorque(RandomValue(torqueRange), RandomValue(torqueRange), RandomValue(torqueRange), ForceMode.Impulse);

    }

    private float RandomValue(float value, float value1 = 0)
    {
        if (value1 == 0)
        {
            return Random.Range(-value, value);
        }
        else
        {
            return Random.Range(value, value1);
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(scoreValue);
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    /*private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minHeight, maxHeight);
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRangePosition, xRangePosition), yPosition);
    }

    private float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }*/
}
