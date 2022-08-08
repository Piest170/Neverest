using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private float x, y;
    private bool isWalking;

    public float Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if(x != 0 || y != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
            }
            OnAnimatorMove();
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                anim.SetBool("isWalking", isWalking);
            }
        }
    }
    private void OnAnimatorMove()
    {
        anim.SetFloat("X", x);
        anim.SetFloat ("Y", y);

        transform.Translate(x * Time.deltaTime * Speed, y * Time.deltaTime * Speed, 0);
    }
}
