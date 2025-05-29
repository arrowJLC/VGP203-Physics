using UnityEngine;

public class FootBall : MonoBehaviour
{

    public float speed = 4f;
    public float resetPositionX = 10f;
    public float startPositionX = -10f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > resetPositionX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = startPositionX;
            transform.position = newPosition;
        }
    }
}

