using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource[] soundEffects; 
    public AudioSource bgm, levelEndMusic, levelSelectMusic, bossMusic, gameCompleted, titleScreen;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);

        soundEffects[soundToPlay].Play();
    }

    public void StopSFXLS()
    {
        levelSelectMusic.Play();
        
        levelSelectMusic.Stop();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
    }
    
    public void StopBossMusic()
    {
        bossMusic.Stop();
        bgm.Play(); 
    }
}
