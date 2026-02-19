using UnityEngine;

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

    public static GameManager Instance;

  
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        if(enemyCount > 0 && powerOrbCount < 1 && vulnerable == false)
        {
            // Lose game
        }
    }
    public void AddPoints(int points)
    {
        score = score + points;
        Debug.Log("Score: " + score);
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
}
