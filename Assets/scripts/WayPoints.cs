using UnityEngine;

public class WayPoints : MonoBehaviour
{
    
    
    
    public float speed = 3f;
    LevelGenerator lg;
    private int currentWaypointIndex = 0;
    GameObject levlg;
    private void Start()
    {
        levlg = GameObject.Find("LevelGenerator");
        lg = levlg.GetComponent<LevelGenerator>();
    }

    void Update()
    {
        if (lg.waypoints.Length == 0) return;

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
            if (currentWaypointIndex >= lg.waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
    
}
