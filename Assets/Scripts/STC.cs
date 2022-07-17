using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class STC : MonoBehaviour
{ 
    public static GameObject InstantiateGameObject(string path)
    {
        GameObject gameObj = (GameObject)Instantiate(Resources.Load(path));
        return gameObj;
    }

    public static void SavePlayerScore(int bestScore,int koinz)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/PData.koibito";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerPrefData data = new PlayerPrefData(bestScore, koinz);
            formatter.Serialize(stream, data);
            if (Login.isGoogleAuth)
                FindObjectOfType<GPGS>().OpenSave(true);
            stream.Close();

        }
        catch (Exception ex)
        {
            Debug.LogError("Save Player Error: " + ex.Message);
        }
    }

    public static PlayerPrefData LoadPlayerScore()
    {
        string path = Application.persistentDataPath + "/PData.koibito";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerPrefData data = formatter.Deserialize(stream) as PlayerPrefData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Sorry file not found.! " + path);
            return null;
        }
    }
    
    [System.Serializable]
    public class PlayerPrefData
    {
        public int bestScore;
        public int totalKoinz;
        public PlayerPrefData(int playerBestScore,int playerKoinz)
        {
            bestScore = playerBestScore;
            totalKoinz = playerKoinz;
        }
    }

    public static bool isConnectedToInternet()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
            return true;
        else
            return false;
    }
}
