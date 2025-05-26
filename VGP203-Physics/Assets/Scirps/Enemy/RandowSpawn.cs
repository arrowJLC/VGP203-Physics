using UnityEngine;

public class RandowSpawn : MonoBehaviour
{
    public GameObject[] ENcharger;
    public int randomN;

    private void Start()
    {
        randomN = Random.Range(-2, 2);
        spawnUpperEN();
        //spawnMiddleN();
        spawnLowerEN();
    }

    private void spawnMiddleN()
    {
        Vector3 startPosition = new Vector3(1, -2.256672f, 0f);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, ENcharger.Length);

            // Offset each spawn by 2 units on the X-axis
            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    private void spawnLowerEN()
    {
        Vector3 startPosition = new Vector3(-1f, -3.256672f, 0f); // Starting position

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, ENcharger.Length);

            // Offset each spawn by 2 units on the X-axis
            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    void spawnUpperEN()
    {
        Vector3 startPosition = new Vector3(randomN, -1.256672f, 0f); // Starting position

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, ENcharger.Length);

            // Offset each spawn by 2 units on the X-axis
            Vector3 spawnPosition = startPosition + new Vector3(i * 3f, 0f, 0f);

            Instantiate(ENcharger[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}


