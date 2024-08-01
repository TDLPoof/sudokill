using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giblet : MonoBehaviour
{
    public float healthRestore = 0.5f, distance = 2f;
    PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.DistanceSQ(transform.position, player.transform.position) < distance * distance)
        {
            player.health += healthRestore;
            Destroy(gameObject);
        }
    }
}
