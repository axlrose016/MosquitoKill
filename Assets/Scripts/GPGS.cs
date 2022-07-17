using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPGS : MonoBehaviour
{
    public Text uiMessage;
    public static PlayGamesPlatform platform;
    //public float timer = 0, waitTime = .5f;
    public bool isSaving;

    //Instantiate the Platform
    void Awake()
    {
        try
        {

            #region Instantiate the GPGS
            PlayGamesClientConfiguration.Builder config = new PlayGamesClientConfiguration.Builder();
            config.EnableSavedGames();
            PlayGamesPlatform.InitializeInstance(config.Build());
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            platform = PlayGamesPlatform.Activate();
            #endregion
            SignIn();
        }
        catch(Exception ex)
        {
            Debug.LogError("GPGS Instantiate Platform Error: " + ex.Message);
        }
    }

    //void LateUpdate()
    //{
    //    RuntimeValidation();
    //}
    ////Runtime Validation
    //public void RuntimeValidation()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > waitTime)
    //    {
    //        bool isConnected = STC.isConnectedToInternet();
    //        uiMessage.text += $"isConnected: {isConnected}";
    //        if (Login.isGoogleAuth)
    //            FindObjectOfType<GPGS>().OpenSave(false);

    //        timer = 0f;
    //    }
    //}

    //Sign in to Google Play
    public void SignIn()
    {
        try
        {
            if(platform != null)
            {
                #region Sign In Player
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
                        Login.isGoogleAuth = true; Login.isGuest = false;
                        OpenSave(false);
                    }
                    else
                        FindObjectOfType<UIManager>().ShowLogin();
                });
                #endregion
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("GPGS SignIn Error: " + ex.Message);
        }
    }

    //Sign out to Google Play
    public void SignOut()
    {
        try
        {
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.SignOut();
                Login.isGoogleAuth = false;
                Login.isGuest = true;
                SceneManager.LoadScene("MainGame");
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("GPGS Sign Out: " + ex.Message);
        }
    }
    //Check if Authenticated to Google Play
    public static bool isCurrentlyAuthenticatedToGoogle()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
            return true;
        else
            return false;
    }

    //Cloud Saving
    public void OpenSave(bool saving)
    {
        try
        {
            if (Social.localUser.authenticated)
            {
                isSaving = saving;
                ((PlayGamesPlatform)Social.Active).SavedGame
                    .OpenWithAutomaticConflictResolution(
                    "PKoibitoData",
                    DataSource.ReadCacheOrNetwork,
                    GooglePlayGames.BasicApi.SavedGame.ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("GPGS Open Save Error: " + ex.Message);
        }
    }
    private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        try
        {
            if (status == SavedGameRequestStatus.Success)
            {
                if (isSaving)
                {
                    string dataToSave = $"{PlayerPrefs.GetInt("Score")},{PlayerPrefs.GetInt("Koinz")},{PlayerPrefs.GetString("DefaultThrowable")},1,{PlayerPrefs.GetInt("item1")},{PlayerPrefs.GetInt("item2")}" +
                        $",{PlayerPrefs.GetInt("item3")},{PlayerPrefs.GetInt("item4")},{PlayerPrefs.GetInt("item5")},{PlayerPrefs.GetInt("item6")},{PlayerPrefs.GetInt("item7")},{PlayerPrefs.GetInt("item8")}" +
                        $",{PlayerPrefs.GetInt("item9")},{PlayerPrefs.GetInt("item10")},{PlayerPrefs.GetInt("item11")},{PlayerPrefs.GetInt("item12")},{PlayerPrefs.GetInt("item13")},{PlayerPrefs.GetInt("item14")},{PlayerPrefs.GetInt("item15")}";
                    byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(dataToSave);
                    uiMessage.text += $"\n dataToSave: {dataToSave} \n data: {data}";
                    SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved at " + DateTime.Now.ToString()).Build();
                    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
                }
                else
                {
                    uiMessage.text += $"\n SaveGameOpened Loading: {status} >> isSaving:{isSaving}";
                    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, SaveRead);
                }
            }
            else
                uiMessage.text += $"\n SaveGameOpened Status: {status}";
        }
        catch (Exception ex)
        {
            Debug.LogError("GPGS SaveGameOpened Error: " + ex.Message);
        }
    }
    //Load Data
    private void SaveRead(SavedGameRequestStatus status, byte[] data)
    {
        try
        {
            if (status == SavedGameRequestStatus.Success)
            {
                string savedData = System.Text.ASCIIEncoding.ASCII.GetString(data);
                int score = Convert.ToInt32(savedData.Split(',')[0]);
                int koinz = Convert.ToInt32(savedData.Split(',')[1]);
                string playerDefault = Convert.ToString(savedData.Split(',')[2]);
                int item0 = Convert.ToInt32(savedData.Split(',')[3]); int item1 = Convert.ToInt32(savedData.Split(',')[4]); int item2 = Convert.ToInt32(savedData.Split(',')[5]); int item3 = Convert.ToInt32(savedData.Split(',')[6]);
                int item4 = Convert.ToInt32(savedData.Split(',')[7]); int item5 = Convert.ToInt32(savedData.Split(',')[8]); int item6 = Convert.ToInt32(savedData.Split(',')[9]); int item7 = Convert.ToInt32(savedData.Split(',')[10]);
                int item8 = Convert.ToInt32(savedData.Split(',')[11]); int item9 = Convert.ToInt32(savedData.Split(',')[12]); int item10 = Convert.ToInt32(savedData.Split(',')[13]); int item11 = Convert.ToInt32(savedData.Split(',')[14]);
                int item12 = Convert.ToInt32(savedData.Split(',')[15]); int item13 = Convert.ToInt32(savedData.Split(',')[16]); int item14 = Convert.ToInt32(savedData.Split(',')[17]); int item15 = Convert.ToInt32(savedData.Split(',')[18]);
                PlayerPrefs.SetString("RawData", savedData);
                PlayerPrefs.SetInt("Score", score);
                PlayerPrefs.SetInt("Koinz", koinz);
                if (String.IsNullOrEmpty(playerDefault))
                    playerDefault = "item1";
                PlayerPrefs.SetString("DefaultThrowable", playerDefault);
                PlayerPrefs.SetInt("item0", item0); PlayerPrefs.SetInt("item1", item1); PlayerPrefs.SetInt("item2", item2); PlayerPrefs.SetInt("item3", item3); PlayerPrefs.SetInt("item4", item4); PlayerPrefs.SetInt("item5", item5);
                PlayerPrefs.SetInt("item6", item6); PlayerPrefs.SetInt("item7", item7); PlayerPrefs.SetInt("item8", item8); PlayerPrefs.SetInt("item9", item9); PlayerPrefs.SetInt("item10", item10); PlayerPrefs.SetInt("item11", item11);
                PlayerPrefs.SetInt("item12", item12); PlayerPrefs.SetInt("item13", item13); PlayerPrefs.SetInt("item14", item14); PlayerPrefs.SetInt("item15", item15);
                GameplayManager gm = FindObjectOfType<GameplayManager>();
                gm.txtBestScore.text = PlayerPrefs.GetInt("Score").ToString();
                GameplayManager.PlayerKoinz = PlayerPrefs.GetInt("Koinz");
                gm.txtKoinz.text = gm.txtStoreKoinz.text = PlayerPrefs.GetInt("Koinz").ToString();
            }
            else
                uiMessage.text += $"\n SaveRead status: {status}";
        }
        catch (Exception ex)
        {
            uiMessage.text += $"\n SaveRead error: {ex.Message}";
            Debug.LogError("GPGS SaveRead Error:" + ex.Message);
        }
    }
    //Status Callback for Saving
    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        GameplayManager gm = FindObjectOfType<GameplayManager>();
        gm.txtKoinz.text = gm.txtStoreKoinz.text = PlayerPrefs.GetInt("Koinz").ToString();
        gm.txtBestScore.text = PlayerPrefs.GetInt("Score").ToString();
        OpenSave(false);
        Debug.Log("Save Update: "+status);
    }
    //Achievements
    public static void UnlockAchievement(string achievementId)
    {
        Social.ReportProgress(achievementId, 100.0f, (bool success) =>
        {
        });
    }
    public static void IncrementalAchievement(string achievementId)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(achievementId,1, null);
    }
    public void ShowAchievements()
    {
        try
        {
            if (Login.isGuest)
            {
                FindObjectOfType<UIManager>().ShowLogin();
                if (GPGS.isCurrentlyAuthenticatedToGoogle())
                    Social.ShowAchievementsUI();
            }
            else
                Social.ShowAchievementsUI();
        }
        catch (Exception ex)
        {
            FindObjectOfType<UIManager>().ShowDialogBox(ex.Message);
            Debug.LogError("Show Achievement Error: " + ex.Message);
        }
    }
    //Leaderboards
    public static void AddScoreToLeaderBoard(string leaderboardId, long score)
    {
        try
        {
            Social.ReportScore(score, leaderboardId, success => { });
        }
        catch(Exception ex)
        {
            FindObjectOfType<UIManager>().ShowDialogBox(ex.Message);
            Debug.LogError("AddScoreToLeaderBoard Error: " + ex.Message);
        }
    }
    public void ShowLeaderboard()
    {
        try
        {
            if (Login.isGuest)
            {
                FindObjectOfType<UIManager>().ShowLogin();
                if (GPGS.isCurrentlyAuthenticatedToGoogle())
                    Social.ShowLeaderboardUI();
            }
            else
                Social.ShowLeaderboardUI();
        }
        catch(Exception ex)
        {
            FindObjectOfType<UIManager>().ShowDialogBox(ex.Message);
            Debug.LogError("Show LeaderBoard Error: " + ex.Message);
        }
    }
}
