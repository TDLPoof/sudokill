using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigunner : MonoBehaviour
{
    public Transform[] shootPoints;
    int shootPointIndex;

    public float shootCooldown;
    float shootTimer;
    public float force;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0;
            Shoot();
            shootPointIndex++;
            shootPointIndex %= shootPoints.Length;
        }
    }

    public void Shoot()
    {
        var proj = Instantiate(projectile, shootPoints[shootPointIndex].position, Quaternion.identity);
        proj.GetComponent<Rigidbody>().AddForce(shootPoints[shootPointIndex].forward * force, ForceMode.Impulse);
    }
}
