using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Reference")]
    //[SerializeField]
    //private GameObject SettingUI;
    [SerializeField]
    private GameObject RegisterUI;
    [SerializeField]
    private GameObject LoginUI;
    [SerializeField]
    private GameObject PreSettingUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ClearUI()
    {
        RegisterUI.SetActive(false);
        LoginUI.SetActive(false);
        PreSettingUI.SetActive(false);
    }

    public void Login()
    {
        ClearUI();
        LoginUI.SetActive(true);
    }

    public void Register()
    {
        ClearUI();
        RegisterUI.SetActive(true);
    }

    public void PreSetting()
    {
        ClearUI();
        PreSettingUI.SetActive(true);
    }
}
