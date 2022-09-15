using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Vector2 movement;
    private float x, y;
    private bool isWalking;
    public Rigidbody2D rb;
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

        movement = new Vector2(x, y);
        rb.velocity = movement * Speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (x != 0 || y != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && Speed < 2)
            {
                Speed += 1;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && Speed > 0)
            {
                Speed -= 1;
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
        if (movement != Vector2.zero)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        transform.Translate(x * Time.deltaTime * Speed, y * Time.deltaTime * Speed, 0);
    }
}