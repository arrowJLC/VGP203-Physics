//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class TrajectoryDrawer : MonoBehaviour
//{
//    public Transform hand;             // The point from which the projectile is launched
//    public float launchAngle = 45f;    // In degrees
//    public float launchSpeed = 10f;    // Speed of the projectile
//    public int pointsCount = 50;       // Number of points in the line
//    public float timeStep = 0.1f;      // Time between each point

//    private LineRenderer line;

//    void Start()
//    {
//        line = GetComponent<LineRenderer>();
//        line.positionCount = pointsCount;
//    }

//    void Update()
//    {
//        DrawTrajectory();
//    }

//    void DrawTrajectory()
//    {
//        Vector3[] positions = new Vector3[pointsCount];

//        float angleRad = launchAngle * Mathf.Deg2Rad;
//        Vector2 velocity = new Vector2(
//            launchSpeed * Mathf.Cos(angleRad),
//            launchSpeed * Mathf.Sin(angleRad)
//        );

//        for (int i = 0; i < pointsCount; i++)
//        {
//            float t = i * timeStep;
//            Vector3 pos = (Vector2)hand.position + velocity * t + 0.5f * Physics2D.gravity * t * t;
//            positions[i] = pos;
//        }

//        line.SetPositions(positions);
//    }
//}


////using UnityEngine;
////using System.Collections.Generic;

////[RequireComponent(typeof(LineRenderer))]
////public class TrajectoryPredictor : Throw
////{
////    public Rigidbody2D projectileRb;
////    //public float throwForce = 10f;
////   // public float throwAngle = 45f;
////    public int numPoints = 50;
////    public float timeBetweenPoints = 0.1f;
////    public Transform Hand;

////    private LineRenderer lineRenderer;

////    void Start()
////    {
////        lineRenderer = GetComponent<LineRenderer>();
////        lineRenderer.positionCount = numPoints;

////        // Optionally predict at start
////        PredictTrajectory();
////    }

////    public void PredictTrajectory()
////    {
////        Debug.Log("Traject active");
////        Vector3[] points = new Vector3[numPoints];
////        Vector3 startPos = Hand.position;

////        // Direction from angle
////        Vector3 direction = Quaternion.Euler(-throwAngle, 0, 0) * Vector3.forward;
////        Vector3 velocity = direction * throwForce;

////        for (int i = 0; i < numPoints; i++)
////        {
////            float t = i * timeBetweenPoints;
////            Vector3 point = startPos + velocity * t + 0.5f * Physics.gravity * t * t;
////            points[i] = point;
////        }

////        lineRenderer.SetPositions(points);
////    }
////}
