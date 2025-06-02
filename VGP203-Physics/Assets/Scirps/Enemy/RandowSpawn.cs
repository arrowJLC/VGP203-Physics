//using UnityEngine;

//public class RandowSpawn : MonoBehaviour
//{
//    public GameObject[] ENcharger;
//    public int randomN;

//    private void Start()
//    {
//        randomN = Random.Range(-2, 2);
//        spawnUpperEN();
//        spawnMiddleN();
//        spawnLowerEN();
//    }

//    private void spawnMiddleN()
//    {
//        Vector3 startPosition = new Vector3(1, -2.64f, 0f);

//        for (int i = 0; i < 2; i++)
//        {
//            int randomIndex = Random.Range(0, ENcharger.Length);

//            // Offset each spawn by 2 units on the X-axis
//            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

//            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
//        }
//    }

//    private void spawnLowerEN()
//    {
//        Vector3 startPosition = new Vector3(-1f, -3.69f, 0f); // Starting position

//        for (int i = 0; i < 3; i++)
//        {
//            int randomIndex = Random.Range(0, ENcharger.Length);

//            // Offset each spawn by 2 units on the X-axis
//            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

//            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
//        }
//    }

//    void spawnUpperEN()
//    {
//        Vector3 startPosition = new Vector3(randomN, -1.71f, 0f); // Starting position

//        for (int i = 0; i < 3; i++)
//        {
//            int randomIndex = Random.Range(0, ENcharger.Length);

//            // Offset each spawn by 2 units on the X-axis
//            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

//            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
//        }
//    }
//}

//using UnityEngine;
//using System.Collections.Generic;

//public class RandowSpawn : MonoBehaviour
//{
//    public GameObject[] ENcharger;

//    private List<float> usedXPositions = new List<float>();

//    private void Start()
//    {
//        spawnUpperEN();
//        spawnMiddleN();
//        spawnLowerEN();
//    }

//    private void spawnMiddleN()
//    {
//        float yPosition = -2.64f;
//        SpawnRow(2, yPosition);
//    }

//    private void spawnLowerEN()
//    {
//        float yPosition = -3.69f;
//        SpawnRow(3, yPosition);
//    }

//    private void spawnUpperEN()
//    {
//        float yPosition = -1.71f;
//        SpawnRow(3, yPosition);
//    }

//    private void SpawnRow(int count, float yPos)
//    {
//        int attempts = 0;
//        int maxAttempts = 100; // avoid infinite loops
//        int spawned = 0;

//        while (spawned < count && attempts < maxAttempts)
//        {
//            attempts++;

//            int randomIndex = Random.Range(0, ENcharger.Length);
//            float randomX = Random.Range(-4, 5); // Adjust range as needed
//            randomX = Mathf.Round(randomX * 10f) / 10f; // Round to 1 decimal to avoid float precision issues

//            // Check if this X is at least 3 units away from all used X positions
//            bool isFarEnough = true;
//            foreach (float usedX in usedXPositions)
//            {
//                if (Mathf.Abs(usedX - randomX) < 3f)
//                {
//                    isFarEnough = false;
//                    break;
//                }
//            }

//            if (isFarEnough)
//            {
//                Vector3 spawnPos = new Vector3(randomX, yPos, 0f);
//                Instantiate(ENcharger[randomIndex], spawnPos, Quaternion.identity);
//                usedXPositions.Add(randomX);
//                spawned++;
//            }
//        }
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class RandowSpawn : MonoBehaviour
{
    public GameObject[] ENcharger;

    private List<float> availableXPositions = new List<float>();

    private void Start()
    {
        for (float x = -5f; x <= 6f; x += 3f)
        {
            availableXPositions.Add(x);
        }

        spawnUpperEN();
        spawnMiddleN();
        spawnLowerEN();
    }

    private void spawnUpperEN()
    {
        spawnRow(3, -1.71f);
    }

    private void spawnMiddleN()
    {
        spawnRow(2, -2.64f);
    }

    private void spawnLowerEN()
    {
        spawnRow(3, -3.69f);
    }

    private void spawnRow(int count, float yPos)
    {
        List<float> rowXPositions = new List<float>(availableXPositions);

        for (int i = 0; i < count; i++)
        {
            if (rowXPositions.Count == 0)
            {
                Debug.LogWarning("No more available X positions for this row.");
                break;
            }

            int randomIndex = Random.Range(0, rowXPositions.Count);
            float xPos = rowXPositions[randomIndex];
            rowXPositions.RemoveAt(randomIndex);

            int prefabIndex = Random.Range(0, ENcharger.Length);
            Instantiate(ENcharger[prefabIndex], new Vector3(xPos, yPos, 0f), Quaternion.identity);
        }
    }
}



