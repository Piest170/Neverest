using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Script
{
    public class CharacterSkillManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetSkill());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator GetSkill()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(""))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
        }
    }
}
