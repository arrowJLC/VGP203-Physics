using UnityEngine;

public class ChnageColor : MonoBehaviour
{
    public Color newcolor;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = newcolor;
    }
}
