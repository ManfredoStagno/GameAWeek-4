using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one GameManager instance!");
            return;
        }
        instance = this;
    }

    #endregion

    public Transform player;

    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;

    public bool debugging = false;

    [HideInInspector]
    public bool GAMEISOVER = false;

    [HideInInspector]
    public float score;

    private void Update()
    {
        if (!GAMEISOVER)
        {
            score = player.position.z;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void GameOver()
    {
        if(!debugging)
            GAMEISOVER = true;
    }

}
