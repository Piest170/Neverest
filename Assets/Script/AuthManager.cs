using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class RegisterCreds
{
    public string email;
    public string username;
    public string password;
    public string confirmPassword;
}

[Serializable]
public class LoginCreds
{
    public string username;
    public string password;
}

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;

    [SerializeField]
    public TMP_InputField registeremail;
    [SerializeField]
    public TMP_InputField registerusername;
    [SerializeField]
    public TMP_InputField registerpassword;
    [SerializeField]
    public TMP_InputField registerconfirmPassword;
    [SerializeField]
    public TMP_InputField loginusername;
    [SerializeField]
    public TMP_InputField loginpassword;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Register()
    {
        string email = registeremail.text;
        string username = registerusername.text;
        string password = registerpassword.text;
        string confirmPassword = registerconfirmPassword.text;
        RegisterCreds credentials = new RegisterCreds();
        credentials.email = email;
        credentials.username = username;
        credentials.password = password;
        credentials.confirmPassword = confirmPassword;
        StartCoroutine(Register(credentials));
    }

    public void Login()
    {
        string username = loginusername.text;
        string userPassword = loginpassword.text;
        LoginCreds credentials = new LoginCreds();
        credentials.username = username;
        credentials.password = userPassword;
        StartCoroutine(LogIn(credentials));
    }

    public IEnumerator Register(RegisterCreds credentials)
    {
        string URL = "https://localhost:7094/Auth/user/register";
        string jsonData = JsonUtility.ToJson(credentials);
        using (UnityWebRequest restAPI = UnityWebRequest.Put(URL, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPOST; 
            restAPI.SetRequestHeader("content-type", "application/json");
            restAPI.SetRequestHeader("Accept", "text/plain");

            yield return restAPI.SendWebRequest();

            if (restAPI.isNetworkError || restAPI.isHttpError)
            {
                Debug.Log(restAPI.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (restAPI.isDone)
                {
                    string token = JsonUtility.FromJson<string>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    if (token == null)
                    {
                        Debug.Log("register failed");
                    }
                    else
                    {
                        Debug.Log(token);//this DISPLAYS the information the rest api sent in response to the POST.
                    }
                }
            }
        }
    }

    public IEnumerator LogIn(LoginCreds credentials)
    {
        string URL = "https://localhost:7094/Auth/user/login";
        string jsonData = JsonUtility.ToJson(credentials);

        using (UnityWebRequest restAPI = UnityWebRequest.Put(URL, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPOST;
            restAPI.SetRequestHeader("content-type", "application/json");
            restAPI.SetRequestHeader("Accept", "text/plain");

            yield return restAPI.SendWebRequest();

            if (restAPI.isNetworkError || restAPI.isHttpError)
            {
                Debug.Log(restAPI.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (restAPI.isDone)
                {
                    string token = JsonUtility.FromJson<string>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    if (token == null)
                    {
                        Debug.Log("failed log in");
                    }
                    else
                    {
                        Debug.Log(token);
                    }
                }
            }
        }
    }
}
