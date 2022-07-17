using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour
{
    public static int Score, Koinz;
    public static bool hasItem0, hasItem1, hasItem2, hasItem3, hasItem4, hasItem5, hasItem6, hasItem7, hasItem8, hasItem9, hasItem10, hasItem11, hasItem12, hasItem13, hasItem14, hasItem15;

    public static void LoadPlayerData()
    {
        PlayerData.Score = PlayerPrefs.GetInt("Score");
        PlayerData.Koinz = PlayerPrefs.GetInt("Koinz");
        PlayerData.hasItem0 = Convert.ToBoolean(PlayerPrefs.GetInt("item0"));
        PlayerData.hasItem1 = Convert.ToBoolean(PlayerPrefs.GetInt("item1"));
        PlayerData.hasItem2 = Convert.ToBoolean(PlayerPrefs.GetInt("item2"));
        PlayerData.hasItem3 = Convert.ToBoolean(PlayerPrefs.GetInt("item3"));
        PlayerData.hasItem4 = Convert.ToBoolean(PlayerPrefs.GetInt("item4"));
        PlayerData.hasItem5 = Convert.ToBoolean(PlayerPrefs.GetInt("item5"));
        PlayerData.hasItem6 = Convert.ToBoolean(PlayerPrefs.GetInt("item6"));
        PlayerData.hasItem7 = Convert.ToBoolean(PlayerPrefs.GetInt("item7"));
        PlayerData.hasItem8 = Convert.ToBoolean(PlayerPrefs.GetInt("item8"));
        PlayerData.hasItem9 = Convert.ToBoolean(PlayerPrefs.GetInt("item9"));
        PlayerData.hasItem10 = Convert.ToBoolean(PlayerPrefs.GetInt("item10"));
        PlayerData.hasItem11 = Convert.ToBoolean(PlayerPrefs.GetInt("item11"));
        PlayerData.hasItem12 = Convert.ToBoolean(PlayerPrefs.GetInt("item12"));
        PlayerData.hasItem13 = Convert.ToBoolean(PlayerPrefs.GetInt("item13"));
        PlayerData.hasItem14 = Convert.ToBoolean(PlayerPrefs.GetInt("item14"));
        PlayerData.hasItem15 = Convert.ToBoolean(PlayerPrefs.GetInt("item15"));
    }
}
