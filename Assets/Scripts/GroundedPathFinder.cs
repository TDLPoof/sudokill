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

    public float lookSpeed = -1;

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

            if (lookSpeed == -1) transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            else
            {
                EasingLookAt(player, lookSpeed * Time.deltaTime);
            }
            if (Utils.DistanceSQ(transform.position, player.position) > targetDistance * targetDistance)
                rb.AddForce(transform.forward * speed/* * Time.deltaTime*/);
            if (fallBackToTarget && Utils.DistanceSQ(transform.position, player.position) < (targetDistance - 1f) * (targetDistance - 1f))
                rb.AddForce(transform.forward * speed * -0.75f);
        }

        void EasingLookAt(Transform target, float easing)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            print(targetRotation.eulerAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, easing);
        }
    }
}
