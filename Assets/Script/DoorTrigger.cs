using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public Transform transforms;
    public GameObject DoorControl;
    public BoxCollider2D Stage;

    // Start is called before the first frame update
    void Start()
    {
        Stage = DoorControl.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (Stage.name == "FrontendDoorControl")
            {
                if(SceneManager.GetActiveScene().name == "Village")
                {
                    SceneManager.LoadScene("Frontend House");
                }
                else if (SceneManager.GetActiveScene().name == "Frontend House")
                {
                    SceneManager.LoadScene("Village");
                    Debug.Log(transforms.position);
                }
            }
            if (Stage.name == "BackendDoorControl")
            {
                if (SceneManager.GetActiveScene().name == "Village")
                {
                    SceneManager.LoadScene("Backend House");
                }
                else if (SceneManager.GetActiveScene().name == "Backend House")
                {
                    SceneManager.LoadScene("Village");
                    Debug.Log(transforms.position);
                }
            }
        }
    }
}
