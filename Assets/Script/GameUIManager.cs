using Assets.Script.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("Reference")]
    [SerializeField]
    public GameObject Menu;
    [SerializeField]
    public GameObject EditMenu;
    [SerializeField]
    public GameObject Save;

    // Start is called before the first frame update
    void Start()
    {
        ClearUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClearUI()
    {
        Menu.SetActive(false);
        EditMenu.SetActive(false);
    }

    public void GetMenu()
    {
        Menu.SetActive(true);
    }

    public void GetEditMenu()
    {
        EditMenu.SetActive(true);
    }

    public void Saved()
    {
        Save.SetActive(false);
    }

    public void Exit()
    {
        ClearUI();
    }

    public void LogOut()
    {
        ClearUI();
        PlayerPrefs.SetString("CharacterId", null);
        SceneManager.LoadScene("Title Page");
    }
}
