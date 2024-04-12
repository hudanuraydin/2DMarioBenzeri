using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float speed = 1f;
    public float jumpSpeed = 1f;
    bool facingRight = true;
    public bool isGrounded=false;
    public float jumpFrequency=1f, nextJumpTime;

    public Transform groundChechkPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRB=GetComponent<Rigidbody2D>();
        playerAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();
        if (playerRB.velocity.x<0 && facingRight)
        {
            FlipFace();
        }
        else if(playerRB.velocity.x>0 && !facingRight) {
            FlipFace();        
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime<Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }
    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale=transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }
    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundChechkPosition.position,groundCheckRadius,groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }
    
}
