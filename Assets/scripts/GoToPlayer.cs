using Prime31;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class GoToPlayer : MonoBehaviour
{
    Transform player;
    private Transform tf;
    public GameObject FireBallPrefab;
    public bool CanShoot = true;
    public Vector3 dir = new Vector3(1,0,0);
    public float angle;
    
    void Start()
    {
      
        tf = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        float x = Input.GetAxisRaw("Horizontal");

        tf.position = player.position;
        float y = Input.GetAxisRaw("Vertical");
        if (y != 0 || x != 0)
        {
            dir = new Vector3(x, y, 0);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        ShootFireBall();
    }

    void ShootFireBall()
    {
        if(CanShoot == true)
        {
            if (Input.GetMouseButton(0))
            { 
                GameObject projectile = Instantiate(FireBallPrefab);
                CanShoot = false;
            }
        }
    }
}
