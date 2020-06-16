using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    private int startingSceneIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Debug.Log("num Scene Persists" + numScenePersists);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != startingSceneIndex)
        {
            Debug.Log("Destroy scene persist");
            Destroy(this.gameObject);
        }
    }
}
