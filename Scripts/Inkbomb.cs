using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inkbomb : MonoBehaviour
{
    Rigidbody rb;
    bool hitGround = false;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<SphereCollider>().enabled = false;
        transform.SetParent(other.transform);
        GetComponent<Rigidbody>().isKinematic = true;

        if (other.GetComponentInParent<Rigidbody>() != null)
        {
            other.GetComponentInParent<Rigidbody>().AddForce(rb.velocity, ForceMode.Impulse);
        }
        if (other.GetComponentInParent<Enemy>() != null)
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            enemy.health -= 0.1f;
            enemy.SpawnGib();
        }
        if (other.GetComponentInParent<GroundedPathFinder>() != null)
        {
            GroundedPathFinder gpf = other.GetComponentInParent<GroundedPathFinder>();
            gpf.speed /= 2;
        }
        if (other.GetComponentInParent<FlyingPathFinder>() != null)
        {
            other.GetComponentInParent<Rigidbody>().useGravity = true;
            FlyingPathFinder fpf = other.GetComponentInParent<FlyingPathFinder>();
            fpf.timer = fpf.timeWithGravity / 2;
            fpf.speed /= 2;
        }
        if (other.GetComponentInParent<StrafingAI>() != null)
        {
            other.GetComponentInParent<StrafingAI>().enabled = false;
        }
        if (other.GetComponentInParent<DodgeAI>() != null)
        {
            other.GetComponentInParent<DodgeAI>().enabled = true;
        }
    }

    private void OnDestroy()
    {
        if (transform.parent != null)
        {
            Transform attached = transform.parent;
            if (attached.GetComponentInParent<GroundedPathFinder>() != null)
            {
                GroundedPathFinder gpf = attached.GetComponentInParent<GroundedPathFinder>();
                gpf.speed *= 2;
            }
            if (attached.GetComponentInParent<FlyingPathFinder>() != null)
            {
                FlyingPathFinder fpf = attached.GetComponentInParent<FlyingPathFinder>();
                fpf.speed *= 2;
            }
            if (attached.GetComponentInParent<StrafingAI>() != null)
            {
                attached.GetComponentInParent<StrafingAI>().enabled = true;
            }
            if (attached.GetComponentInParent<DodgeAI>() != null)
            {
                attached.GetComponentInParent<DodgeAI>().enabled = true;
            }
        }
    }
}
