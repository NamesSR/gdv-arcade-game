using System.Collections;
using UnityEngine;

public class test : MonoBehaviour
{
    public LevelGenerator l;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
           // GameManager.Instance.AddPoints(10);
           // Debug.Log("a");
            //GameManager.Instance.TakeDamage(1);
           l.destroyLevel();


        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {


            StartCoroutine(WaitV());

        }
       
    }
    IEnumerator WaitV()
    {
        l.destroyLevel();
        yield return new WaitForSeconds(0.001f);
        l.GenerateLevel(1);
    }

}
