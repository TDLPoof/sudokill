using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAI : MonoBehaviour
{
    Transform player;
    Rigidbody rb;
    public float speed = 3, strength = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = player.position - transform.position;
        toTarget *= speed / Utils.Length(toTarget);
        print(toTarget);
        rb.velocity = Vector3.Lerp(rb.velocity, toTarget, strength * Time.deltaTime);
    }
}
