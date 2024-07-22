using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public float maxHealth = 1;

    public PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            player.health += maxHealth;
            Destroy(gameObject);
        }
        if (health > maxHealth) health = maxHealth;
    }
}
