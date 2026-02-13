using UnityEngine;

public class WayPoints : MonoBehaviour
{


    public int offset = 0;
    public bool chase = false;
    public bool ischasing = false;
    public float speed = 3f;
    public float chaseRange = 5f;
    LevelGenerator lg;
    private int currentWaypointIndex = 0;
    GameObject levlg;
    
    public Transform player;
    private void Start()
    {
        levlg = GameObject.Find("LevelGenerator");
      //  gpy = GameObject.Find("Player");
        lg = levlg.GetComponent<LevelGenerator>();
        
        
        currentWaypointIndex = offset;
    }

    void Update()
    {
        
        
            moveToWaypoint();
        
        
        if (chase ==  true) // chasing script is broken
        {
            float distance = Vector3.Distance(transform.position, player.position);
            Debug.Log(distance);

            if (distance < chaseRange)
            {
                ischasing = true;
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }else if (ischasing == true)
            {
                
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
                    ischasing = false;
                }
            }

        }
    }
    void moveToWaypoint()
    {
        if (ischasing == false)
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
        if (ischasing == true)
        { 
            
        }
    }
}
