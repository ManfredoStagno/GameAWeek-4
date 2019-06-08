using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public float playerSpeed;

    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;

}
