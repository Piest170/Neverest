using Assets.Script.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public bool IsAppear;
    public float textSpeed;
    public bool IsClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsClose)
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
    }

    public void newText()
    {
        DialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        PlayerPrefs.SetString("SkillId", null);
        PlayerPrefs.SetString("LearningLevel", null);
        SkillPanel.SetActive(true);
        IsAppear = true;
    }

    IEnumerator TextType()
    {
        if (IsAppear)
        {
            foreach (char letter in PreDialog[index].ToCharArray())
            {
                DialogText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    public void newTextLine()
    {
        if(index < PreDialog.Length - 1)
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
        StartCoroutine(GameManager.Instance.GetSkillModel(1));
        Level2Panel.SetActive(true);
        PlayerPrefs.SetString("SkillId", "1");
    }

    public void GetCSS()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(2));
        PlayerPrefs.SetString("SkillId", "2");
        PlayerPrefs.SetString("LearningLevel", "1");
    }

    public void GetJavaScript()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(3));
        PlayerPrefs.SetString("SkillId", "3");
        Level2Panel.SetActive(true);
    }

    public void GetTypeScript()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(4));
        PlayerPrefs.SetString("SkillId", "4");
        Level2Panel.SetActive(true);
    }

    public void GetAngular()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(5));
        PlayerPrefs.SetString("SkillId", "5");
        Level3Panel.SetActive(true);
    }
    public void GetCsharp()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(6));
        PlayerPrefs.SetString("SkillId", "6");
        Level3Panel.SetActive(true);
    }
    public void GetAspNet()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(7));
        PlayerPrefs.SetString("SkillId", "7");
        Level3Panel.SetActive(true);
    }
    public void GetJava()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(8));
        PlayerPrefs.SetString("SkillId", "8");
        Level2Panel.SetActive(true);
    }
    public void GetPython()
    {
        StartCoroutine(GameManager.Instance.GetSkillModel(9));
        PlayerPrefs.SetString("SkillId", "9");
        Level2Panel.SetActive(true);
    }

    public void FinishDialog()
    {
        string characterId = PlayerPrefs.GetString("CharacterId");
        string skillId = PlayerPrefs.GetString("SkillId"); // choose in UI dialog
        string level = PlayerPrefs.GetString("LearningLevel"); // choose in UI dialog

        SkillCreds skill = new SkillCreds()
        {
            characterId = int.Parse(characterId),
            skillId = int.Parse(skillId),
            learningLevel = int.Parse(level),
            learningStatus = "Learning"
        };
        StartCoroutine(GameManager.Instance.GetCharacter(int.Parse(characterId)));
        var skills = new List<SkillCreds>();
        skills.Add(skill);
        StartCoroutine(GameManager.Instance.LearnSkill(skills.ToArray()));
        PlayerPrefs.SetString("SkillId", null);
        PlayerPrefs.SetString("LearningLevel", null);
        SkillPanel.SetActive(false);
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
            IsClose = false;
        }
    }
}
