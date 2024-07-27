using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAI : MonoBehaviour
{
    public float dodgeDistance, dodgeForce;
    public float dodgeCooldown = 0.5f;
    float dodgeTimer = 0;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dodgeTimer += Time.deltaTime;
        if (Utils.DistanceSQ(transform.position, player.position) < dodgeDistance * dodgeDistance
         && dodgeTimer > dodgeCooldown)
        {
            Dodge(player.position, dodgeForce);
            dodgeTimer = 0;
        }
    }

    public void Dodge(Vector3 target, float force)
    {
        Vector3 direction = (transform.position - target) / Utils.Length(transform.position - target);
        GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
