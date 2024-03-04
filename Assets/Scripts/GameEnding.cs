using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    private const float DELAY = 3f;

    public GameObject player;
    public CanvasGroup exitCanvasGroup;
    public CanvasGroup caughtCanvasGroup;

    private float fadeDuration = 1;
    private float timer = 0;
    private bool isPlayerAtExit;
    private bool isPlayerCaught;

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
            EndLevel(exitCanvasGroup, false);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtCanvasGroup, true);
        }
    }

    private void EndLevel(CanvasGroup canvasGroup, bool doRestart)
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + DELAY)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
                Debug.Log("Quit");
            }
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }
}
