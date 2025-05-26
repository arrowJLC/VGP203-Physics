//////using UnityEngine;
//////using System.Collections;

//////public class Throw : MonoBehaviour
//////{
//////    public Rigidbody2D ball;
//////    public Transform target;

//////    public float h = 25;
//////    //13
//////    public float gravity = -18;
//////    //9.81



//////    public void throwBall()
//////    {
//////        Physics2D.gravity = Vector2.up * gravity;
//////        //ball.useGravity = true;
//////        ball.angularVelocity = CalculateVelovity();

//////        Debug.Log("");
//////        print (CalculateVelovity());
//////    }
//////    Vector2 CalculateVelovity()
//////    {
//////        float displacmentY = target.position.y - ball.position.y;
//////        Vector2 displacmentX = new Vector2(target.position.x - ball.position.x, 0);

//////        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);
//////        Vector2 velocityX = displacmentX / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacmentY - h) / gravity));

//////        return velocityX + velocityY;
//////    }


//////}

//using UnityEngine;

//public class Throw : MonoBehaviour
//{
//    public Rigidbody2D ball;
//    public Transform target;

//    public float throwForce = 10f;         // Adjust for power
//    public float throwAngle = 45f;

//    public float h = 2f; // height of the arc
//    public float gravity = -18f;

//    private void Start()
//    {
//        // Set gravity at the start of the game
//        Physics2D.gravity = new Vector2(0, gravity);
//    }

//    public void throwBall()
//    {
//        ball.linearVelocity = CalculateVelocity();
//        Debug.Log("Throwing ball with velocity: " + ball.linearVelocity);
//    }

//    Vector2 CalculateVelocity()
//    {
//        float displacementY = target.position.y - ball.position.y;
//        Vector2 displacementX = new Vector2(target.position.x - ball.position.x, 0);

//        // Calculate vertical component of velocity (to reach max height 'h')
//        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);

//        // Time to reach the highest point
//        float timeUp = Mathf.Sqrt(-2 * h / gravity);

//        // Time from the highest point to the target
//        float timeDown = Mathf.Sqrt(2 * (displacementY - h) / gravity);

//        float totalTime = timeUp + timeDown;

//        // Horizontal velocity needed to reach the target in totalTime
//        Vector2 velocityX = displacementX / totalTime;

//        return velocityX + velocityY;
//    }
//}






//using UnityEngine;

//public class Throw : MonoBehaviour
//{
//    [SerializeField] Transform Hand;
//    [SerializeField] LineRenderer line;

//    [SerializeField] float throwForce = 1.5f;
//    [SerializeField] float st = 0.05f;
//    [SerializeField] int cs = 15;

//    Vector2 velocity, startMousePos, currentMousePos;


//    private void Update()
//    {
//        if(Input.GetMouseButtonDown(0))
//        {
//            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        }

//        if(Input.GetMouseButton(0))
//        {
//            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            velocity = (startMousePos - currentMousePos) * throwForce;

//            DrawTrajLine();
//        }
        
//    }

//    void DrawTrajLine()
//    {
//        Vector3[] positions = new Vector3[cs];
//        for(int i = 0; i < cs; i++)
//        {
//            float t = i * st;
//            Vector3 pos = (Vector2)Hand.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

//            positions[i] = pos;
//        }

//        line.SetPositions(positions);
//    }

//    void fire()
//    {
//        Transform pr = Instantiate(Hand, Hand.position, Quaternion.identity);

//        pr.GetComponent<Rigidbody2D>().linearVelocity = velocity;

//    }
//}