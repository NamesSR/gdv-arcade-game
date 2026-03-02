using UnityEditor;
using UnityEngine;
public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
 public int score = 0;
 public int hp = 3;
 public int enemyCount;
 public int powerOrbCount;
 public int level = 0;
 public bool vulnerable = false;
 public int Ehp = 2;
 public int BossHp = 3;
 public int FireBallDagame = 1;
 public GameObject menuPanel;
 public GameObject pausePanel;
 public GameObject gameOverPanel;
 public LevelGenerator LevelGen;


    public static GameManager Instance;

  public GameState currentState = GameState.Menu;

   void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Menu:
                Time.timeScale = 1;
                menuPanel.SetActive(true);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                // Toon menu UI
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                // Verberg menu's
                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                // Toon pause menu
                menuPanel.SetActive(false);
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                // Toon game over scherm
                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
        }
    }

   void Start()
    {
        SetState(GameState.Menu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                SetState(GameState.Paused);
            else if (currentState == GameState.Paused)
                SetState(GameState.Playing);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == GameState.Menu)
            {
                SetState(GameState.Playing);
                LevelGen.GenerateLevel();
            }
            if (currentState == GameState.GameOver)
            {
                SetState(GameState.Playing);
            }
        }
        if(enemyCount > 0 && powerOrbCount <= 0 && vulnerable == false)
        {
            SetState(GameState.GameOver);
        }
        if (hp <= 0)
        {
            SetState(GameState.GameOver);
        }
    }
    
    public void AddPoints(int points)
    {
        score = score + points;
      //  Debug.Log("Score: " + score);
    }
    public void TakeDamage(int damage)
    {
        if (hp > 0)
        {
            hp = hp - damage;
        }
        
        if(hp < 1)
        {
            Debug.Log("game over");
        }
    }
    public void enemycountAdd(int count)
    {
        enemyCount = enemyCount + count;
    }
    public void powerOrbCountAdd(int count)
    {
        powerOrbCount = powerOrbCount + count;
    }
    public int enemyHealth()
    {
        return Ehp;
    }
    public void StartGame() => SetState(GameState.Playing);
    public void PauseGame() => SetState(GameState.Paused);
    public void GameOver() => SetState(GameState.GameOver);
}
