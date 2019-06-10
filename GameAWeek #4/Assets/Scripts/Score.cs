using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    Text gameOverText;

    public GameObject gameOver;


    private void Start()
    {
        if(gameOver)
            gameOver.SetActive(false);
    }

    void Update()
    {
        scoreText.text = GameManager.instance.score.ToString("0.00");

        if (GameManager.instance.GAMEISOVER)
        {
            if (gameOver)
                gameOver.SetActive(true);
        }
    }
}
