using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCObj : ObjectBase
{
    public npc_info m_info;
    public NPCObj(npc_info info)
    {
        m_info = info;
        m_insID = info.ID;
        m_modelPath = info.m_res;
    }

    public NPCObj(int plot, object_info info)
    {
        m_info = new npc_info(plot,info);
        m_insID = info.ID;
        m_modelPath = info.m_res;

    }

    public override void CreateObj(MonsterType type)
    {
        SetPos(m_info.m_pos);
        base.CreateObj(type);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        

        StaticCircleCheck check = m_go.AddComponent<StaticCircleCheck>();
        check.m_call = (isenter) =>
        {
            Debug.Log("进入检测范围");

        };

    }

}


