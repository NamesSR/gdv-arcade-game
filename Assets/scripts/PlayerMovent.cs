using Prime31;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovent : MonoBehaviour
{
    public float speed = 8f;
    public int value = 10;
    public float groundDamping = 20f;
    public bool CanShoot = true;
    public Vector3 dir;
    public float angle;
    public GameObject FireBallPrefab;
    private Vector3 velocity;
    private RaycastHit2D _lastControllerColliderHit;
    private CharacterController2D Controller2D;


    private void Awake()
    {
        Controller2D = GetComponent<CharacterController2D>();
        Controller2D.onControllerCollidedEvent += onControllerCollider;
        Controller2D.onTriggerEnterEvent += onTriggerEnterEvent;
    }
        
        
    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
       // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }

    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if(col.gameObject.tag == "Enemy")
        {
            if(GameManager.Instance.vulnerable == false)
            {
            GameManager.Instance.TakeDamage(1);
            
            }
                
        }
        Debug.Log("trigger");
        if (col.CompareTag("dot"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.AddPoints(value);
        }

        if (col.CompareTag("PowerOrb"))
        {

            Destroy(col.gameObject);
            GameManager.Instance.powerOrbCountAdd(-1);
            StartCoroutine(PowerOrbs());




        }
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    void Update()
    {
        ShootFireBall();
    }

    private void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");

        
        float y = Input.GetAxisRaw("Vertical");
        var smoothedMovementFactor = groundDamping; // how fast do we change direction?
         

        velocity.x = Mathf.Lerp(velocity.x, x * speed, Time.deltaTime * smoothedMovementFactor);


        velocity.y = Mathf.Lerp(velocity.y, y * speed, Time.deltaTime * smoothedMovementFactor);
        if (y != 0 || x != 0)
        {
           dir = new Vector3(x, y, 0);
           angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
           float currentZ = (transform.eulerAngles = new Vector3(0,0,angle)).z;
           float snappedZ = Mathf.Round(currentZ / 90.0f) * 90.0f;
           transform.rotation = Quaternion.Euler(0, 0, snappedZ);
           Debug.Log($"{currentZ}:{snappedZ}:{angle}");
           
        }
        
        
        Controller2D.move(velocity * Time.deltaTime);
        velocity = Controller2D.velocity;//.normalized;
    }
   
    
    IEnumerator PowerOrbs()
    {

        GameManager.Instance.vulnerable = true;
        yield return new WaitForSeconds(3);
        GameManager.Instance.vulnerable = false;

    }
    void ShootFireBall()
    {
        if (CanShoot == true)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject projectile = Instantiate(FireBallPrefab);
                CanShoot = false;
                StartCoroutine(AttackCoolDown());
            }
        }
    }
    IEnumerator AttackCoolDown()
    {

        
        yield return new WaitForSeconds(1f);
        CanShoot = true;

    }
}
