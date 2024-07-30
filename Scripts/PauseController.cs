using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseController : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseMenu;
    public TipGenerator pauseTip;
    public PlayerHealth player;
    public Volume CA_Volume;
    ChromaticAberration aberration;

    int pausedTicks = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        CA_Volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration ca);
        aberration = ca;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0)
        {
            paused = false;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                pauseMenu.SetActive(paused);
                if (paused) pauseTip.NewTip();
            }
            Cursor.visible = paused;
            GetComponent<CameraControl>().enabled = !paused;
            if (paused) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        if (paused)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.001f, 0.01f);
            if (pausedTicks++ > 900) pausedTicks = 900;
            aberration.intensity.value = (100 + pausedTicks) / 1000f;
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 0.01f);
            aberration.intensity.value = 0.1f;
            pausedTicks = 0;
        }
    }
}
