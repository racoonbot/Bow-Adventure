using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public GameObject BulletPrefab;
   public Trajectory trajectory;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Rigidbody rb = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(trajectory.direction * -trajectory.force, ForceMode.Impulse);
            
        }
    }
}
