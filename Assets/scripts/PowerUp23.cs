using TMPro;
using UnityEngine;

public class PowerUp23 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int hp;
    public int damage;
    public float speed;
  
    void Start()
    {
        
    }
    private void Awake()
    {
       
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
     public void PowerUpAdd()
    {
        GameManager.Instance.AddHp += hp;
        GameManager.Instance.AddMaxHp += hp;
        GameManager.Instance.AddDamage += damage;
        GameManager.Instance.AddMageDamage += damage;
        GameManager.Instance.AddSpeed += speed;
        
        GameManager.Instance.addStats();
        
    }
    private void OnDisable()
    {
        hp = 0;
        speed = 0;
        damage = 0;
    }
}
