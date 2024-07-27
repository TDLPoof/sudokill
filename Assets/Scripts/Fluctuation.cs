using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluctuation : MonoBehaviour
{
    Vector3 originalPos;
    public Vector3 maxShake;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shakeVector = Vector3.zero;
        shakeVector.x = originalPos.x + Random.Range(-maxShake.x, maxShake.x);
        shakeVector.y = originalPos.y + Random.Range(-maxShake.y, maxShake.y);
        shakeVector.z = originalPos.z + Random.Range(-maxShake.z, maxShake.z);
        transform.localPosition = shakeVector;
    }
}
