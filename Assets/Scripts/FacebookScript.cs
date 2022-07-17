using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookScript : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't Initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    public void FacebookLogin()
    {
        var permission = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permission, (success) => {
            if (string.IsNullOrEmpty(success.Error))
                Login.isFBAuth = true;
        });
    }

    public void FacebookShare()
    {
        if (!FB.IsLoggedIn)
            FacebookLogin();
     
        if(FB.IsLoggedIn)
        {
            FB.ShareLink(
                contentURL: new System.Uri("https://www.facebook.com/Koibito-Games-102983231394618/?modal=admin_todo_tour"),
                contentTitle: "Mosquito Kill",
                contentDescription: $"Hey I Scored:{FindObjectOfType<GameplayManager>().PlayerBestScore} in this game. Install now and beat my score.",
                photoURL: new System.Uri("https://drive.google.com/file/d/1Y46xBnchTYimYTUHqjAjJUfwzGy2XhQm/view?usp=sharing"));
        }
    }

    public void FacebookInvite()
    {
        FB.AppRequest($"Hey.. I scored{FindObjectOfType<GameplayManager>().PlayerBestScore} in this game. Install now and beat me.", title: "Mosquito Kill");
    }

    public void FacebookLogout()
    {
        FB.LogOut();
        Login.isFBAuth = false;
    }
}
