using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager Instance;

    [Header("Reference")]
    [SerializeField]
    private GameObject RegisterUI;
    [SerializeField]
    private GameObject LoginUI;

    private void Awake()
    {
        ClearUI();
        LoginUI.SetActive(true);
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
}
