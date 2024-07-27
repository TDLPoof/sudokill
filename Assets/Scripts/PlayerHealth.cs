using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;
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
            GetComponentInChildren<CameraVisuals>().DamageFlash();
            if (health <= 0 && !GetComponentInChildren<DeathCamera>().activated)
            {
                GetComponentInChildren<DeathCamera>().Activate();
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}
