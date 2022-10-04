using Assets.Script.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject PanelText;
    public CheckSkill checkSkill;

    [SerializeField]
    public TMP_Text Text;
    [SerializeField]
    public TMP_Text charactername;
    [SerializeField]
    public TMP_Text jobname;
    [SerializeField]
    public TMP_Text skillname;
    [SerializeField]
    public TMP_InputField editcharactername;

    // Start is called before the first frame update
    void Start()
    {
        Text.text = SceneManager.GetActiveScene().name;
        Debug.Log("CharacterId : " + PlayerPrefs.GetString("CharacterId"));
        string characterId = PlayerPrefs.GetString("CharacterId");
        StartCoroutine(GetCharacter(int.Parse(characterId)));
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

    public void OnSave()
    {
        string characterId = PlayerPrefs.GetString("CharacterId");
        string Name = editcharactername.text;
        UpdateName updateName = new UpdateName();
        updateName.id = int.Parse(characterId);
        updateName.characterName = Name.ToString();
        StartCoroutine(UpdateCharacter(updateName));
        GameUIManager.Instance.Saved();
    }

    public IEnumerator GetCharacter(int id)
    {
        string URL = "https://localhost:7094/api/Character/" + id;
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
            var results = JsonUtility.FromJson<ServiceResponse<CharacterModel>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
            charactername.text = results.data.characterName;
            jobname.text = results.data.jobName;
            editcharactername.text = results.data.characterName;
            skillname.text = results.data.completedSkill;
            Debug.Log(results.data.characterId + " , " + results.data.characterName + " , " + results.data.jobName);
        }
    }

    public IEnumerator UpdateCharacter(UpdateName updateName)
    {
        string URL = "https://localhost:7094/api/Character";
        string jsonData = JsonUtility.ToJson(updateName);
        using (UnityWebRequest restAPI = UnityWebRequest.Put(URL, jsonData))
        {
            restAPI.method = UnityWebRequest.kHttpVerbPUT;
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
                    Debug.Log(restAPI.downloadHandler.text);

                    // Or retrieve results as binary data
                    //var results = JsonUtility.FromJson<ServiceResponse<CharacterModel>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    //int Id = results.data.characterId;
                    //editcharactername.text = results.data.characterName;
                    StartCoroutine(GetCharacter(int.Parse(PlayerPrefs.GetString("CharacterId"))));
                }
            }
        }
    }

    public IEnumerator GetCharacterSkill(CharacterSkill characterSkill)
    {
        string URL = "https://localhost:7094/api/Character/Skill?characterid=" + characterSkill.characterId + "&skillid=" + characterSkill.skillId + "&level=" + characterSkill.learningLevel;
        UnityWebRequest restAPI = UnityWebRequest.Get(URL);
        
        yield return restAPI.SendWebRequest();
        if (restAPI.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(restAPI.error);
            //yield return false;
        }
        else
        {
            // Show results as text
            Debug.Log(restAPI.downloadHandler.text);

            // Or retrieve results as binary data
            var results = JsonUtility.FromJson<ServiceResponse<CheckSkill>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
            Debug.Log(results);
            checkSkill.OnCheck = results.success;
            PlayerPrefs.SetString("CheckSkill", results.success.ToString());
            Debug.Log(checkSkill.OnCheck);
            //yield return results.success;
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
                    var results = JsonUtility.FromJson<ServiceResponse<CharacterModel>>(System.Text.Encoding.UTF8.GetString(restAPI.downloadHandler.data));
                    Debug.Log(results);
                    PlayerPrefs.SetString("CheckSkill", results.success.ToString());

                }
            }

            
        }
    }
}
