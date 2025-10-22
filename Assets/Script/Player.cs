using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public GameObject BulletPrefab;
   public Trajectory trajectory;
   public bool isBuffed = false;

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isBuffed)
        {
            Rigidbody rb = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.gameObject.layer = LayerMask.NameToLayer("BulletPlayer");
            rb.AddForce(trajectory.direction * -trajectory.force, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonUp(0) && isBuffed)
        {
            Rigidbody rb1 = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb1.gameObject.layer = LayerMask.NameToLayer("BulletPlayer");
            rb1.AddForce(trajectory.direction * -trajectory.force, ForceMode.Impulse); 
            
            Rigidbody rb2 = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb2.gameObject.layer = LayerMask.NameToLayer("BulletPlayer");
            rb2.AddForce((trajectory.direction + trajectory.transform.up.normalized * -0.15f) * -trajectory.force, ForceMode.Impulse); 
            
            Rigidbody rb3 = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb3.gameObject.layer = LayerMask.NameToLayer("BulletPlayer");
            rb3.AddForce((trajectory.direction + trajectory.transform.up.normalized * 0.15f) * -trajectory.force, ForceMode.Impulse);
        }
    }
}
