using Prime31;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

   // public Rigidbody2D rigidBod;
    public int offset = 0;
    public bool chase = false;
    public bool isChasing = false;
    public float speed = 3f;
    public float chaseRange = 5f;
    LevelGenerator lg;
    private Vector3 dir;
    private int currentWaypointIndex = 0;
    GameObject levlg;
    CharacterController2D Controller2D2;
    Transform player;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 velocity2;
    public int enemyHp;
    private void Start()
    {
        levlg = GameObject.Find("LevelGenerator");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lg = levlg.GetComponent<LevelGenerator>();
        

        currentWaypointIndex = offset;
    }
    private void Awake()
    {
        Controller2D2 = GetComponent<CharacterController2D>();
        Controller2D2.onControllerCollidedEvent += onControllerCollider2;
        Controller2D2.onTriggerEnterEvent += onTriggerEnterEvent2;
        enemyHp = GameManager.Instance.enemyHealth();
    }
    void onControllerCollider2(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }
    void onTriggerEnterEvent2(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
          if(GameManager.Instance.vulnerable == true)
            {
                enemyHp = enemyHp - 1;
                if(enemyHp < 1)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
    void FixedUpdate()
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
                if (currentWaypointIndex >= offset + 6)
                {
                    currentWaypointIndex = offset;
                }
            }
       
    }
   
   
}
