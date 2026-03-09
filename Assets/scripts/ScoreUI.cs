using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreUpdate(int score)
    {
        scoreText.text = $"SCORE: {score}";
        
    }
   public void Resetscore()
    {
        scoreText.text = "0";
    }
}
