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
    //[HideInInspector]
    AudioSource mainAudioSource;

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

    private void Start()
    {
        mainAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!GAMEISOVER)
        {
            score = player.position.z;
        }
        else
        {
            mainAudioSource.volume = Mathf.Lerp(mainAudioSource.volume, 0, 0.01f);
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
