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

    //gerekli actionlara subscribe yapıyoruz. bu sayede actionlar harekete geçince gerekli işlemleri yapıyoruz
    private void Start()
    {
        PlayerController.onRankChanged += changeRankText;
        percentage.onPercentChanged += changePercent;
    }

    // eğer boyama yüzdesi değiştiyse değiştiriyoruz
    private void changePercent(float obj)
    {
        percent.text = obj.ToString();
    }

    //sıralama değiştiyse değiştiriyoruz
    private void changeRankText(int obj)
    {
        rank.text = obj.ToString();
    }


}
