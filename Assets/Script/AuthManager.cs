using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Assets.Script.Models;

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
    public TMP_Text registerNotifytext;
    [SerializeField]
    public TMP_InputField loginusername;
    [SerializeField]
    public TMP_InputField loginpassword;
    [SerializeField]
    public TMP_Text LoginNotifytext;

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
                var message = JsonUtility.FromJson<ServiceResponse<string>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                Debug.Log(restAPI.error);
                registerNotifytext.text = message.message;
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (restAPI.isDone)
                {
                    var token = JsonUtility.FromJson<ServiceResponse<string>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    if (token == null)
                    {
                        Debug.Log("register failed");
                        registerNotifytext.text = token.message;
                    }
                    else
                    {
                        Debug.Log(token);
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
                var message = JsonUtility.FromJson<ServiceResponse<string>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                Debug.Log(restAPI.error);
                LoginNotifytext.text = message.message;
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (restAPI.isDone)
                {
                    var token = JsonUtility.FromJson<ServiceResponse<string>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    if (token == null)
                    {
                        Debug.Log("failed log in");
                        LoginNotifytext.text = token.message;
                    }
                    else
                    {
                        PlayerPrefs.SetString("CharacterId", token.data);
                        SceneManager.LoadScene("Village");
                        Debug.Log(token);
                    }
                }
            }
        }
    }
}
