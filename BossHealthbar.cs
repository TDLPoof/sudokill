using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().maxValue = GetComponentInParent<Enemy>().maxHealth;
    }

    void Update()
    {
        GetComponent<Slider>().value = Mathf.Lerp(GetComponent<Slider>().value, GetComponentInParent<Enemy>().health, Time.deltaTime);
    }
}
