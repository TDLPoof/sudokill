using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttacker : MonoBehaviour
{
    public float initiateAttackDistance = 1, attackCooldown = 1;
    public Transform shootPoint;
    public AnimationClip[] attacks;
    public GameObject[] projectiles;
    public float[] attackForces;
    public Transform player;

    float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    float DistanceSQ(Vector3 point1, Vector3 point2)
    {
        return Mathf.Pow((point1.x - point2.x), 2) + Mathf.Pow((point1.y - point2.y), 2) + Mathf.Pow((point1.z - point2.z), 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > attackCooldown && DistanceSQ(transform.position, player.position) < initiateAttackDistance * initiateAttackDistance)
        {
            GetComponent<Animator>().Play(attacks[Random.Range(0, attacks.Length)].name);
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        int i = Random.Range(0, projectiles.Length);
        var projectile = Instantiate(projectiles[i], shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * attackForces[i], ForceMode.Impulse);
    }
}
