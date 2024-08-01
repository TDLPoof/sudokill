using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public float maxHealth = 1;
    public int hitGibs = 1;
    public float waveValue = 1, firstAvailableWave = 1;
    public float friendlyFireMult = 1;
    public string stunState = "";
    public bool stunnable, regainHealth;
    public ParticleSystem blood;

    public float killplaneDepth = -30;

    public PlayerHealth player;

    public GameObject giblet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform. position.y < killplaneDepth) health = 0;
        if (health <= 0)
        {
            for (int i = 0; i < 6 * (int)maxHealth; i++)
            {
                SpawnGib();
            }
            blood.transform.SetParent(null);
            blood.Play();
            Destroy(gameObject);
        }
        if (health > maxHealth) health = maxHealth;
    }

    public void SpawnGib()
    {
        var gib = Instantiate(giblet, transform.position + 0.5f * Random.insideUnitSphere, Random.rotation);
        Rigidbody rb = gib.GetComponent<Rigidbody>();
        rb.AddExplosionForce(9, transform.position, 6, 3, ForceMode.Impulse);
        gib.GetComponent<RemoveOnTime>().time += (2 * Random.value) - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hurtbox>() != null && other.GetComponent<Hurtbox>().friendlyFire)
        {
            if (stunState != null && stunnable)
            {
                //GetComponent<Animator>().Play(stunState);
                GetComponent<MeleeAttacker>().attackTimer = GetComponent<MeleeAttacker>().attackCooldown * -0.5f;
            }
            health -= other.GetComponent<Hurtbox>().damage * friendlyFireMult;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                other.GetComponent<Hurtbox>().killsManager.kills++;
                other.GetComponent<Hurtbox>().killsManager.killsThisWave++;
            }
        }
    }
}
