using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

    public Rigidbody2D rigidBod;
    public int offset = 0;
    public bool chase = false;
    public bool isChasing = false;
    public float speed = 3f;
    public float chaseRange = 5f;
    LevelGenerator lg;
    private int currentWaypointIndex = 0;
    GameObject levlg;
    
    Transform player;
    private void Start()
    {
        levlg = GameObject.Find("LevelGenerator");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lg = levlg.GetComponent<LevelGenerator>();
        if (!rigidBod)
        {
            rigidBod = GetComponent<Rigidbody2D>();
        }

        currentWaypointIndex = offset;
    }

    void Update()
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
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

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
