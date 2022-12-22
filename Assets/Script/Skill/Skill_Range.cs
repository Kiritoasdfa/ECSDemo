using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Skill_Range : SkillBase
{
    private Player m_player;
    public float ang = 0;
    public float dis = 0;
    public float atk = 0;

    public Skill_Range(Player player)
    {
        m_player = player;
    }

    public override void Play()
    {

        base.Play();
        Attack();

    }

    async void Attack()
    {
        await Task.Delay((int)(Timer * 1000));
        if (EnemyMgr.Instance.EnemyList.Count != 0)
        {
            foreach (var item in EnemyMgr.Instance.EnemyList)
            {
                Vector3 pianyi = item.transform.position - m_player.transform.position;
                Vector3 qianfang = m_player.transform.forward;
                float jiaodu = Vector3.Angle(pianyi, qianfang);
                if (Vector3.Distance(item.transform.position, m_player.transform.position) < dis && jiaodu < ang)
                {
                    if (item.GetComponent<MyEnemy>().SetHP((int)atk, item) <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }

    public void SetRangeData(float _ang, float _dis, float _atk)
    {
        ang = _ang;
        dis = _dis;
        atk = _atk;
    }

    public void SetTime(float effectstimer)
    {
        Timer = effectstimer;
    }
}
