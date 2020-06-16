using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    [SerializeField] private int score = 0;

    [SerializeField] private float deathTime = 2f;

    [SerializeField]
    private TextMeshProUGUI LivesText = null;

    [SerializeField]
    private TextMeshProUGUI ScoreText = null;

    private int amethystsPickedUp = 0;

    public static GameSession Instance { get; private set; }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        LivesText.text = lives.ToString();
        ScoreText.text = score.ToString();
    }

    public void AddAmethyst(int pointsToAdd)
    {
        ++amethystsPickedUp;
        score += pointsToAdd;
        ScoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        --lives;
        LivesText.text = lives.ToString();
        if (lives <= 0)
        {
            StartCoroutine(ProcessPlayerDeathRoutine(() =>
            {
                SceneManager.LoadScene(0);
            }));
        }
        else
        {
            StartCoroutine(ProcessPlayerDeathRoutine(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }));            
        }
    }

    private IEnumerator ProcessPlayerDeathRoutine(Action sceneLoad)
    {
        yield return new WaitForSeconds(deathTime);

        sceneLoad();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
