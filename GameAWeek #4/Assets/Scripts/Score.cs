using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    public GameObject gameOver;


    private void Start()
    {
        gameOver.SetActive(false);
    }

    void Update()
    {
        scoreText.text = GameManager.instance.score.ToString("0.00");

        if (GameManager.instance.GAMEISOVER)
        {
            gameOver.SetActive(true);
        }
    }
}
