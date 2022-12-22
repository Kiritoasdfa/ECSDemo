using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SkillWindow : EditorWindow
{
    Player player;
    List<SkillBase> skills;
    float currSpeed = 1;
    float temptime = 0;
    internal void SetInitSkill(List<SkillBase> _skills, Player _player)
    {
        player = _player;
        // player.AnimSpeed = 1;
        currSpeed = 1;
        skills = _skills;
    }
    string[] skillComponent = new string[] { "null", "∂Øª≠", "…˘“Ù", "Ãÿ–ß","∑∂ŒßºÏ≤‚"};
    int skillComponentIndex = 0;

    Vector2 ScrollViewPos = new Vector2(0, 0);
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("≤•∑≈"))
        {
            foreach (var item in skills)
            {
                item.Play();
            }
        }
        if (GUILayout.Button("Õ£÷π"))
        {
            foreach (var item in skills)
            {
                item.Stop();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("ÀŸ∂»");
        float speed = EditorGUILayout.Slider(currSpeed, 0, 5);
        if (speed != currSpeed)
        {
            currSpeed = speed;
            Time.timeScale = currSpeed;
        }
        GUILayout.BeginHorizontal();
        skillComponentIndex = EditorGUILayout.Popup(skillComponentIndex, skillComponent);
        if (GUILayout.Button("ÃÌº”"))
        {
            switch (skillComponentIndex)
            {
                case 1:
                    skills.Add(new Skill_Anim(player));
                    break;
                case 2:
                    skills.Add(new Skill_Audio(player));
                    break;
                case 3:
                    skills.Add(new Skill_Effects(player));
                    break;
                case 4:
                    skills.Add(new Skill_Range(player));
                    break;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(25);

        ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
        foreach (var item in skills)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            if (GUILayout.Button("…æ≥˝"))
            {
                skills.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            if (item is Skill_Anim)
            {
                ShowSkill_Anim(item as Skill_Anim);
            }
            else if (item is Skill_Audio)
            {
                Skill_Audio(item as Skill_Audio);
            }
            else if (item is Skill_Effects)
            {
                Skill_Effects(item as Skill_Effects);
            }
            else if(item is Skill_Range)
            {
                Skill_Ranges(item as Skill_Range);
            }
            GUILayout.Space(25);
        }
        GUILayout.EndScrollView();
    }
    float ang = 0;
    float dis = 0;
    float atk = 0;
    private void Skill_Ranges(Skill_Range _Range)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("æ‡¿Î£∫");
        dis=EditorGUILayout.FloatField(_Range.dis);
        GUILayout.Label("Ω«∂»£∫");
        ang = EditorGUILayout.FloatField(_Range.ang);
        GUILayout.Label("…À∫¶£∫");
        atk = EditorGUILayout.FloatField(_Range.atk);
        GUILayout.EndHorizontal();
        _Range.SetTime(Effectstimer);
        if (_Range.ang!=ang||_Range.dis!=dis|| _Range.atk != atk)
        {
            _Range.SetRangeData(ang, dis,atk);
        }
    }

    float Effectstimer=0;
    private void Skill_Effects(Skill_Effects _Effects)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("—” ±");
        Effectstimer = EditorGUILayout.Slider(_Effects.Timer, 0, 3f);
        _Effects.SetTime(Effectstimer);
        GUILayout.EndHorizontal();

        GameObject clip= EditorGUILayout.ObjectField(_Effects.gameClip, typeof(GameObject), false) as GameObject;
        if (_Effects.gameClip != clip)
        {
            _Effects.SetGameClip(clip);
        }
    }
    float Audiotimer = 0;
    private void Skill_Audio(Skill_Audio _Audio)
    {
        AudioClip animClip = EditorGUILayout.ObjectField(_Audio.audioClip, typeof(AudioClip), false) as AudioClip;
        if (_Audio.audioClip != animClip)
        {
            _Audio.audioClip = animClip;
            _Audio.SetAnimClip(animClip);
        }
    }
    float animationtimer = 0;
    private void ShowSkill_Anim(Skill_Anim _Anim)
    {
        
        AnimationClip animClip = EditorGUILayout.ObjectField(_Anim.animClip, typeof(AnimationClip), false) as AnimationClip;
        if (_Anim.animClip != animClip)
        {
            _Anim.animClip = animClip;
            _Anim.SetAnimClip(animClip);
        }
    }
}
