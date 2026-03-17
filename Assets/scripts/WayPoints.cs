using Prime31;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

   
    public int offset = 0;
    public int MaxOffset = 6;
    public bool chase = false;
    public bool isChasing = false;
    public float speed = 3f;
    public float chaseRange = 5f;
    public int whichEnemy;
    public bool chaseDog = false;
    Transform HunterTransform;
    WayPoints HunterWaypoints;
    LevelGenerator lg;
    private Vector3 dir;
    public bool Iframs = false;
    private int currentWaypointIndex = 0;
    GameObject levlg;
    CharacterController2D Controller2D2;
    Transform player;
    public bool dogischasing = false;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 velocity2;
    public int enemyHp = 2;
    private SpriteRenderer enemyColler;
    public Color mainColer;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
       

        currentWaypointIndex = offset;
    }
    private void Awake()
    {
        lg = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        Controller2D2 = GetComponent<CharacterController2D>();
        Controller2D2.onControllerCollidedEvent += onControllerCollider2;
        Controller2D2.onTriggerEnterEvent += onTriggerEnterEvent2;
        //enemyHp //.= GameManager.Instance.enemyHealth();
        enemyColler = GetComponent<SpriteRenderer>();
        LevelGenerator.startgame += loaddateforenemy; 


    }
    void onControllerCollider2(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }
    void loaddateforenemy()
    {
        if (whichEnemy == 3)
        {
            if (chaseDog == true)
            {
                if (HunterTransform == null)
                {
                    HunterTransform = GameObject.FindGameObjectWithTag("hunter").transform;
                    HunterWaypoints = GameObject.FindGameObjectWithTag("hunter").GetComponent<WayPoints>();
                }
            }
        }
    }
    void onTriggerEnterEvent2(Collider2D col)
    {
        Debug.Log("hit");
        if (col.gameObject.tag == "Attack")
        {
          if(GameManager.Instance.vulnerable == true)
          {
                takeDamage(GameManager.Instance.FireBallDagame);
          }
        }
        

    }

    IEnumerator IFramsV()
    {
        
        yield return new WaitForSeconds(0.1f);
        Iframs = false;
    }
    private void Update()
    {
        if (GameManager.Instance.vulnerable == true)
        {
            enemyColler.color = Color.blue;

        }
        else
        {
            enemyColler.color = mainColer;
        }
       
    }
    void FixedUpdate()
    {
        if (whichEnemy == 1)
        {
            moveToWaypoint();
        }
        if (whichEnemy == 2)
        {
            chasing();
        }
        if (whichEnemy == 3)
        {
            if(chaseDog == true)
            {
               
                chasingdog();

            }
        }

    }   
    void moveToWaypoint()
    {
        
        
            if (lg.waypoints.Length == currentWaypointIndex) return;

            // Huidige waypoint
            Vector3 target = lg.waypoints[currentWaypointIndex];

            // Beweeg naar target
            dir = (target - transform.position).normalized;
            velocity2 = dir * speed;
            Controller2D2.move(velocity2 * Time.deltaTime);
       

        // Check of we er zijn
        if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                // Volgende waypoint
                currentWaypointIndex++;

                // Loop terug naar begin
                if (currentWaypointIndex >= offset + MaxOffset)
                {
                    currentWaypointIndex = offset;
                }
            }
       
    }
   
   public void takeDamage(int Damage)
    {
        if (Iframs == false)
        {
            enemyHp -= Damage;
            Iframs = true;
            StartCoroutine(IFramsV());
        }


        if (enemyHp <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.AddPoints(50);
            GameManager.Instance.enemyCount--;
        }
    }
    void chasing()
    {
        if (isChasing == false)
        {
            moveToWaypoint();
        }
        if (chase == true)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            // Debug.Log(distance);
            // Debug.Log(player.position);

            if (distance < chaseRange)
            {
                isChasing = true;
                dir = (player.position - transform.position).normalized;
                velocity2 = dir * speed;
                Controller2D2.move(velocity2 * Time.deltaTime);

            }
            else if (isChasing == true)
            {
                isChasing = false;
            }

        }
    }
    void chasingdog()
    {
        if (HunterWaypoints.isChasing == false && dogischasing == false)
        {
            float distance = Vector3.Distance(transform.position, HunterTransform.position);
            if (distance > 2f)
            {
                dir = (HunterTransform.position - transform.position).normalized;
                velocity2 = dir * speed;
                Controller2D2.move(velocity2 * Time.deltaTime);
            }
        }
        else if (HunterWaypoints.isChasing == true && dogischasing == false)
        { 
          
            float distance = Vector3.Distance(transform.position, player.position);
            
                
                dir = (player.position - transform.position).normalized;
                velocity2 = dir * speed;
                Controller2D2.move(velocity2 * Time.deltaTime);

            
        }

        if(HunterWaypoints.enemyHp <= 0)
        {
            dogischasing = true;
            float distance = Vector3.Distance(transform.position, player.position);


            dir = (player.position - transform.position).normalized;
            velocity2 = dir * speed;
            Controller2D2.move(velocity2 * Time.deltaTime);

        }
    }
    
}
