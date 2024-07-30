using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillsManager : MonoBehaviour
{
    public TextMeshProUGUI killField;
    public int kills;
    public int killsThisWave;
    public WaveController waveController;

    // Update is called once per frame
    void Update()
    {
        int progress = (int)(100f * killsThisWave / waveController.enemiesInThisWave);
        killField.text = "KILLS: " + kills + "\nWAVE: " + waveController.wave + "\nPROGRESS: " + progress + "%";

    }
}
