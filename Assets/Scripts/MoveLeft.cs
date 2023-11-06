using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private float leftBound = -15;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver)
        {
            if (playerController.isDash == true)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime * 2);
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }

        if (gameObject.TryGetComponent(out Obstacle obstacle) && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
