using Prime31;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovent : MonoBehaviour
{
    public float speed = 8f;
    public int value = 10;
    public float groundDamping = 20f;
    

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
            health.Instans.TakeDamage(1);
        }
        Debug.Log("trigger");
        if (col.CompareTag("dot"))
        {

            Destroy(col.gameObject);
            scoremanager.Instance.AddPoints(value);
            // Debug.Log(scoremanager.Instance.score);


        }
        if (col.CompareTag("PowerOrb"))
        {

            Destroy(col.gameObject);
            // - 1 PowerOrb Amount
            StartCoroutine(PowerOrbs());



        }
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");

        
        float y = Input.GetAxisRaw("Vertical");
        var smoothedMovementFactor = groundDamping; // how fast do we change direction?

        velocity.x = Mathf.Lerp(velocity.x, x * speed, Time.deltaTime * smoothedMovementFactor);


        velocity.y = Mathf.Lerp(velocity.y, y * speed, Time.deltaTime * smoothedMovementFactor);



        Controller2D.move(velocity * Time.deltaTime);
        velocity = Controller2D.velocity;
    }
   
    
    IEnumerator PowerOrbs()
    {
        // set Enemy Vonerabilety to True
        yield return new WaitForSeconds(5);
        // set Enemy Vonerabilety to fales
    }
}
