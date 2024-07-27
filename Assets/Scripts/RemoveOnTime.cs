using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnTime : MonoBehaviour
{
    public float time = 5;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > time) Destroy(gameObject);
        timer += Time.deltaTime;
    }
}
