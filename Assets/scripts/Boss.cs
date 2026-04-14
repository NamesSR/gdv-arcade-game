using Prime31;
using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp = 3;
    CharacterController2D CCD2;
    public GameObject FireBallEnemyprefab;
    Transform player;
    LevelGenerator lg;
    Vector3 dir;
    Vector3[] shootpos = new UnityEngine.Vector3[5] 
    {new Vector3(-1f,-0.2f,0f), 
     new Vector3(-0.7f,-0.7f,0f),
     new Vector3(0f, -1f,0f),
     new Vector3(1f,-0.2f,0f),
     new Vector3(0.7f,-0.7f,0f),


    };

   
    shoot shoot;
   public float nextAttackTime = 0f;
    public float attackrate = 0.75f;
   public  float AttackingAttackRate = 0.75f;
    public float normalAttackRate = 1.5f;
   public  float panicAttackRate = 0.5f;
    
    public enum StateMachine
    {
        normal,
        attacking,
        panic

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StateMachine currentState;


    void Start()
    {
        currentState = StateMachine.normal;
    }
    private void Awake()
    {
        CCD2 = GetComponent<CharacterController2D>();
        CCD2.onTriggerEnterEvent += onTriggerEnterEvent;
        CCD2.onTriggerStayEvent += onTriggerStayEvent;
        LevelGenerator.startgame += loaddateforenemy;
    }

    void loaddateforenemy()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
        lg = GameObject.FindGameObjectWithTag("levelgen").GetComponent<LevelGenerator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
           
            GameManager.Instance.shown();
            Debug.Log("show");
        }
        switch (currentState)
        {
            case StateMachine.normal:
                attackrate = normalAttackRate;
                normal();
                break;
            case StateMachine.attacking:
                attackrate = AttackingAttackRate;
                attacking();
                break;
            case StateMachine.panic:
                attackrate = panicAttackRate;
                panic();
                break;

        }
        if(hp == 3 && currentState != StateMachine.normal)
        {
            currentState = StateMachine.normal;
        }
        if (hp == 2 && currentState != StateMachine.attacking)
        {
            currentState = StateMachine.attacking;
        }
        if (hp == 1 && currentState != StateMachine.panic)
        {
            currentState = StateMachine.panic;
        }
    }
   void onTriggerEnterEvent(Collider2D col)
    {
        if (col.tag == "Attack")
        {
            if (GameManager.Instance.bossIsVulnerable == true)
            {

                takedamage();
            }
        }
       

    }
    void onTriggerStayEvent(Collider2D col)
    {
        Debug.Log("dasdads23");
        if (col.tag == "Wall")
        {
            Destroy(col.gameObject);
        }
    }
   public void takedamage()
    {
        hp -= 1 * GameManager.Instance.extraBossDamageX;
        GameManager.Instance.bossIsVulnerable = false;
        GameManager.Instance.hit234 = true;
        lg.SpawnPowerOrbRandom(GameManager.Instance.orbspawnAmount);
        if(hp <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.shown();

            GameManager.Instance.bosscount--;
            
        }
    }
    void normal()
    {
        if (Time.time >= nextAttackTime)
        {
             dir = (player.position - transform.position).normalized;
           
            var pos = transform.position + new Vector3(0,-2,0);
            var FireBallEnemy = Instantiate(FireBallEnemyprefab, pos, Quaternion.identity);
                shoot = FireBallEnemy.GetComponent<shoot>();
            shoot.dir2 = dir;
            nextAttackTime = Time.time + 1 / attackrate;

        }
    }
   void attacking()
    {
        if (Time.time >= nextAttackTime)
        {
            for (int i = 0; i < shootpos.Length; i++)
            {
                var pos = transform.position + shootpos[i];
                var FireBallEnemy = Instantiate(FireBallEnemyprefab, pos, Quaternion.identity);
                shoot = FireBallEnemy.GetComponent<shoot>();
                shoot.dir2 = shootpos[i];
                
            }
            nextAttackTime = Time.time + 1 / attackrate;
        }
    }
    void panic()
    {
        if (Time.time >= nextAttackTime)
        {
            int WA = UnityEngine.Random.Range(0, 2);
             if (WA == 0)
             {
                 dir = (player.position - transform.position).normalized;
                var pos = transform.position + new Vector3(0, -2, 0);
                var FireBallEnemy = Instantiate(FireBallEnemyprefab, pos, Quaternion.identity);
                shoot = FireBallEnemy.GetComponent<shoot>();
                 shoot.dir2 = dir;
                 nextAttackTime = Time.time + 1 / attackrate;
             }
            else
            {
                for (int i = 0; i < shootpos.Length; i++)
                {
                    var pos = transform.position + shootpos[i];
                    var FireBallEnemy = Instantiate(FireBallEnemyprefab, pos, Quaternion.identity);
                    shoot = FireBallEnemy.GetComponent<shoot>();
                    shoot.dir2 = shootpos[i];
                    nextAttackTime = Time.time + 1 / attackrate;
                }
            }
        }
    }
}
