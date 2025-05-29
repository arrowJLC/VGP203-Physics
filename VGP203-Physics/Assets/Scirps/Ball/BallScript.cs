using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D cc;
    public Transform hand;

    JumpEN j;
    ChargeEn c;
    GameoverScript eg;

    [SerializeField] LineRenderer line;
    //Throw th;

    public Slider angleSlider;
    public Slider speedSlider;

    public TMP_Text anSliderText;
    public TMP_Text spSliderText;

    //TrajectoryPredictor tp;

    //public Rigidbody2D ball;
    //public Transform target;

    //public float throwForce = 10f;         // Adjust for power
    //public float throwAngle = 45f;

    // public float h = 2f; // height of the arc
    //public float gravity = 6f;


    public float launchAngle = 45f;  
    public float launchSpeed = 10f;       
    public float gravity = -9.81f;
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;

    private Vector2 initialVelocity;
    private Vector2 startPosition;
    private float timeElapsed = 0f;
    private bool isThrown = false;
    public bool playerHeld = false;
    public int pointsCount = 50;

    private float changeAmount = 0.7f;
    private float holdTime = 0.7f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        eg = FindFirstObjectByType<GameoverScript>();
        //JumpEN jumpE = Object.FindFirstObjectByType<JumpEN>();
        //j = Object.FindFirstObjectByType<JumpEN>();
        //c = Object.FindAnyObjectByType<ChargeEn>();
        //ChargeEn chargeE = Object.FindFirstObjectByType<ChargeEn>();
        //c = Object.FindFirstObjectByType<ChargeEn>();

        //JumpEN[] j = FindObjectsByType<JumpEN>();


        // PlayerController.OnControllerColliderHitInternal += OnPlayerControllerHit;

        // Physics2D.gravity = new Vector2(0, gravity);

        startPosition = transform.position;

        line = GetComponent<LineRenderer>();
        line.positionCount = pointsCount;
        launchAngle = angleSlider.value;
        launchSpeed = speedSlider.value;

        if (angleSlider)
        {
            angleSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(angleSlider.value);

            speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(speedSlider.value);
        }

    }

    void Update()
    {

        launchAngle = angleSlider.value;
        launchSpeed = speedSlider.value;

        Debug.Log($"Angle: {launchAngle}, Speed: {launchSpeed}");

        changePower();

        if (!isThrown && playerHeld)
        {
            DrawTrajectory(); // Update trajectory while held, before throw
        }


        if (isThrown)
        {
            timeElapsed += Time.deltaTime;

            float x = startPosition.x + initialVelocity.x * timeElapsed;
            float y = startPosition.y + initialVelocity.y * timeElapsed + 0.5f * gravity * Mathf.Pow(timeElapsed, 2);

            transform.position = new Vector2(x, y);
            // rb.MovePosition(new Vector2(x, y));

        }

        //if (isThrown)
        //{
        //    timeElapsed += Time.deltaTime;

        //    // Calculate next position manually
        //    float x = startPosition.x + initialVelocity.x * timeElapsed;
        //    float y = startPosition.y + initialVelocity.y * timeElapsed + 0.5f * gravity * Mathf.Pow(timeElapsed, 2);
        //    Vector2 nextPosition = new Vector2(x, y);

        //    Vector2 currentPosition = rb.position;
        //    Vector2 direction = nextPosition - currentPosition;
        //    float distance = direction.magnitude;

        //    // Cast ray from current to next position
        //    RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction.normalized, distance);

        //    if (hit.collider != null)
        //    {
        //        Debug.Log("Raycast hit: " + hit.collider.name);

        //        if (hit.collider.CompareTag("Ground"))
        //        {
        //            // Simulate bounce or stop
        //            Vector2 normal = hit.normal;
        //            initialVelocity = Vector2.Reflect(initialVelocity, normal) * 0.8f;

        //            // Restart physics sim from impact point
        //            startPosition = hit.point;
        //            timeElapsed = 0f;

        //            // Optionally stop if velocity too low
        //            if (initialVelocity.magnitude < 0.5f)
        //            {
        //                initialVelocity = Vector2.zero;
        //                isThrown = false;
        //                Debug.Log("Ball stopped after ground bounce.");
        //                // ed.endGAmeScript(); // Optional
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Move if clear
        //        rb.MovePosition(nextPosition);
        //    }
        //    Debug.DrawRay(currentPosition, direction.normalized * distance, Color.red, 0.1f);

        //}

    }

   // private void OnDestroy() => PlayerController.OnControllerColliderHitInternal -= OnPlayerControllerHit;

    void OnPlayerControllerHit(Collider2D playerCollider, ControllerColliderHit thingThatHitPlayer)
    {
        Debug.Log($"Player has been hit by {thingThatHitPlayer.collider.name}");
    }

    public void Hold(Collider2D playerCollider, Transform Hand)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        cc.isTrigger = true;

        playerHeld = true;

        JumpEN[] j = FindObjectsByType<JumpEN>(FindObjectsSortMode.None);
        
        foreach (JumpEN jump in j)
        {
            jump.hasBall = true;

        }



        ChargeEn[] c = FindObjectsOfType<ChargeEn>();

        foreach (ChargeEn charge in c)
        {
            charge.hasBall = true;
        }

        transform.SetParent(Hand);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        //Physics2D.IgnoreCollision(playerCollider, cc);
        //OnDrawGizmos();
        //DrawTrajectory();
    }

    public void Drop(Collider2D playerCollider/*, Vector3 playerForward*/)
    {
        transform.parent = null;
        cc.isTrigger = false;
        Physics2D.IgnoreCollision(playerCollider, cc, false);
        ThrowBall();
    }

    //public void Steal(Collider2D playerCollider)
    //{
    //    Debug.Log(" 1 S Called");
    //    transform.parent = null;
    //    cc.isTrigger = false;
    //}


    public void Release()
    {
        Debug.Log("Ball Released");

        transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        cc.isTrigger = false;
        playerHeld = false;
        isThrown = false;

        JumpEN[] j = FindObjectsByType<JumpEN>(FindObjectsSortMode.None);

        foreach (JumpEN jump in j)
        {
            jump.hasBall = false;

        }


        ChargeEn[] c = FindObjectsByType<ChargeEn>(FindObjectsSortMode.None);

        foreach (ChargeEn charge in c)
        {
            charge.hasBall = false;
            charge.enemyHeld = true;
        }
    }


    public void ThrowBall()
    {
        float angleRad = launchAngle * Mathf.Deg2Rad;

        float vx = launchSpeed * Mathf.Cos(angleRad);
        float vy = launchSpeed * Mathf.Sin(angleRad);

        initialVelocity = new Vector2(vx, vy);
        startPosition = transform.position;
        timeElapsed = 0f;
        isThrown = true;
        line.enabled = false;

        cc.isTrigger = false;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        JumpEN[] j = FindObjectsByType<JumpEN>(FindObjectsSortMode.None);

        //foreach (JumpEN jump in j)
        //{
        //    jump.hasBall = false;

        //}


        ChargeEn[] c = FindObjectsOfType<ChargeEn>();

        foreach (ChargeEn charge in c)
        {

            charge.hasBall = false;

        }
    }
    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[pointsCount];

        float angleRad = launchAngle * Mathf.Deg2Rad;
        Vector2 velocity = new Vector2 (launchSpeed * Mathf.Cos(angleRad), launchSpeed * Mathf.Sin(angleRad));

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i * timeStep;
            Vector3 pos = (Vector2)hand.position + velocity * t + 0.5f * new Vector2(0, gravity) * t * t;
            //
            //Fix this next line here
            if ((pos - hand.position).magnitude > 5f) break;
            positions[i] = pos;
        }

        line.SetPositions(positions);
    }

    void changePower()
    {
        if(playerHeld)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                speedSlider.value -= changeAmount;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                speedSlider.value += changeAmount;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                angleSlider.value += changeAmount;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                angleSlider.value -= changeAmount;
            }
            speedSlider.value = Mathf.Clamp(speedSlider.value, speedSlider.minValue, speedSlider.maxValue);
            angleSlider.value = Mathf.Clamp(angleSlider.value, angleSlider.minValue, angleSlider.maxValue);
        }

    }

    void OnSliderValueChanged(float sliderValue)
    {
        if (anSliderText)
            anSliderText.text = angleSlider.value.ToString();

        if (spSliderText)
            spSliderText.text = speedSlider.value.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("JumpEn"))
        {
            Debug.Log("Kinematic bounce off JumpEn");

            // Reflect velocity manually
            Vector2 normal = collision.contacts[0].normal;
            initialVelocity = Vector2.Reflect(initialVelocity, normal) * 0.8f; // simulate energy loss

            // Reset timer so position updates correctly from new direction
            startPosition = transform.position;
            timeElapsed = 0f;
            eg.ballStolen();
            //FindAnyObjectByType<GameoverScript>().ResetGame();
        }

        if(collision.collider.CompareTag("Ground") && isThrown)
        {
            Debug.Log("gameOver");
            Vector2 normal = collision.contacts[0].normal;
            initialVelocity = Vector2.Reflect(initialVelocity, normal) * 0.8f; // simulate energy loss

            // Reset timer so position updates correctly from new direction
            startPosition = transform.position;
            timeElapsed = 0f;

            if (initialVelocity.magnitude < 0.5f)
            {
                initialVelocity = Vector2.zero;
                isThrown = false;

            }
                eg.ballStolen();
        }
    }

    

    //void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying)
    //        return;

    //    Gizmos.color = Color.yellow;

    //    float angleRad = launchAngle * Mathf.Deg2Rad;

    //    float vx = launchSpeed * Mathf.Cos(angleRad);
    //    float vy = launchSpeed * Mathf.Sin(angleRad);

    //    Vector2 start = transform.position;

    //    for (int i = 0; i < trajectoryPoints; i++)
    //    {
    //        float t = i * timeStep;
    //        float x = start.x + vx * t;
    //        float y = start.y + vy * t + 0.5f * gravity * t * t;

    //        Gizmos.DrawSphere(new Vector2(x, y), 0.05f);

    //        line.SetPositions(positions);
    //    }
    //}
}



























































/*
 using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public float angularSpeed = 180f; // Degrees per second
    private float currentAngle = 0f;
    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            float deltaAngle = angularSpeed * Time.deltaTime;
            currentAngle += deltaAngle;

            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    public void SetAngularSpeed(float newSpeed)
    {
        angularSpeed = newSpeed;
    }
}

Attach this script to your 2D ball GameObject.

Call StartRotation() when you want the ball to start spinning (e.g. when thrown).

You can adjust angularSpeed directly or with a slider.

You can rotate clockwise (angularSpeed > 0) or counter-clockwise (angularSpeed < 0).

 Optional Enhancements:
Link rotation direction to movement direction.

Sync spin speed to launch speed for realism.

Add a friction factor to reduce angular speed over time.

Would you like the rotation to match how the ball rolls on the ground (like realistic rolling with slipping/friction)?
 
 
 */

/*
 using UnityEngine;

public class KinematicProjectile : MonoBehaviour
{
    public float launchAngle = 45f;        // In degrees
    public float launchSpeed = 10f;        // In units/second
    public float gravity = -9.81f;
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;

    private Vector2 initialVelocity;
    private Vector2 startPosition;
    private float timeElapsed = 0f;
    private bool isThrown = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isThrown)
        {
            timeElapsed += Time.deltaTime;

            float x = startPosition.x + initialVelocity.x * timeElapsed;
            float y = startPosition.y + initialVelocity.y * timeElapsed + 0.5f * gravity * Mathf.Pow(timeElapsed, 2);

            transform.position = new Vector2(x, y);
        }
    }

    public void Throw()
    {
        float angleRad = launchAngle * Mathf.Deg2Rad;

        float vx = launchSpeed * Mathf.Cos(angleRad);
        float vy = launchSpeed * Mathf.Sin(angleRad);

        initialVelocity = new Vector2(vx, vy);
        startPosition = transform.position;
        timeElapsed = 0f;
        isThrown = true;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.yellow;

        float angleRad = launchAngle * Mathf.Deg2Rad;

        float vx = launchSpeed * Mathf.Cos(angleRad);
        float vy = launchSpeed * Mathf.Sin(angleRad);

        Vector2 start = transform.position;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            float x = start.x + vx * t;
            float y = start.y + vy * t + 0.5f * gravity * t * t;

            Gizmos.DrawSphere(new Vector2(x, y), 0.05f);
        }
    }
}


 How to Use:
Attach this script to your GameObject.

Hit Play in Unity.

The trajectory preview will appear in the Scene view as small yellow spheres.

You can press a key to call Throw() if you want to trigger the motion:

csharp
Copy
Edit
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
        Throw();

    // existing motion code...
}
 Next Steps
Would you like to:

Enable this trajectory preview even in edit mode (without Play)?

Add bouncing behavior to the kinematic ball?

Let me know what you want to do next!
 
 
 */

//public void throwBall()
//{
//    ball.linearVelocity = CalculateVelocity();
//    Debug.Log("Throwing ball with velocity: " + ball.linearVelocity);
//}

//Vector2 CalculateVelocity()
//{
//    float displacementY = target.position.y - ball.position.y;
//    Vector2 displacementX = new Vector2(target.position.x - ball.position.x, 0);

//    // Calculate vertical component of velocity (to reach max height 'h')
//    Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);

//    // Time to reach the highest point
//    float timeUp = Mathf.Sqrt(-2 * h / gravity);

//    // Time from the highest point to the target
//    float timeDown = Mathf.Sqrt(2 * (displacementY - h) / gravity);

//    float totalTime = timeUp + timeDown;

//    // Horizontal velocity needed to reach the target in totalTime
//    Vector2 velocityX = displacementX / totalTime;

//    return velocityX + velocityY;
//}

//Vector2 CalculateVelocity()
//{
//    float displacementY = target.position.y - ball.position.y;
//    Vector2 displacementX = new Vector2(target.position.x - ball.position.x, 0);

//    if (gravity >= 0)
//    {
//        Debug.LogError("Gravity must be negative.");
//        return Vector2.zero;
//    }

//    if (h <= 0)
//    {
//        Debug.LogError("Arc height 'h' must be positive.");
//        return Vector2.zero;
//    }

//    // Check if target is below or at the launch height
//    float heightDifference = displacementY - h;

//    if (heightDifference < 0)
//    {
//        Debug.LogWarning("Target is too low for the given arc height. Lower 'h'.");
//        return Vector2.zero;
//    }

//    float velocityY = Mathf.Sqrt(-2 * gravity * h);
//    float timeUp = Mathf.Sqrt(-2 * h / gravity);
//    float timeDown = Mathf.Sqrt(2 * heightDifference / -gravity);
//    float totalTime = timeUp + timeDown;

//    Vector2 velocityX = displacementX / totalTime;
//    return velocityX + (Vector2.up * velocityY);
//}


//IEnumerator DropCooldown(Collider2D playerCollider)
//{
//    yield return new WaitForSeconds(3);

//    Physics2D.IgnoreCollision(cc, playerCollider, false);
//}
/*
 if (Input.GetKeyDown(KeyCode.Space))
{
    GetComponent<KinematicProjectile>().Throw();
}



using UnityEngine;

public class KinematicProjectile : MonoBehaviour
{
    public float launchAngle = 45f;        // In degrees
    public float launchSpeed = 10f;        // In units/second
    public float gravity = -9.81f;

    private Vector2 initialVelocity;
    private Vector2 startPosition;
    private float timeElapsed = 0f;
    private bool isThrown = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isThrown)
        {
            timeElapsed += Time.deltaTime;

            float x = startPosition.x + initialVelocity.x * timeElapsed;
            float y = startPosition.y + initialVelocity.y * timeElapsed + 0.5f * gravity * Mathf.Pow(timeElapsed, 2);

            transform.position = new Vector2(x, y);
        }
    }

    public void Throw()
    {
        float angleRad = launchAngle * Mathf.Deg2Rad;

        float vx = launchSpeed * Mathf.Cos(angleRad);
        float vy = launchSpeed * Mathf.Sin(angleRad);

        initialVelocity = new Vector2(vx, vy);
        startPosition = transform.position;
        timeElapsed = 0f;
        isThrown = true;
    }
}

 */

/*
 using UnityEngine;

public class BallKinematicScript : MonoBehaviour
{
    public Transform target;
    public float initialSpeed = 10f;
    public float gravity = -9.81f;

    private Vector2 startPos;
    private Vector2 velocity;
    private float timeElapsed = 0f;
    private bool isThrown = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isThrown)
        {
            timeElapsed += Time.deltaTime;

            float x = startPos.x + velocity.x * timeElapsed;
            float y = startPos.y + velocity.y * timeElapsed + 0.5f * gravity * timeElapsed * timeElapsed;

            transform.position = new Vector2(x, y);
        }
    }

    public void ThrowKinematic()
    {
        isThrown = true;
        timeElapsed = 0f;
        startPos = transform.position;

        Vector2 toTarget = target.position - transform.position;
        float angleRadians = 45 * Mathf.Deg2Rad; // or any other desired angle
        float distance = toTarget.magnitude;

        float vx = initialSpeed * Mathf.Cos(angleRadians);
        float vy = initialSpeed * Mathf.Sin(angleRadians);

        velocity = new Vector2(vx * Mathf.Sign(toTarget.x), vy);
    }
}

 */


//public Transform target;

//public float throwForce = 10f;         // Adjust for power
//public float throwAngle = 45f;

//public float h = 2f; // height of the arc
//public float gravity = -18f;

//private void Start()
//{
// Set gravity at the start of the game
//Physics2D.gravity = new Vector2(0, gravity);
//}


//Vector2 CalculateVelocity()
//{
//    float displacementY = target.position.y - ball.position.y;
//    Vector2 displacementX = new Vector2(target.position.x - ball.position.x, 0);

//    // Calculate vertical component of velocity (to reach max height 'h')
//    Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);

//    // Time to reach the highest point
//    float timeUp = Mathf.Sqrt(-2 * h / gravity);

//    // Time from the highest point to the target
//    float timeDown = Mathf.Sqrt(2 * (displacementY - h) / gravity);

//    float totalTime = timeUp + timeDown;

//    // Horizontal velocity needed to reach the target in totalTime
//    Vector2 velocityX = displacementX / totalTime;

//    return velocityX + velocityY;
//}

/*
 using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D cc;
    PushPlayer pp;
    Throw th;

    public Rigidbody2D ball;
    public Transform target;

    public float throwForce = 10f;
    public float h = 2f; // Arc height
    public float gravity = -18f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        pp = GetComponent<PushPlayer>();
        th = GetComponent<Throw>();

        PlayerController.OnControllerColliderHitInternal += OnPlayerControllerHit;

        // Set global gravity
        Physics2D.gravity = new Vector2(0, gravity);
    }

    private void OnDestroy()
    {
        PlayerController.OnControllerColliderHitInternal -= OnPlayerControllerHit;
    }

    void OnPlayerControllerHit(Collider2D playerCollider, ControllerColliderHit thingThatHitPlayer)
    {
        Debug.Log($"Player has been hit by {thingThatHitPlayer.collider.name}");
    }

    public void Hold(Collider2D playerCollider, Transform Hand)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        cc.isTrigger = true;

        transform.SetParent(Hand);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Physics2D.IgnoreCollision(playerCollider, cc);
    }

    public void Drop(Collider2D playerCollider)
    {
        transform.parent = null;
        cc.isTrigger = false;

        throwBall();
    }

    public void throwBall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // Enable physics
        rb.velocity = CalculateVelocity();      // Use calculated velocity
        Debug.Log("Throwing ball with velocity: " + rb.velocity);
    }

    Vector2 CalculateVelocity()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector2 displacementX = new Vector2(target.position.x - ball.position.x, 0);

        if (gravity >= 0 || h <= 0)
        {
            Debug.LogError("Invalid gravity or arc height.");
            return Vector2.zero;
        }

        float heightDifference = displacementY - h;

        if (heightDifference < 0)
        {
            Debug.LogWarning("Target is too low for the given arc height. Lower 'h'.");
            return Vector2.zero;
        }

        float velocityY = Mathf.Sqrt(-2 * gravity * h);
        float timeUp = Mathf.Sqrt(-2 * h / gravity);
        float timeDown = Mathf.Sqrt(2 * heightDifference / -gravity);
        float totalTime = timeUp + timeDown;

        Vector2 velocityX = displacementX / totalTime;
        return velocityX + (Vector2.up * velocityY);
    }

    void OnDrawGizmos()
    {
        if (target == null || ball == null) return;

        Gizmos.color = Color.red;

        Vector2 startPos = ball.position;
        Vector2 velocity = CalculateVelocity();

        int numPoints = 30;
        float timeStep = 0.1f;

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            Vector2 position = startPos + velocity * t + 0.5f * Physics2D.gravity * t * t;
            Gizmos.DrawSphere(position, 0.05f);
        }
    }

    IEnumerator DropCooldown(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreCollision(cc, playerCollider, false);
    }
}

void OnDrawGizmos()
{
    if (target == null || ball == null)
        return;

    Gizmos.color = Color.red;

    Vector2 startPos = ball.position;
    Vector2 velocity = CalculateVelocity(); // Same as used for the actual throw

    int numPoints = 30;
    float timeStep = 0.1f;

    for (int i = 0; i < numPoints; i++)
    {
        float t = i * timeStep;
        Vector2 position = startPos + velocity * t + 0.5f * Physics2D.gravity * t * t;
        Gizmos.DrawSphere(position, 0.05f);
    }
}

 */