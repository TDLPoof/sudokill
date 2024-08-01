using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;
    public float killplaneDepth = -30;

    [HideInInspector]
    public bool dead;
    public Slider bar;
    public TextMeshProUGUI text;

    int slowValue = 100;
    float invincibleTime = 0.15f;
    float invTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        bar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < killplaneDepth && !dead)
        {
            health = 0;
            KillDaPlayer();
        }
        // make sure health never exceeds maximum
        if (health > maxHealth) health = maxHealth;

        bar.value = Mathf.Lerp(bar.value, health, 0.33f);
        text.text = "" + Mathf.RoundToInt(health * 10);
        invTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hurtbox>() != null && invTimer > invincibleTime)
        {
            invTimer = 0;
            health -= other.GetComponent<Hurtbox>().damage;
            Vector3 toEnemy = other.transform.position - transform.position;
            toEnemy /= Utils.Length(toEnemy);
            GetComponent<PlayerMovement>().velocity /= 4;
            GetComponent<PlayerMovement>().velocity += toEnemy * -other.GetComponent<Hurtbox>().kbForce;
            GetComponentInChildren<CameraVisuals>().DamageFlash();
            if (health <= 0 && !dead)
            {
                KillDaPlayer();
            }
        }
    }

    void KillDaPlayer()
    {
        dead = true;
        GetComponentInChildren<DeathCamera>().Activate();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
