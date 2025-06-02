using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR;

public class JumpEN : MonoBehaviour
{
    [Range(4.0f, 7.0f)]
    public float jumpForce = 5.0f;
    bool hasJumped = false;
    [SerializeField] float waitTime;
    public bool hasBall = false;


    //[SerializeField] public Transform jEnHand;
    Rigidbody2D rb;
    BallScript ball;
    BoxCollider2D bc;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        ball = GetComponent<BallScript>();
        ball = FindFirstObjectByType<BallScript>();
    }

    private void Update()
    {
        checkForJump();
    }

    private void checkForJump()
    {
        if (!hasJumped && hasBall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(WaitForJumpCooldown());
        }
    }

    private IEnumerator WaitForJumpCooldown()
    {
        hasJumped = true;
        yield return new WaitForSeconds(waitTime);
        hasJumped = false;
    }
}
   
  