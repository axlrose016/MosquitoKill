using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public bool isGameStart;
    public int PlayerScore;
    public int PlayerBestScore;
    public static int PlayerKoinz;
    public int initialScore;
    public float currSpeed;
    public float pitch;
    public Text txtBestScore;
    public Text txtKoinz, txtStoreKoinz;
    public bool isBeginner = false, isTalented = false, isExperienced = false, isExpert = false;


    void Awake()
    {
        //string path = Application.persistentDataPath + "/PData.koibito";
        //if (File.Exists(path))
        //{
        //    STC.PlayerPrefData data = STC.LoadPlayerScore();
        //    PlayerBestScore = data.bestScore;
        //    PlayerKoinz = data.totalKoinz;
        //    txtBestScore.text = PlayerBestScore.ToString();
        //    txtKoinz.text = txtStoreKoinz.text = PlayerKoinz.ToString();
        //}
    }


    void Start()
    {
        initialScore = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if (isGameStart)
            {
                Difficulty();
                if(Login.isGoogleAuth)
                    UnlockAchievement(PlayerScore);
                GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = PlayerScore.ToString();
                txtKoinz.text = txtStoreKoinz.text = PlayerKoinz.ToString();
                #region Instantiate Game Object
                bool isThrowableNotInstantiated = GameObject.FindGameObjectWithTag("Player") == null;
                bool isTargetNotInstantiated = GameObject.FindGameObjectWithTag("MainTarget") == null;
                string throwable = !String.IsNullOrEmpty(PlayerPrefs.GetString("DefaultThrowable")) ? PlayerPrefs.GetString("DefaultThrowable") : "item1";
                if (isThrowableNotInstantiated)
                    STC.InstantiateGameObject($"Prefabs/Throwables/{throwable}");
                if (isTargetNotInstantiated)
                {
                    FindObjectOfType<RandomPosition>().RandomPos("Reactor");
                    GameObject target = STC.InstantiateGameObject("Prefabs/Targets/Target");
                    target.gameObject.transform.position = GameObject.FindGameObjectWithTag("Move").transform.position;
                    target.GetComponent<TargetMovement>().target = GameObject.FindGameObjectWithTag("Move").transform;
                    target.GetComponent<TargetMovement>().speed += currSpeed;
                    target.GetComponent<AudioSource>().pitch = pitch;
                    FindObjectOfType<UIManager>().MainTarget = target;
                    FindObjectOfType<UIManager>().emote = target.GetComponentInChildren<Transform>().Find("Emote");
                    FindObjectOfType<RandomPosition>().RandomPos("MainTarget");
                }
                #endregion
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("Gameplay Manager Fix Update: " + ex.Message);
        }
    }

    void Difficulty()
    {
        if (PlayerScore == initialScore)
        {
            currSpeed += 1;
            initialScore += 9;
            pitch += .5f;
        }
    }

    public void UnlockAchievement(int playerScore)
    {
        switch(playerScore)
        {
            case 20:
                if(!isBeginner)
                {
                    GPGS.UnlockAchievement(GPGSIds.achievement_the_beginner);
                    isBeginner = true;
                }
                break;
            case 50:
                if(!isTalented)
                {
                    GPGS.UnlockAchievement(GPGSIds.achievement_the_talented_hunter);
                    isTalented = true;
                }
                break;
            case 100:
                if(!isExperienced)
                {
                    GPGS.UnlockAchievement(GPGSIds.achievement_the_experienced_hunter);
                    isExperienced = true;
                }
                break;
            case 150:
                if(!isExpert)
                {
                    GPGS.UnlockAchievement(GPGSIds.achievement_the_expert_hunter);
                    isExpert = true;
                }
                break;
            default:
                break;
        }
    }
}
