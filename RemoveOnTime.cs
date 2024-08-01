using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnTime : MonoBehaviour
{
    public float time = 5;
    float timer = 0;
    public bool scaleDown = false;
    Vector3 ogScale;

    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > time) Destroy(gameObject);
        timer += Time.deltaTime;
        if (scaleDown)
        {
            transform.localScale = Vector3.Lerp(ogScale, Vector3.zero, timer / time);
        }
    }
}
