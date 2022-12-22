using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System;

public class Player : MonoBehaviour
{
    public Dictionary<string, List<SkillBase>> skillsList = new Dictionary<string, List<SkillBase>>();
    public List<List<SkillBase>> skillPlay = new List<List<SkillBase>>();

    RuntimeAnimatorController controller;

    public AnimatorOverrideController overrideController;

    public Transform effectsparent;

    AudioSource audioSource;

    Animator anim;

    string m_strSKillId;

    private Skill_Anim _Anim;
    private Skill_Audio _Aduio;
    private Skill_Effects _Effect;
    public void InitData()
    {

        overrideController = new AnimatorOverrideController();
        controller = Resources.Load<RuntimeAnimatorController>("Player"); //动画控制器
        overrideController.runtimeAnimatorController = controller;
        anim.runtimeAnimatorController = overrideController;
        audioSource = gameObject.AddComponent<AudioSource>(); //音效是添加上的
        effectsparent = transform.Find("effectsparent");  // 特效的父节点
                                                          // gameObject.name = path;
                                                          //加载技能配置表
        LoadAllSkill("Teddy");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="WuPin")
        {
            Destroy(collision.gameObject);

        }
    }


    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(anim!=null)
        {
            if(JoyStickMgr.Instance.m_joystick.transform.Find("Thumb").localPosition!=Vector3.zero)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    internal void play()
    {
        Debug.Log("skillsList" + " " + skillsList.Count);
        if (m_strSKillId!=null&& skillsList.ContainsKey(m_strSKillId))
        {
            foreach(var item in skillsList[m_strSKillId])
            {
                item.Play();
            }
        }
    }

    internal void SetData(string skillName)
    {
        m_strSKillId = skillName;
        List<SkillXml> skillList = GameData.Instance.GetSkillsByRoleName("Teddy");
        foreach(var item in skillList)
        {
            if(item.name==skillName)
            {
                foreach(var ite in item.Dicskills)
                {
                    foreach(var it in ite.Value)
                    {
                        if(ite.Key.Equals("动画"))
                        {
                            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameDate/Anim/" + it.name + ".anim");
                            if (_Anim == null) _Anim = new Skill_Anim(this); //创建一个技能的类对象
                            _Anim.SetAnimClip(clip);
                        }
                        else if(ite.Key.Equals("音效"))
                        {
                            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameDate/Audio/" + it + ".mp3");
                            if (_Aduio == null) _Aduio = new Skill_Audio(this); //创建一个音效的对象。
                            _Aduio.SetAnimClip(clip);
                        }
                        else if(ite.Key.Equals("特效"))
                        {
                            GameObject clip = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameDate/Effect/Skill/" + it + ".prefab");
                            if (_Effect == null) _Effect = new Skill_Effects(this);
                            _Effect.SetGameClip(clip); //
                        }
                    }
                }
            }
        }
    }

    public static Player Init(string path)
    {
        if (path != null)
        {
            string str = "Assets/aaa/" + path + ".prefab";
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(str);
            if (obj != null)
            {
                Player player = Instantiate(obj).GetComponent<Player>();
                player.overrideController = new AnimatorOverrideController();
                player.controller = Resources.Load<RuntimeAnimatorController>("Player");
                player.overrideController.runtimeAnimatorController = player.controller;
                player.overrideController["Run3"] = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameDate/Anim/Run3.anim");
                player.anim.runtimeAnimatorController = player.overrideController;
                player.audioSource = player.gameObject.AddComponent<AudioSource>();
                player.effectsparent = player.transform.Find("effectsparent");
                player.gameObject.name = path;
                player.LoadAllSkill(path);
                return player;
            }
        }
        return null;
    }



    private void LoadAllSkill(string name)
    {
        if (File.Exists("Assets/" + name + ".txt"))
        {
            string str = File.ReadAllText("Assets/" + name + ".txt");
            List<SkillXml> skills = JsonConvert.DeserializeObject<List<SkillXml>>(str);
            foreach (var item in skills)
            {
                skillsList.Add(item.name, new List<SkillBase>());
                foreach (var ite in item.Dicskills)
                {
                    foreach (var it in ite.Value)
                    {
                        if (ite.Key.Equals("动画"))
                        {
                            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameDate/Anim/" + it.name + ".anim");
                            Skill_Anim _Anim = new Skill_Anim(this);
                            _Anim.name = it.name;
                            _Anim.SetTime(it.Timer);
                            _Anim.SetAnimClip(clip);
                            skillsList[item.name].Add(_Anim);
                        }
                        else if (ite.Key.Equals("音效"))
                        {
                            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameDate/Audio/" + it.name + ".mp3");
                            Skill_Audio _Aduio = new Skill_Audio(this);
                            _Aduio.name = it.name;
                            _Aduio.SetAnimClip(clip);
                            _Aduio.SetTime(it.Timer);
                            skillsList[item.name].Add(_Aduio);
                        }
                        else if (ite.Key.Equals("特效"))
                        {
                            GameObject clip = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameDate/Effect/Skill/" + it.name + ".prefab");
                            Skill_Effects _Effect = new Skill_Effects(this);
                            _Effect.name = it.name;
                            _Effect.SetGameClip(clip);
                            _Effect.SetTime(it.Timer);
                            skillsList[item.name].Add(_Effect);
                        }
                        else if(ite.Key.Equals("范围检测"))
                        {
                            Skill_Range _Range = new Skill_Range(this);
                            _Range.name = it.name;
                            _Range.Timer = it.Timer;
                            _Range.SetRangeData(it.ang, it.dis,it.atk);
                            skillsList[item.name].Add(_Range);
                        }
                    }
                }
            }
        }
        foreach (var item in skillsList)
        {
            skillPlay.Add(item.Value);
        }

    }

    public void Save()
    {
        List<SkillXml> skills = new List<SkillXml>();
        foreach (var item in skillsList)
        {
            SkillXml skillxml = new SkillXml();
            skillxml.name = item.Key;
            foreach (var ite in item.Value)
            {
                if (ite is Skill_Anim)
                {
                    if (!skillxml.Dicskills.ContainsKey("动画"))
                    {
                        skillxml.Dicskills.Add("动画", new List<SkillData>());
                    }
                    SkillData skilldata = new SkillData();
                    skilldata.name = ite.name;
                    skilldata.Timer = ite.Timer;
                    skillxml.Dicskills["动画"].Add(skilldata);
                }
                else if (ite is Skill_Audio)
                {
                    if (!skillxml.Dicskills.ContainsKey("音效"))
                    {
                        skillxml.Dicskills.Add("音效", new List<SkillData>());
                    }
                    SkillData skilldata = new SkillData();
                    skilldata.name = ite.name;
                    skilldata.Timer = ite.Timer;
                    skillxml.Dicskills["音效"].Add(skilldata);
                }
                else if (ite is Skill_Effects)
                {
                    if (!skillxml.Dicskills.ContainsKey("特效"))
                    {
                        skillxml.Dicskills.Add("特效", new List<SkillData>());
                    }
                    SkillData skilldata = new SkillData();
                    skilldata.name = ite.name;
                    skilldata.Timer = ite.Timer;
                    skillxml.Dicskills["特效"].Add(skilldata);
                }
                else if(ite is Skill_Range)
                {
                    if(!skillxml.Dicskills.ContainsKey("范围检测"))
                    {
                        skillxml.Dicskills.Add("范围检测", new List<SkillData>());
                    }
                    SkillData skilldata = new SkillData();
                    skilldata.name = ite.name;
                    skilldata.Timer = ite.Timer;
                    skilldata.ang = (ite as Skill_Range).ang;
                    skilldata.dis = (ite as Skill_Range).dis;
                    skilldata.atk = (ite as Skill_Range).atk;
                    skillxml.Dicskills["范围检测"].Add(skilldata);
                }
            }
            skills.Add(skillxml);
        }
        string str = JsonConvert.SerializeObject(skills);
        File.WriteAllText("Assets/" + gameObject.name + ".txt", str);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    public List<SkillBase> AddNewSkill(string newSkillName)
    {
        if (skillsList.ContainsKey(newSkillName))
        {
            return skillsList[newSkillName];
        }
        skillsList.Add(newSkillName, new List<SkillBase>());
        return skillsList[newSkillName];
    }

    public List<SkillBase> GetSkill(string key)
    {
        if (skillsList.ContainsKey(key))
        {
            return skillsList[key];
        }
        return null;
    }

    public void RevSkill(string key)
    {
        if (skillsList.ContainsKey(key))
        {
            skillsList.Remove(key);
        }
    }

}
public class SkillXml
{
    public string name;

    public Dictionary<string, List<SkillData>> Dicskills = new Dictionary<string, List<SkillData>>();
}

public class SkillData
{
    public string name;
    public float Timer = 0;
    public float dis = 0;
    public float ang = 0;
    public float atk=0;

}

