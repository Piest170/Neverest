using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public TMP_Text charactername;
    [SerializeField]
    public TMP_Text jobname;
    [SerializeField]
    public TMP_Text skillname;
    void Start()
    {
        string characterId = PlayerPrefs.GetString("CharacterId");
        Debug.Log(characterId);
        //StartCoroutine(GameManager.Instance.GetCharacter(int.Parse(characterId)));
    }

    void Update()
    {
        
    }

}
