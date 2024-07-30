using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public float shakeIntensity = 2f;
    public Transform shootPoint;
    public float penCharge = 0;
    public float maxPenCharge = 10;

    float shake = 0;
    Animator anim;
    public GameObject inkbomb;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shoot(float randomness = 0.5f)
    {
        var ink = Instantiate(inkbomb, shootPoint.position, Quaternion.identity);
        ink.GetComponent<Rigidbody>().AddForce((transform.up + (randomness * Random.insideUnitSphere)) * penCharge, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.Play("ChargePen");
            shake = Mathf.Lerp(shake, shakeIntensity, Time.deltaTime);
            Vector3 offsetVector = Random.insideUnitSphere * shake;
            transform.position = new Vector3(0.75f, -0.8f, 0.46f) + offsetVector;
            penCharge += Time.deltaTime * 5;
            if (penCharge > maxPenCharge) penCharge = maxPenCharge;
        }
        else
        {
            shake = 0;
            if (Input.GetMouseButtonUp(0))
            {
                anim.Play("ShootPen");
                for (int i = 0; i < 8; i++) Shoot();
                Shoot(0);
            }
            GetComponent<CapsuleCollider>().enabled = false;
            penCharge = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hitbox>() != null && other.GetComponent<Hitbox>().enabled)
        {
            other.GetComponentInParent<Enemy>().health -= 1;
        }
    }
}
