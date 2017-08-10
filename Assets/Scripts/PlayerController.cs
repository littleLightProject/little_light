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
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        _transform = transform;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal < 0)
        {
            animator.SetBool("isPlayerMove", true);
            renderer.flipX = true;
        }else if(moveHorizontal > 0)
        {
            animator.SetBool("isPlayerMove", true);
            renderer.flipX = false;
        } else
        {
            animator.SetBool("isPlayerMove", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = new Vector2(moveHorizontal, jumpPower);
            isJumping = true;
        }

        Vector2 movement = new Vector2(moveHorizontal, 0);

        _transform.Translate(movement * speed * Time.deltaTime);
    }
        

    void FixedUpdate()
    {
        Jump();
    }

    void Move()
    {
    }

    void Jump()
    {
        if (!isJumping)
            return;

        Debug.Log("jump: " + jump);

        rigid.AddForce(jump, ForceMode2D.Impulse);
        animator.SetTrigger("PlayerJump");

        isJumping = false;
    }
}
