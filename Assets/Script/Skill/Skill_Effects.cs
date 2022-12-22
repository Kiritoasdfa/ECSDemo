using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Skill_Effects : SkillBase
{
    public GameObject gameClip;
    Player player;

    ParticleSystem particleSystem;

    GameObject obj;

    public Skill_Effects (Player play)
    {
        player = play;

    }
    public void SetGameClip(GameObject _audioClip)
    {
        gameClip = _audioClip;
        if(gameClip!=null)
        {
            if(gameClip.GetComponent<ParticleSystem>())
            {
            obj = GameObject.Instantiate(gameClip, player.effectsparent,false);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
            }
            name = _audioClip.name;
        }
        
    }
    public void SetTime(float time)
    {
        Timer = time;
    }
    public override void Play()
    {
        base.Play();
        
        if (particleSystem!=null)
        {
            PlayEffect();
            //gameClip.transform.parent = (GameObject.Find("Plane").transform);
        }

    }

    async void PlayEffect()
    {
        await Task.Delay((int)(Timer * 1000));
        particleSystem.Play();
    }

    public override void Init()
    {
        base.Init();
        if(gameClip.GetComponent<ParticleSystem>())
        {
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }
    public override void Stop()
    {
        base.Stop();
        if(particleSystem!=null)
        {
            particleSystem.Stop();
        }
    }
}
