using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TipGenerator : MonoBehaviour
{
    public string[] tips;
    public TextMeshProUGUI tipField;

    public void NewTip()
    {
        tipField.text = tips[Random.Range(0, tips.Length)];
    }
}
