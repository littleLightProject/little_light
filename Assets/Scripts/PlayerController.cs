using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpPower;

    Vector2 movement;
    Vector2 jump;

    private bool isJumping;

    private Animator animator;
    private Transform _transform;
    private SpriteRenderer renderer;
    private Rigidbody2D rigid;

    void Start()
    {
        jump = Vector2.zero;

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        _transform = transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Debug.Log(jump.x);
            jump.y = jumpPower;
            Jump();
        }

        if (!isJumping)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (Input.GetAxis("Horizontal") != 0)
            {
                animator.SetBool("isPlayerMove", true);

                if (moveHorizontal < 0)
                {
                    renderer.flipX = true;
                }
                else if (moveHorizontal > 0)
                {
                    renderer.flipX = false;
                }

                jump.x = moveHorizontal * 5;
                Vector2 movement = new Vector2(moveHorizontal, 0);

                _transform.Translate(movement * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isPlayerMove", false);
            }
        }
    }
        
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Background")
        {
            isJumping = false;
            Debug.Log("test");
            animator.SetBool("isPlayerJump", false);
        }
    }
   
    void Move()
    {
    }

    void Jump()
    {
        isJumping = true;
        rigid.AddForce(jump, ForceMode2D.Impulse);
        animator.SetBool("isPlayerJump", true);
    }
}
