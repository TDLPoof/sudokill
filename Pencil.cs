using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    // Start is called before the first frame update

    public float pencilboostForce;
    public Transform raycastPoint;
    public LayerMask pencilboostLayers;
    public int slashState = 1;

    public ParticleSystem deflectParticle;

    // keep track of old position to calculate velocity on non-rigidbody object
    Vector3 lastPosition;
    Vector3 frameVelocityNormalized;

    bool boostCheck;
    bool damaging = false;

    public KillsManager killsManager;

    void Start()
    {
        
    }

    float Length(Vector3 vector)
    {
        return Mathf.Sqrt(Utils.DistanceSQ(Vector3.zero, vector));
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        if (Input.GetMouseButton(0))
        {
            if (slashState == 1)
            {
                anim.Play("PencilSlash_1");
                damaging = true;
            }
            if (slashState == 2)
            {
                anim.Play("PencilSlash_2"); damaging = true;
            }
        }
    }

    private void LateUpdate()
    {
        lastPosition = transform.position;
    }

    public void Pencilboost(float distanceThreshold)
    {
        if (Physics.Raycast(raycastPoint.position, raycastPoint.up, out RaycastHit hitInfo, distanceThreshold, pencilboostLayers.value))
        {
            // after testing for a raycast (the easy part), we calculate a normal from the pencil to the surface
            GetComponentInParent<PlayerMovement>().velocity += transform.parent.parent.forward * pencilboostForce * Time.deltaTime;
            boostCheck = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hitbox>() != null && other.GetComponent<Hitbox>().enabled && damaging)
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy.GetComponent<GroundedPathFinder>() != null && enemy.stunState != "" && enemy.stunnable)
            {
                enemy.GetComponent<Animator>().Play(enemy.stunState);
                enemy.GetComponent<MeleeAttacker>().attackTimer = GetComponent<MeleeAttacker>().attackCooldown * -1f;
            }
            enemy.blood.Play();
            for (int i = 0; i < enemy.hitGibs; i++)
            {
                enemy.SpawnGib();
            }
            enemy.health -= 1;
            GetComponentInParent<PlayerWeapons>().solverKills += 0.25f;
            if (enemy.health <= 0)
            {
                GetComponentInParent<PlayerWeapons>().solverKills += 0.75f;
                killsManager.kills++;
                killsManager.killsThisWave++;
            }
            enemy.GetComponent<Rigidbody>().AddForce(transform.parent.forward * pencilboostForce, ForceMode.Impulse);
            if (other.GetComponent<FlyingPathFinder>() != null)
            {
                other.GetComponent<FlyingPathFinder>().targetHeight = 0;
            }
            damaging = false;
        }
        if (other.GetComponent<Hurtbox>() != null && other.GetComponent<Hurtbox>().projectile)
        {
            other.GetComponent<Rigidbody>().velocity = transform.parent.forward * pencilboostForce * 4;
            //other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Hurtbox>().friendlyFire = true;
            other.GetComponent<Hurtbox>().killsManager = killsManager;
            GetComponentInParent<PlayerHealth>().health += other.GetComponent<Hurtbox>().damage;
            deflectParticle.Play();
        }
    }
}
