//using System;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class PlayerController : MonoBehaviour
//{
//    public enum directionType
//    {
//        Up,
//        Down,
//        None
//    }

//    private directionType direction = directionType.None;


//    private Vector2 pStartPos, pEndPos, difference;

//    bool canJump = true;
//    bool touchDownActive = false;
//    public bool isMoving = false;

//    //AnimController animController;
//    protected Animator anim;

//    [SerializeField] private Transform Hand;
//    BallScript ball = null;

//    //Action for ControllerColliderHit - Assumes that there is only one controller in the scene
//    public static event Action<Collider2D, ControllerColliderHit> OnControllerColliderHitInternal;

//    [Range(1, 5)]
//    public float playerSpeed = 1.5f;

//    public bool isGrounded = false;

//    Rigidbody2D rb;
//    Collider2D pc;
//    // PushPlayer pp;

//    private void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        pc = rb.GetComponent<Collider2D>();
//        anim = GetComponent<Animator>();
//        //animController = GetComponent<AnimController>();
//        //animController.PlayAnim("Idle", 0.1f);

//        #region testcode
//        rb.bodyType = RigidbodyType2D.Kinematic;
//        #endregion

//    }


//    void Update()
//    {
//        float hinput = Input.GetAxis("Horizontal");
//        isMoving = Mathf.Abs(hinput) > 0.01f;

//        if (isMoving)
//        {
//            //animController.PlayAnim("Walk", 0.1f);
//            anim.SetTrigger("isWalking");
//            rb.linearVelocity = new Vector2(hinput * playerSpeed, rb.linearVelocity.y);
//        }

//       // else anim.SetTrigger("isIdle");
//            //animController.PlayAnim("Idle", 0.1f);


//        if (Input.GetKeyDown(KeyCode.E) && ball != null)
//        {
//            ball.Drop(pc);
//            ball = null;
//        }

//        if(Input.GetKeyDown(KeyCode.W))
//        {
//            //startPos = endPos = Input
//            //pStartPos =  pEndPos = input
//        }

//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            //endPos = input
//            //pEndPos = input.mousePosition
//        }
//    }

//    void playerMoveDir()
//    {
//        direction = directionType.None;

//        difference = pEndPos - pStartPos;

//        if(difference.magnitude > Screen.width)
//        {
//            if(difference.x > 0)
//            {
//                direction = directionType.Up;
//            }

//            if (difference.x < 0)
//            {
//                direction = directionType.Down;
//            }
//        }

//    }

//    void dirMethod(directionType direction)
//    {
//        switch (direction)
//        {
//            case directionType.Up:
//                endXPos = transform.position.y + 3;
//                break;

//            case directionType.Down:
//                endXPos += transform.position.y - 3;
//                break;
//        }

//        endXPos = Mathf.Clamp(endXPos, -3, 3);
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.collider.CompareTag("Ball") && ball == null)
//        {
//            ball = collision.gameObject.GetComponent<BallScript>();
//            //ball.Hold(GetComponent<Collider2D>(), Hand);

//            //ball = collision.collider.GetComponent<BallScript>();
//            if (ball != null)
//            {
//                //animController.PlayAnim("Throw", 0.1f);
//                anim.SetTrigger("beforeThrow");

//                ball.Hold(pc, Hand);

//                //pp.startPush();
//            }
//        }
//    }

//    private float endXPos = 0f;


//}




































////void Update()
////{
////    if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
////    {
////        isMoving = true;
////    }

////    if(isMoving)
////    {
////        animController.PlayAnim("Walk", 0.1f);
////        float hinput = Input.GetAxis("Horizontal");
////        rb.linearVelocity = new Vector2(hinput * playerSpeed, rb.linearVelocity.y);
////    }

////    // float hinput = Input.GetAxis("Horizontal");
////    //rb.linearVelocity = new Vector2(hinput * playerSpeed, rb.linearVelocity.y);
////    //bool isMoving = true;

////    //if (isMoving) animController.PlayAnim("Walk", 0.1f);

////    if (Input.GetKeyDown(KeyCode.E) && ball != null)
////    {
////        ball.Drop(pc/*, transform.right*/);
////        ball = null;    
////    }

////}
///



using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private enum DirectionType
    {
        Up,
        Down,
        None
    }

    private Rigidbody2D rb;
    private Collider2D pc;
    private Animator anim;

    [SerializeField] private Transform Hand;
    private BallScript ball = null;

    [Range(1f, 5f)]
    public float playerSpeed = 1.5f;

    public bool isMoving = false;
    public bool isGrounded = false;

    private float targetYPos = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = rb.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        //rb.bodyType = RigidbodyType2D.Kinematic;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;

        targetYPos = transform.position.y;
    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        isMoving = Mathf.Abs(hInput) > 0.01f;

        if (isMoving)
        {
           // anim.SetTrigger("isWalking");
            rb.linearVelocity = new Vector2(hInput * playerSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            // anim.SetTrigger("isIdle");
        }

        if (Input.GetKeyDown(KeyCode.E) && ball != null)
        {
            ball.Drop(pc);
            ball = null;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerDirection(DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerDirection(DirectionType.Down);
        }
    }

    private void playerDirection(DirectionType direction)
    {
        switch (direction)
        {
            case DirectionType.Up:
                targetYPos = transform.position.y + 1;
                break;
            case DirectionType.Down:
                targetYPos = transform.position.y - 1;
                break;
        }

        targetYPos = Mathf.Clamp(targetYPos, -3.33f, -1.33f);

        transform.position = new Vector2(transform.position.x, targetYPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball") && ball == null)
        {
            ball = collision.collider.GetComponent<BallScript>();

            if (ball != null)
            {
                //anim.SetTrigger("beforeThrow");
                ball.Hold(pc, Hand);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.CompareTag("Wall"))
    //    {
    //        Debug.Log("Hit wall");
    //        rb.bodyType = RigidbodyType2D.Dynamic;
    //        rb.gravityScale = 0;

    //    }
    //}
}
