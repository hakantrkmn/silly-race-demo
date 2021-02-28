using System;
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

    public enum gameStates { run,paint,over,start };
    public gameStates gameState;
    public enum playerStates { onGround, onAir };
    public playerStates playerState;
    public enum opponentStates { onGround, onAir };
    public opponentStates opponentState;
    private void Start()
    {
        gameState = gameStates.start;
        playerState = playerStates.onGround;
        opponentState = opponentStates.onGround;
    }

    private void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (gameState==gameStates.start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameState = gameStates.run;
            }
        }
    }
}
