using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public bool m_musicEnable = true;
    public bool m_fxEnable = true;

    [Range(0.1f, 1f)]
    public float m_musicVolume = 1f;

    [Range(0.1f, 1f)]
    public float m_fxVolume = 1f;

    public AudioClip m_cleareRowSound;
    public AudioClip m_dropBrickSound;
    public AudioClip m_moveBrickSound;
    public AudioClip m_gameOverSound;
    public AudioClip m_errorSound;
    //public AudioClip m_backgroundMusic;

    public AudioSource m_musicSource;

    public AudioClip[] m_audioClips;
    //AudioClip m_randomAudioClip;

    public AudioClip[] m_vocalClips;

    public AudioClip m_vocalGameOver;

    public AudioClip m_vocalLevelUp;

    public ToogleIcon m_toogleIconMusic;
    public ToogleIcon m_toogleIconFX;
    

    public AudioClip GetOneRandomAudioClip(AudioClip[] clips)
    {
        AudioClip audio = clips[Random.Range(0, clips.Length)];
        return audio;
    }

    public void PlayBackgorundMusic(AudioClip musicClip)
    {
        if(!m_musicEnable || !musicClip || !m_musicSource)
        {
            return;
        }

        m_musicSource.Stop();
        m_musicSource.clip = musicClip;
        m_musicSource.volume = m_musicVolume;
        m_musicSource.loop = true;
        m_musicSource.Play();
    }

    void UpdateMusic()
    {
        if(m_musicSource.isPlaying != m_musicEnable)
        {
            if (m_musicEnable)
            {
                PlayBackgorundMusic(GetOneRandomAudioClip(m_audioClips));
            }
            else
            {
                m_musicSource.Stop();
            }
        }
    }

    public void ToggleMusic()
    {
        m_musicEnable = !m_musicEnable;
        UpdateMusic();

        if (m_toogleIconMusic)
        {
            m_toogleIconMusic.ToogleOnIcon(m_musicEnable);
        }
        else
        {
            Debug.Log("WARNING ToogleIcon music not found!");
        }
    }

    public void ToggleFx()
    {
        m_fxEnable = !m_fxEnable;
        if (m_toogleIconFX)
        {
            m_toogleIconFX.ToogleOnIcon(m_fxEnable);
        }
        else
        {
            Debug.Log("WARNING ToogleIcon fx not found!");
        }
    }

    // Use this for initialization
    void Start () {
        //PlayBackgorundMusic(GetOneRandomAudioClip(m_audioClips));
    }
	
	// Update is called once per frame
	void Update () {
    }
}
