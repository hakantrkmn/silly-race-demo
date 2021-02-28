using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region singleton


    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<UIController>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion    

    public Text rank;
    public Text percent;
    private void Start()
    {
        PlayerController.onRankChanged += changeRankText;
        percentage.onPercentChanged += changePercent;
    }

    private void changePercent(float obj)
    {
        percent.text = obj.ToString();
    }

    private void changeRankText(int obj)
    {
        rank.text = obj.ToString();
    }


}
