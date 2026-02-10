using UnityEngine;
using UnityEngine.SceneManagement;

public class scoremanager : MonoBehaviour
{
    public int score = 0;
    public static scoremanager Instance;

    void Start()
    {
        
    }
    private void Awake()
    {
        // controleren of er al een ScoreManager bestaat
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // dit is nu de enige ScoreManager in de scene
        Instance = this;
    }

    void Update()
    {
        
    }

    public void AddPoints(int points)
    {
        score = score + points;
        Debug.Log("Score: " + score);
    }
}
