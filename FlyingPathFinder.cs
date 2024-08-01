using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPathFinder : MonoBehaviour
{
    public float timeWithGravity = 4;
    public float speed = 2, ascendSpeed = 1.5f;
    public float targetDistance = 1, targetHeight = 3;
    float saveTargetHeight;
    public bool fallBackToTarget;
    public float lookSpeed = -1;

    [HideInInspector]
    public float timer = 0;

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        saveTargetHeight = targetHeight;
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

    void EasingLookAt(Transform target, float easing)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        print(targetRotation.eulerAngles);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, easing);
    }

// Update is called once per frame
void Update()
    {
        if (timer > timeWithGravity && !GetComponent<Rigidbody>().useGravity) GetComponent<Rigidbody>().useGravity = true;
        timer += Time.deltaTime;
        targetHeight = Mathf.Lerp(targetHeight, saveTargetHeight, Time.deltaTime);
        if (lookSpeed == -1) transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        else
        {
            EasingLookAt(player, lookSpeed * Time.deltaTime);
        }
        Vector3 forward = transform.forward;
        transform.LookAt(player.position);
        if (DistanceSQ(transform.position, player.position) > targetDistance * targetDistance)
            rb.AddForce(forward * speed * Time.deltaTime);
        if (fallBackToTarget && DistanceSQ(transform.position, player.position) < (targetDistance - 1f) * (targetDistance - 1f))
            rb.AddForce(forward * speed * -0.75f * Time.deltaTime);

        if (GetComponentInChildren<Inkbomb>() == null)
        {
            if (transform.position.y - player.position.y < targetHeight)
            {
                rb.AddForce(Vector3.up * ascendSpeed * Time.deltaTime);
            }
            else
            {
                float gravity = ascendSpeed / 4f;
                rb.AddForce(Vector3.up * gravity * Time.deltaTime);
            }
        }
    }
}
