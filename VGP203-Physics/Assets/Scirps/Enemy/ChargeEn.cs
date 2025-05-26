using System;
using UnityEngine;
using UnityEngine.XR;

public class ChargeEn : MonoBehaviour
{
    [Range(-4, -0.1f)]
    [SerializeField] float speed = -4.0f;
    bool hasRan = false;
    public bool hasBall = false;
    public bool enemyHeld = false;

    [SerializeField] private Transform EnHand;
    //public static event Action<Collider2D, ControllerColliderHit> OnControllerColliderHitInternal;
    Rigidbody2D rb;
    BallScript ball;
    BoxCollider2D bc;
    GameoverScript eg;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //ball = GetComponent<BallScript>();
        ball = FindFirstObjectByType<BallScript>();

        bc = GetComponent<BoxCollider2D>();

        eg = FindFirstObjectByType<GameoverScript>();
    }

    private void Update()
    {
        checkForRun();
    }

    void checkForRun()
    {
        if (!hasRan && hasBall)
        {
            //rb.linearVelocity = new Vector2(rb.linearVelocity.y, speed);
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            hasRan = true;
        }

        if (!hasBall && hasRan)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
    }

    void checkForSteal()
    {
        if (hasBall && hasRan && !enemyHeld)
        {
            ball.Release();
            Debug.Log("ball dropped");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            if (ball != null && enemyHeld)
            {
                ball.Hold(bc, EnHand);
                eg.ballStolen();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} hit the player");
            checkForSteal();
        }
    }


}
























//    //void checkForSteal(Collision2D collision)
//    //{

//    //    if(hasRan && hasBall)
//    //    {
//    //        if (collision.collider.CompareTag("Player"))
//    //        {
//    //            ball = collision.gameObject.GetComponent<BallScript>();
//    //            Debug.Log("Steel called");

//    //            //ball.Steal();

//    //            if (ball != null)
//    //            {
//    //                ball.Hold(bc, EnHand);
//    //                //ed.endGAmeScript();
//    //            }
//    //        }
//    //    }
//    //}

//    //public void StealBallFrom(BallScript targetBall)
//    //{
//    //    targetBall.Release();
//    //    targetBall.Hold(bc, EnHand);
//    //    ball = targetBall;
//    //}

//    void checkForStealE()
//    {
//        //Debug.Log("Steal called");

//        if (hasRan && hasBall)
//        {
//            if (collision.collider.CompareTag("Player"))
//            {
//                BallScript playerBall = collision.gameObject.GetComponentInChildren<BallScript>();
//                Debug.Log("ene hit player");

//                if (playerBall != null)
//                {
//                    playerBall.Release(); // Force player to drop the ball
//                    //StealBallFrom(playerBall);

//                    // Now enemy takes it
//                    playerBall.Hold(bc, EnHand);
//                    ball = playerBall; // Update enemy's internal reference
//                }
//            }
//        }
//    }

//    void checkForSteal()
//    {
//        if (hasRan && hasBall)
//        {

//        }
//    }
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        checkForStealE(collision);

//        if (collision.collider.CompareTag("Ball") && ball == null)
//        {
//            ball = collision.gameObject.GetComponent<BallScript>();



//            if (ball != null)
//            {
//                ball.Hold(bc, EnHand);
//            }
//        }
//    }
//}
