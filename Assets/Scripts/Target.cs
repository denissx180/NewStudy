using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    None,
    Good,
    Bad
}

public class Target : MonoBehaviour
{
    public int scoreValue;
    public ParticleSystem explosionParticle;
    public TargetType targetType;

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

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(scoreValue);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetType == TargetType.Good)
        {
            gameManager.UpdateLives(1); 
        }
        Destroy(gameObject);
    }
}
