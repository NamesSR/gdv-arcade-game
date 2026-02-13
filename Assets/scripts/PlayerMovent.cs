using UnityEngine;

public class PlayerMovent : MonoBehaviour
{
    public float speed = 5f;
    public int value = 10;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("dot"))
        {
            
            Destroy(other.gameObject);
            scoremanager.Instance.AddPoints(value);
            Debug.Log(scoremanager.Instance.score);

        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health.Instans.TakeDamage(1);
        }
    }
}
