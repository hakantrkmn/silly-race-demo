using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    #region singleton


    private static Ranking _instance;

    public static Ranking Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<Ranking>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public GameObject Player;
    public List<GameObject> Opponents;
    public List<float> Distances;

    //player ve obstacleleri level bitince atamasını yapıyoruz. bu fonksiyonla bütün playerlardan uzaklığını alıp sıralayıp asıl oyuncunun rankını geri döndürüyoruz.
    public void Rank()
    {
        foreach (var item in Opponents)
        {
            Distances.Add(item.GetComponent<Opponent>().EndDistance);
        }
        var playerDist = Player.GetComponent<PlayerController>().EndDistance;
        Distances.Add(Player.GetComponent<PlayerController>().EndDistance);
        Distances.Sort();
        Player.GetComponent<PlayerController>().line = Distances.IndexOf(playerDist);
        Distances.Clear();

    }
}
