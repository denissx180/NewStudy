using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    private const float DELAY = 3f;

    public GameObject player;
    public CanvasGroup exitCanvasGroup;

    private float fadeDuration = 1;
    private bool isPlayerAtExit;
    private float timer = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit == true)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        timer += Time.deltaTime;
        exitCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + DELAY)
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
