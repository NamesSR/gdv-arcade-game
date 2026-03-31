using UnityEngine;

public class show : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        Boss.Upragades += shown;
    }
    void shown()
    {
        gameObject.SetActive(true);
    }
}
