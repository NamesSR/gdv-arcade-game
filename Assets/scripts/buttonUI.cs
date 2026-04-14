using UnityEngine;

public class buttonUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if ((GameManager.Instance.currentState == GameState.Menu && this.gameObject.tag == "menu"))
        {
            this.gameObject.SetActive(true); 
        }
        else
        {
            
                this.gameObject.SetActive(false);
            
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverButton()
    {
        if (GameManager.Instance.currentState == GameState.GameOver && this.gameObject.tag == "GameOver")
        {
            this.gameObject.SetActive(true);
        }
    }
    public void StartGameButton()
    {
        if (GameManager.Instance.currentState == GameState.Menu && this.gameObject.tag == "menu")
        {
            this.gameObject.SetActive(true);
        }
    }

}
