using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int Score, Gold;
    public float PlayTime;
    public int hour, min, sec;
    public int HP_Level, Speed_Level, Attack_Level;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            DataReset();
    }

    private void FixedUpdate()
    {
        DataSet();
        TimeSet();
    }

    public void TimeSet()
    {
        PlayTime += Time.deltaTime;

        hour = (int)(PlayTime / 3600);
        min = (int)((PlayTime - hour * 3600) / 60);
        sec = (int)(PlayTime % 60);
    }

    public void DataSet()
    {

        Gold = PlayerPrefs.GetInt("Gold");
        
        if(Score >= PlayerPrefs.GetInt("BestScore"))
        {
            int bestScore = Score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public void DataReset()
    {
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("HP_Level", 0);
        PlayerPrefs.SetInt("Speed_Level", 0);
        PlayerPrefs.SetInt("Attack_Level", 0);
        PlayerPrefs.SetInt("BestScore", 0);
    }
}
