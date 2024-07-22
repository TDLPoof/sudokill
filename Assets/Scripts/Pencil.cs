using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    // Start is called before the first frame update

    public float pencilboostForce;
    public Transform raycastPoint;
    public LayerMask pencilboostLayers;
    public int slashState = 1;

    bool boostCheck;
    bool damaging = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        if (Input.GetMouseButton(0))
        {
            if (slashState == 1) anim.Play("PencilSlash_1"); damaging = true;
            if (slashState == 2) anim.Play("PencilSlash_2"); damaging = true;
        }
    }

    public void Pencilboost(float distanceThreshold)
    {
        if (Physics.Raycast(raycastPoint.position, raycastPoint.up, out RaycastHit hitInfo, distanceThreshold, pencilboostLayers.value))
        {
            // after testing for a raycast (the easy part), we calculate a normal from the pencil to the surface
            GetComponentInParent<PlayerMovement>().velocity += Vector3.up * pencilboostForce * Time.deltaTime;
            boostCheck = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hitbox>() != null && other.GetComponent<Hitbox>().enabled && damaging)
        {
            other.GetComponentInParent<Enemy>().health -= 1;
            damaging = false;
        }
    }
}
