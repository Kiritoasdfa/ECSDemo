using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Skill_Anim : SkillBase
{
    Player player;
    Animator anim;
    
    public AnimationClip animClip;
    AnimatorOverrideController controller;
     
    public Skill_Anim(Player _player)
    {
        player = _player;
        anim = player.gameObject.GetComponent<Animator>();
        controller = player.overrideController;
    }
    public override void Init()
    {
        controller["Start"] = animClip;
    }

    public void SetAnimClip(AnimationClip _animClip)
    {
        animClip = _animClip;
        if (animClip != null)
        {
            name = animClip.name;
        }
        controller["Start"] = animClip;
    }
    public void SetTime(float animationtimer)
    {
        Timer = animationtimer;
    }
    public override void Play()
    {
        base.Play();
        anim.StopPlayback();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle1"))
        {
            PlayAnim();
        }
        else if(stateInfo.IsName("Run3"))
        {
            PlayAnim();
        }
    }

    async void PlayAnim()
    {
        await Task.Delay((int)(Timer * 1000));
        anim.SetTrigger("Play");
    }

    public override void Stop()
    {
        base.Play();
        anim.StartPlayback();
    }

    
}
