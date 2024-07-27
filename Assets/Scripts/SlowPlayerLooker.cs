using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerLooker : MonoBehaviour
{
    Transform target;
    public float lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}
