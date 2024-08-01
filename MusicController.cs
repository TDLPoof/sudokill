using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public float volume;
    public AudioClip[] music;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            AudioClip newMusic = source.clip;
            while (newMusic == source.clip)
            {
                newMusic = music[Random.Range(0, music.Length)];
            }
            source.clip = newMusic;
            source.Play();
            source.volume = Mathf.Lerp(source.volume, volume, Time.deltaTime);
        }
    }
}
