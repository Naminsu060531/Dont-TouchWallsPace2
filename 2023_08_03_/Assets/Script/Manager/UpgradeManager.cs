using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int[] price;


    public void HPupgrade()
    {
        int hasGold = PlayerPrefs.GetInt("Gold");
        
        if(hasGold < price[0])
        {
            Debug.Log("°ñµå ºÎÁ·");
            return;
        }

        hasGold -= price[0];

        PlayerPrefs.SetInt("Gold", hasGold);

        price[0] += 50;
        
        int a = PlayerPrefs.GetInt("HP_Level");
        PlayerPrefs.SetInt("HP_Level", a+=1);
        Debug.Log("HP Level : " + PlayerPrefs.GetInt("HP_Level"));
    }

    public void Speedupgrade()
    {
        int hasGold = PlayerPrefs.GetInt("Gold");

        if (hasGold < price[1])
        {
            Debug.Log("°ñµå ºÎÁ·");
            return;
        }

        hasGold -= price[1];

        PlayerPrefs.SetInt("Gold", hasGold);

        price[1] += 100;

        int a = PlayerPrefs.GetInt("Speed_Level");
        PlayerPrefs.SetInt("Speed_Level", a += 1);
        Debug.Log("Speed Level : " + PlayerPrefs.GetInt("Speed_Level"));
    }

    public void Attackupgrade()
    {
        int hasGold = PlayerPrefs.GetInt("Gold");

        if (hasGold < price[2])
        {
            Debug.Log("°ñµå ºÎÁ·");
            return;
        }

        hasGold -= price[2];

        PlayerPrefs.SetInt("Gold", hasGold);

        price[2] += 50;

        int a = PlayerPrefs.GetInt("Attack_Level");
        PlayerPrefs.SetInt("Attack_Level", a += 1);
        Debug.Log("Attack Level : " + PlayerPrefs.GetInt("Attack_Level"));
    }
}
