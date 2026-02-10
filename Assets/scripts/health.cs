using UnityEngine;

public class health : MonoBehaviour
{
    public int hp = 3;

    public static health Instans;
    void Start()
    {
        TakeDamage(1);
    }
    private void Awake()
    {
        // controleren of er al een ScoreManager bestaat
        if (Instans != null && Instans != this)
        {
            Destroy(gameObject);
            return;
        }

        // dit is nu de enige ScoreManager in de scene
        Instans = this;
    }
    // Update is called once per frame
    void Update()
    {
        
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
}
