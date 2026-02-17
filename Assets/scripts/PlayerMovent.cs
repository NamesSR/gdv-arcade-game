using Prime31;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovent : MonoBehaviour
{
    public float speed = 8f;
    public int value = 10;
    public float groundDamping = 20f;
    public float inAirDamping = 5f;

    //private Rigidbody2D rb;
    private Vector3 velocity;
    private RaycastHit2D _lastControllerColliderHit;
   private CharacterController2D Controller2D;

    void Start()
    {
        
      //  rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        Controller2D = GetComponent<CharacterController2D>();
        Controller2D.onControllerCollidedEvent += onControllerCollider;
    }
    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f || hit.normal.x == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
       // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }
    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    void Update()
    {
       
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        var smoothedMovementFactor = Controller2D.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        
            velocity.x = Mathf.Lerp(velocity.x, x * speed, Time.deltaTime * smoothedMovementFactor);
        
        
            velocity.y = Mathf.Lerp(velocity.y, y * speed, Time.deltaTime * smoothedMovementFactor);
        
        // Vector3 dir = new Vector3(x, y, 0);
        // if (dir.magnitude >= 0.1f)
        // {
        //     Controller2D.move(dir * speed * Time.deltaTime);
        //}
        Controller2D.move(velocity * Time.deltaTime);
        velocity = Controller2D.velocity;
    }

    private void FixedUpdate()
    {
      //  rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("dot"))
        {
            
            Destroy(other.gameObject);
            scoremanager.Instance.AddPoints(value);
           // Debug.Log(scoremanager.Instance.score);


        }
        

    }
    
}
