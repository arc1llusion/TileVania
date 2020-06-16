using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float levelLoadDelay = 2.0f;

    [SerializeField]
    private LayerMask playerMask = 0;

    [SerializeField]
    private float LevelExitSlowMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");      
        StartCoroutine(StartExit());
    }

    IEnumerator StartExit()
    {
        Time.timeScale = LevelExitSlowMoFactor;

        yield return new WaitForSeconds(levelLoadDelay);

        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
