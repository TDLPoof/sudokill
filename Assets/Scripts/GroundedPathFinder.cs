using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPathFinder : MonoBehaviour
{
    public float angleRays = 12;
    public float pathingDistance = 30;
    public float speed = 2;
    public float targetDistance = 1;
    public bool fallBackToTarget;

    public Transform pathCheck, groundCheck;
    public float groundCheckDistance;
    public LayerMask validGround;
    [HideInInspector]
    public bool grounded;

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    Vector3 GetEndpoint(Vector3 start, float distance, float angle)
    {
        Vector3 straightDistance = new Vector3(distance, 0, 0);
        straightDistance.x = distance * Mathf.Cos(angle);
        straightDistance.y = distance * Mathf.Sin(angle);
        return start + straightDistance;
    }

    public static float DistanceSQ(Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.y - p2.y, 2);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, validGround.value);
        if (grounded)
        {
            // run a line to the player and check all nearby objects to make sure they do not intersect
            /*
            float cullThreshold = 10f;
            Vector3 toPlayer = transform.position - player.position;
            List<Transform> hit = new List<Transform>();
            foreach (Collider coll in FindObjectsOfType<Collider>())
            {
                Transform body = coll.transform;
                if (DistanceSQ(body.position, transform.position) < cullThreshold * cullThreshold
                 && body.GetComponent<PathfindingOcclusion>().enabled)
                {
                    // box to line collision
                    if (body.GetComponent<BoxCollider>() != null)
                    {

                    }
                    if (body.GetComponent<SphereCollider>() != null)
                    {
                        
                    }
                }
            }*/
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            if (DistanceSQ(transform.position, player.position) > targetDistance * targetDistance)
                rb.AddForce(transform.forward * speed/* * Time.deltaTime*/);
        }
    }
}
