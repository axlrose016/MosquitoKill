using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static bool isGuest, isGoogleAuth, isFBAuth;
    
    public void LogAsGuest()
    {
        isGuest = true;
        isGoogleAuth = false; isFBAuth = false;
        FindObjectOfType<UIManager>().HideLogin();
    }
    public void LogToGoogle()
    {
        try
        {
            FindObjectOfType<GPGS>().SignIn();
            FindObjectOfType<UIManager>().HideLogin();
        }
        catch(Exception ex)
        {
            Debug.LogError("Log To Google Error:" + ex.Message);
        }
    }

    public void LogToFB()
    {
        try
        {
            FindObjectOfType<FacebookScript>().FacebookLogin();
            FindObjectOfType<UIManager>().HideLogin();
        }
        catch (Exception ex)
        {
            Debug.LogError("Log To Facebook Error:" + ex.Message);
        }
    }
}
