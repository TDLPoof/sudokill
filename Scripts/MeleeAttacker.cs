using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    public float initiateAttackDistance = 1, attackCooldown = 1;
    public bool stopMovingForAttack = true;
    public AnimationClip[] attacks;
    public Transform player;

    [HideInInspector]
    public float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > attackCooldown && Utils.DistanceSQ(transform.position, player.position) < initiateAttackDistance * initiateAttackDistance)
        {
            GetComponent<Animator>().Play(attacks[Random.Range(0, attacks.Length)].name);
            attackTimer = 0;
            if (stopMovingForAttack)
            {
                GetComponent<Rigidbody>().velocity = Vector2.zero;
            }
        }
        attackTimer += Time.deltaTime;
    }
}
