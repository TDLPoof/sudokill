using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public GameObject projectile;
    public float force, upwardForce = 5;
    public Transform shootPoint;
    public void MortarShoot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = 2 * i * Mathf.PI / count;
            Vector3 direction = new Vector3(Mathf.Cos(angle), upwardForce, Mathf.Sin(angle));
            var mortar = Instantiate(projectile, shootPoint.position, Quaternion.identity);
            mortar.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
