using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3 direction;
    public float force;
    public float verticalDirectionPoint;
    public float startX;
    public float startY;
    public Transform bow;
    void Start()
    {
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        
    }


    void Update()
    {
        Plane plane = new Plane(-Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (plane.Raycast(ray, out float distance))
            {
                startX = ray.GetPoint(distance).x;
                startY = ray.GetPoint(distance).y;
            }
        }

        if (Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true;
            direction = transform.right;
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                force = point.x - startX;
                verticalDirectionPoint =  point.y - startY;
                transform.rotation = Quaternion.Euler(0f, 0f, -verticalDirectionPoint * 10f ); 
            }

            bow.rotation = Quaternion.LookRotation(lineRenderer.GetPosition(2) -  bow.position);
            
            ShowTrajectory(transform.position, direction * -force);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100]; //!
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2;
        }

        lineRenderer.SetPositions(points);
    }
}