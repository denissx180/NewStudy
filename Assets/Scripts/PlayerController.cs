using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float smashSpeed;
    public float hangTime;
    public float explosionForce;
    public float explosionRadius;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public PowerupType currentPowerupType = PowerupType.None;

    private Rigidbody rigidBody;
    private GameObject focalPoint;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    private float powerupStrength = 15;
    private float lowBound = -5;
    private bool isSmash;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(0, 2, 0);
        rigidBody.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerupIndicator.transform.position = transform.position + offset;
        if (transform.position.y < lowBound)
        {
            Destroy(gameObject);
        }

        if (currentPowerupType == PowerupType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerupType == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !isSmash)
        {
            isSmash = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Powerup powerup))
        {
            hasPowerup = true;
            currentPowerupType = powerup.powerupType;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdown());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && currentPowerupType == PowerupType.Pushback)
        {
            Rigidbody enemyRigidBody = enemy.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (enemy.transform.position - transform.position);

            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(10f);
        hasPowerup = false;
        currentPowerupType = PowerupType.None;
        powerupIndicator.SetActive(false);
    }

    private void LaunchRockets()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
    
    private IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        float floorPosition = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorPosition)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
            }
        }

        isSmash = false;
    }
}
