using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject DoorTrigger;
    public BoxCollider2D Trigger;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        Trigger = DoorTrigger.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            isOpen = true;
            animator.SetBool("IsOpen", isOpen);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            isOpen = false;
            animator.SetBool("IsOpen", isOpen);
        }
    }
}
