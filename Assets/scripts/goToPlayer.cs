using UnityEngine;

public class goToPlayer : MonoBehaviour
{
    Transform player;
   private Transform tf;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        tf.position = player.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health.Instans.TakeDamage(1);
        }
    }
}
