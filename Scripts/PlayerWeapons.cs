using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] icons;
    public float solverKills, maxSolverKills;
    public Slider solverBar;
    public GameObject solver;
    public ParticleSystem solverParticle;
    public Color emptyColor, fullColor;
    public TextMeshProUGUI solverText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // sucks to hardcode but it's the best solution for small scales
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            icons[0].SetActive(true);
            icons[1].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            icons[0].SetActive(false);
            icons[1].SetActive(true);
        }

        float solverScore = solverKills / maxSolverKills;
        if (solverScore >= 1f)
        {
            solverText.color = fullColor;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                solverParticle.Play();
                solverKills = 0;
                Instantiate(solver).SetActive(true);
            }
        }
        else solverText.color = emptyColor;
        solverBar.value = Mathf.Lerp(solverBar.value, solverScore, 0.2f);
    }
}
