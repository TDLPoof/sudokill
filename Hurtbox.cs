using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public float damage = 1f;
    public bool projectile;
    public bool friendlyFire;
    public float kbForce = 0;
    public bool blocksAttacks = false;

    [HideInInspector]
    public KillsManager killsManager;

    private void OnTriggerEnter(Collider other)
    {
        if (blocksAttacks && other.GetComponent<Inkbomb>() != null)
        {
            other.GetComponent<SphereCollider>().enabled = false;
            other.transform.SetParent(transform);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
