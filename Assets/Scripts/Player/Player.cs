using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    bool isJumping;
    bool isAttacking;
       

    public Rigidbody2D rig;
    public Animator animator;

    void Start()
    {
        
    }
   
    void Update()
    {
        Jump();
        OnAttack();
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                animator.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
            
        }
    }

    void OnAttack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            animator.SetInteger("transition", 3);

            StartCoroutine(OnAttacking());
        }
    }

    IEnumerator OnAttacking()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    //É chamado pela física do jogo
    void FixedUpdate()
    {
        float direcao = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(direcao * speed, rig.velocity.y);

        if(direcao > 0 && !isJumping && !isAttacking)
        {
            transform.eulerAngles = new Vector2(0, 0);
            animator.SetInteger("transition", 1);
        }

        if(direcao < 0 && !isJumping && !isAttacking)
        {
            transform.eulerAngles = new Vector2(0, 180);
            animator.SetInteger("transition", 1);
        }

        if(direcao == 0 && !isJumping && !isAttacking)
        {
            animator.SetInteger("transition", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Player está colidindo com o chão
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }

}
