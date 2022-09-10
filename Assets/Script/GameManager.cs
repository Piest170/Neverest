using Assets.Script.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetCharacter(int id)
    {
        string URL = "https://localhost:7094/api/Character" + id;
        UnityWebRequest restAPI = UnityWebRequest.Get(URL);
        yield return restAPI.SendWebRequest();
        if (restAPI.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(restAPI.error);
        }
        else
        {
            // Show results as text
            Debug.Log(restAPI.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = restAPI.downloadHandler.data;
            Debug.Log(results);
        }
    }

    public IEnumerator GetSkillModel(int id)
    {
        string URL = "https://localhost:7094/api/Skill/" + id;
        UnityWebRequest restAPI = UnityWebRequest.Get(URL);
        yield return restAPI.SendWebRequest();
        if (restAPI.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(restAPI.error);
        }
        else
        {
            // Show results as text
            Debug.Log(restAPI.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = restAPI.downloadHandler.data;
            Debug.Log(results);
        }
    }

    public IEnumerator LearnSkill(SkillCreds[] skill)
    {
        string URL = "https://localhost:7094/api/Character/Skill";
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(skill);
        using (UnityWebRequest restAPI = UnityWebRequest.Put(URL, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPOST;
            restAPI.SetRequestHeader("Content-Type", "application/json");
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
                }
            }
        }
    }
}
