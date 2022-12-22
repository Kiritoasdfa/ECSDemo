using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Skill_Audio : SkillBase
{
    public AudioClip audioClip;
    Player player;
    AudioSource audioSource;
    
    public Skill_Audio(Player _player)
    {
        player = _player;
        audioSource = player.GetComponent<AudioSource>();
    }
    public void SetTime(float audiotimer)
    {
        Timer = audiotimer;
    }

    public void SetAnimClip(AudioClip _audioClip)
    {
        audioClip = _audioClip;
        name = audioClip.name;
        audioSource.clip = audioClip;
    }
    public override void Init()
    {
        base.Init();
        audioSource.clip = audioClip;
    }
    public override void Play()
    {
        base.Play();
        
        PlayAnim();
    }

    async void PlayAnim()
    {
        await Task.Delay((int)(Timer * 1000));
        audioSource.Play();
    }

    public override void Stop()
    {
        base.Stop();
        audioSource.Stop();
    }

   
}
