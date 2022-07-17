using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public RectTransform MainUI, GameOverUI, Shop,LoginForm,Setting;
    public Transform lblScore,emote,DialogBox;
    public GameObject MainTarget;

    // Start is called before the first frame update
    void Start()
    {
        MainUI.DOAnchorPos(Vector2.zero,0.25f);
    }

    public void btnPlay()
    {
        FindObjectOfType<GameplayManager>().isGameStart = true;
        lblScore.DOScale(1, .5f);
        MainUI.DOAnchorPos(new Vector2(-500,0), 0.25f);
        FindObjectOfType<RandomPosition>().RandomPos("MainTarget");
    }
    public void ShowGameOver()
    {
        int currScore = FindObjectOfType<GameplayManager>().PlayerScore;
        int bestScore = Convert.ToInt32(FindObjectOfType<GameplayManager>().txtBestScore.text);
        int scoreToSave = currScore > bestScore ? currScore : bestScore;
        PlayerPrefs.SetInt("Score", scoreToSave);
        PlayerPrefs.SetInt("Koinz",  GameplayManager.PlayerKoinz);
        if (Login.isGoogleAuth)
        {
            FindObjectOfType<GPGS>().OpenSave(true);
            GPGS.AddScoreToLeaderBoard(GPGSIds.leaderboard_highscore, scoreToSave);
        }
        #region Save Data
        //int CurrScore = FindObjectOfType<GameplayManager>().PlayerScore;
        //int BestScore = FindObjectOfType<GameplayManager>().PlayerBestScore;
        //int scoreToSave = CurrScore > BestScore ? CurrScore : BestScore;
        //int koinz = FindObjectOfType<GameplayManager>().PlayerKoinz;
        //STC.SavePlayerScore(scoreToSave, koinz);
        #endregion
        #region Load Data
        //STC.PlayerPrefData data = STC.LoadPlayerScore();
        //FindObjectOfType<GameplayManager>().PlayerBestScore = data.bestScore;
        //FindObjectOfType<GameplayManager>().txtBestScore.text = data.bestScore.ToString();
        #endregion
        lblScore.DOScale(0, .5f);
        emote.DOScale(new Vector2(0.15f,0.12f), .5f);
        FindObjectOfType<GameplayManager>().isGameStart = false;
        GameOverUI.DOAnchorPos(Vector2.zero, 0.25f);
    }
    public void ShowShop()
    {
        FindObjectOfType<InventoryManager>().LoadInventory();
        MainTarget.SetActive(false);
        Shop.DOAnchorPos(Vector2.zero, 0.25f);
    }
    public void btnRetry()
    {
        emote.DOScale(0, .5f);
        GameObject.FindGameObjectWithTag("ButtonAd").GetComponent<Button>().interactable = true;
        
        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "0";
        GameObject target = GameObject.FindGameObjectWithTag("MainTarget");
        FindObjectOfType<GameplayManager>().PlayerScore = 0;
        FindObjectOfType<GameplayManager>().currSpeed = 1;
        target.GetComponent<TargetMovement>().speed = 1;
        target.GetComponent<AudioSource>().pitch = 0.5f;
        FindObjectOfType<GameplayManager>().isGameStart = true;
        HideGameOver();
    }
    public void HideGameOver()
    {
        GameOverUI.DOAnchorPos(new Vector2(0, 820), 0.25f);
        lblScore.DOScale(1, .5f);
    }
    public void HideShop()
    {
        MainTarget.SetActive(true);
        Shop.DOAnchorPos(new Vector2(500, 0), 0.25f);
    }
    public void ShowDialogBox(string text)
    {
        DialogBox.GetComponentInChildren<Text>().text = text;
        DialogBox.DOScale(new Vector2(70f, 72f), .5f);
        StartCoroutine(HideDialogBox());
    }
    IEnumerator HideDialogBox()
    {
        yield return new WaitForSeconds(3);
        DialogBox.GetComponentInChildren<Text>().text = string.Empty;
        DialogBox.DOScale(0, .5f);
    }
    public void ShowLogin()
    {
        MainTarget.SetActive(false);
        LoginForm.DOAnchorPos(Vector2.zero, 0.25f);
    }
    public void HideLogin()
    {
        MainTarget.SetActive(true);
        LoginForm.DOAnchorPos(new Vector2(0, -820), 0.25f);
    }

    public void ShowSetting()
    {
        MainTarget.SetActive(false);
        Setting.DOAnchorPos(Vector2.zero, 0.13f);
    }

    public void HideSetting()
    {
        MainTarget.SetActive(true);
        Setting.DOAnchorPos(new Vector2(0, -1640), 0.13f);
    }

    public void BackToMain()
    {
        FindObjectOfType<GameplayManager>().PlayerScore = 0;
        FindObjectOfType<GameplayManager>().currSpeed = 1;
        GameObject target = GameObject.FindGameObjectWithTag("MainTarget");
        target.GetComponent<TargetMovement>().speed = 1;
        target.GetComponent<AudioSource>().pitch = 0.5f;
        emote.DOScale(0, .25f);
        GameOverUI.DOAnchorPos(new Vector2(0, 820), 0.25f);
        MainUI.DOAnchorPos(Vector2.zero, 0.25f);
        GameObject.FindGameObjectWithTag("ButtonAd").GetComponent<Button>().interactable = true;
    }

    public void ScoreIncremental()
    {
        GameplayManager.PlayerKoinz++;
        FindObjectOfType<GameplayManager>().txtKoinz.text = FindObjectOfType<GameplayManager>().txtStoreKoinz.text = GameplayManager.PlayerKoinz.ToString();
    }
}
