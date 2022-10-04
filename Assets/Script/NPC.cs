using Assets.Script.Models;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject NotifyPanel;
    public GameObject SkillPanel;
    public GameObject Level2Panel;
    public GameObject Level3Panel;
    public Text DialogText;
    public string[] PreDialog, PostDialog;
    private int index;

    public bool IsAppear, IsAction, IsClose;
    public float textSpeed;

    // Start is called before the first frame update
    void Start()
    {
        IsAction = false;
        IsAppear = false;
        Debug.Log("SkillId : " + PlayerPrefs.GetString("SkillId"));
        Debug.Log("Level : " + PlayerPrefs.GetString("LearningLevel"));
        Debug.Log("AppearStatus : " + IsAppear);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsClose)
        {
            IsAction = false;
            IsAppear = false;
            Dialog();
        }
    }

    public void Dialog()
    {
        if (dialogPanel.activeInHierarchy)
        {
            newText();
        }
        else
        {
            NotifyPanel.SetActive(false);
            dialogPanel.SetActive(true);
            SkillPanel.SetActive(false);
            StartCoroutine(TextType());

            //Button b = GameObject.Find("Panel").AddComponent<Button>();
            //b.name = "button1";
            //GameObject.Find("button1").GetComponentInChildren<Text>().text = "Hello";
            //b.transform.parent = GameObject.Find("Panel").GetComponent<RectTransform>();
        }
    }

    public void newText()
    {
        DialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        SkillPanel.SetActive(true);
        if (IsAction)
        {
            PlayerPrefs.SetString("SkillId", null);
            PlayerPrefs.SetString("LearningLevel", null);
            SkillPanel.SetActive(false);
        }
    }

    IEnumerator TextType()
    {
        if (!IsAction)
        {

            foreach (char letter in PreDialog[index].ToCharArray())
            {
                DialogText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            foreach (char letter in PostDialog[index].ToCharArray())
            {
                DialogText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    public void newTextLine()
    {
        if ((index < PreDialog.Length - 1) || (index < PostDialog.Length))
        {
            index++;
            DialogText.text = "";
            StartCoroutine(TextType());
        }
        else
        {
            newText();
        }
    }

    public void Lv1()
    {
        PlayerPrefs.SetString("LearningLevel", "1");
        Level2Panel.SetActive(false);
        Level3Panel.SetActive(false);
    }

    public void Lv2()
    {
        PlayerPrefs.SetString("LearningLevel", "2");
        Level2Panel.SetActive(false);
        Level3Panel.SetActive(false);
    }
    public void Lv3()
    {
        PlayerPrefs.SetString("LearningLevel", "3");
        Level3Panel.SetActive(false);
    }

    public void GetHTML()
    {
        PlayerPrefs.SetString("SkillId", "1");
        Level2Panel.SetActive(true);
    }

    public void GetCSS()
    {
        PlayerPrefs.SetString("SkillId", "2");
        PlayerPrefs.SetString("LearningLevel", "1");
    }

    public void GetJavaScript()
    {
        PlayerPrefs.SetString("SkillId", "3");
        Level2Panel.SetActive(true);
    }

    public void GetTypeScript()
    {
        PlayerPrefs.SetString("SkillId", "4");
        Level2Panel.SetActive(true);
    }

    public void GetAngular()
    {
        PlayerPrefs.SetString("SkillId", "5");
        Level3Panel.SetActive(true);
    }
    public void GetCsharp()
    {
        PlayerPrefs.SetString("SkillId", "6");
        Level3Panel.SetActive(true);
    }
    public void GetAspNet()
    {
        PlayerPrefs.SetString("SkillId", "7");
        Level3Panel.SetActive(true);
    }
    public void GetJava()
    {
        PlayerPrefs.SetString("SkillId", "8");
        Level2Panel.SetActive(true);
    }
    public void GetPython()
    {
        PlayerPrefs.SetString("SkillId", "9");
        Level2Panel.SetActive(true);
    }

    public void FinishDialog()
    {
        string characterId = PlayerPrefs.GetString("CharacterId");
        string skillId = PlayerPrefs.GetString("SkillId"); // choose in UI dialog
        string level = PlayerPrefs.GetString("LearningLevel"); // choose in UI dialog

        CharacterSkill characterSkill = new CharacterSkill()
        {
            characterId = int.Parse(characterId),
            skillId = int.Parse(skillId),
            learningLevel = int.Parse(level)
        };
        //StartCoroutine(GameManager.Instance.GetCharacterSkill(characterSkill));
        
        SkillCreds skill = new SkillCreds()
        {
            characterId = int.Parse(characterId),
            skillId = int.Parse(skillId),
            learningLevel = int.Parse(level),
            learningStatus = "Learning"
        };

        var skills = new List<SkillCreds>();
        skills.Add(skill);
        StartCoroutine(LearnSkill(skills.ToArray()));

        //bool success = bool.Parse(PlayerPrefs.GetString("CheckSkill"));
        //if (success)
        //{
        //    //var skills = new List<SkillCreds>();
        //    //skills.Add(skill);
        //    //StartCoroutine(GameManager.Instance.LearnSkill(skills.ToArray()));
        //    PostDialog[0] = "คุณได้ทำการลงทะเบียนสำเร็จ ขอให้คุณโชคดี";
        //}
        //else
        //{
        //    PostDialog[0] = "คุุณได้เคยทำการลงทะเบียนแล้ว";
        //}
        //Debug.Log("Data Staus: " + success);
        //Debug.Log("Appear Status: " + IsAppear);
        //IsAppear = true;
        //IsAction = true;
        //Dialog();
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
                    if (results.success)
                        PostDialog[0] = "คุณได้ทำการลงทะเบียนสำเร็จ ขอให้คุณโชคดี";
                    else
                        PostDialog[0] = "คุุณได้เคยทำการลงทะเบียนแล้ว";

                    IsAppear = true;
                    IsAction = true;
                    Dialog();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            NotifyPanel.SetActive(true);
            IsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            NotifyPanel.SetActive(false);
            IsClose = false;
        }
    }
}
