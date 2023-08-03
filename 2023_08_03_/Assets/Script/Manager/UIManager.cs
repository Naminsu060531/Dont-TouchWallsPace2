using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text HpText, SpeedText;
    public Text A_LevelText, S_LevelText, H_LevelText;
    public Text PlayTimeText, ScoreText, BestScoreText, GoldText;

    private void FixedUpdate()
    {
        TextSet();
    }

    public void TextSet()
    {
        HpText.text = "HP : " + Player.instance.HP;
        SpeedText.text = "Speed : " + Player.instance.Speed;

        A_LevelText.text = "Attack Level : " + PlayerPrefs.GetInt("Attack_Level");
        S_LevelText.text = "Speed Level : " + PlayerPrefs.GetInt("Speed_Level");
        H_LevelText.text = "HP Level : " + PlayerPrefs.GetInt("HP_Level");

        ScoreText.text = "Score : " + DataManager.instance.Score;
        BestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore");
        PlayTimeText.text = "PlayTIme : " + DataManager.instance.hour + " : " + DataManager.instance.min + " : " + DataManager.instance.sec;
        GoldText.text = "Gold : " + PlayerPrefs.GetInt("Gold");
    }
}
