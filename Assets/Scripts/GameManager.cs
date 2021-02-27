using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton

    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance==null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public enum gameStates { run,paint,over };
    public gameStates gameState;
    public enum playerStates { onGround, onAir };
    public playerStates playerState;
    private void Start()
    {
        gameState = gameStates.run;
        playerState = playerStates.onGround;
    }

}
