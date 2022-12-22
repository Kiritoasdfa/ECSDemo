using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : ObjectBase
{
    public player_info m_info;
    public PlayerObj(player_info info)
    {
        m_info = info;
    }

    public override void SetPos(Vector3 pos)
    {
        base.SetPos(pos);
    }
    public void SetPos(Vector3 pos, float speed)
    {
        //平滑移动
    }
    public override void OnCreate()
    {
        base.OnCreate();
        m_pate = m_go.AddComponent<UIPate>();
        m_pate.InitPate();
        m_pate.m_gather.SetActive(false);
        m_pate.SetData(m_info.m_name, m_info.m_HP / m_info.m_hpMax, m_info.m_MP / m_info.m_mpMax);
    }
    public void AddBuff(string path)
    {

    }

}

public class HostPlayer:PlayerObj
{
    Player player;
    public HostPlayer (player_info info):base(info)
    {
        m_insID = info.ID;
        m_modelPath = info.m_res;
    }
    public override void CreateObj(MonsterType type)
    {
        base.CreateObj(type);
        SetPos(m_info.m_pos);
    }
    public override void OnCreate()
    {
        base.OnCreate();
        player = m_go.AddComponent<Player>();
        player.InitData();

    }
    public void JoystickHandlerMoving(float h,float v)
    {
        if (Mathf.Abs(h) > 0.05f || (Mathf.Abs(v) > 0.05f))
        {
            MoveByTranslate(new Vector3(m_go.transform.position.x+h, m_go.transform.position.y, m_go.transform.position.z + v), Vector3.forward * Time.deltaTime * 10);
        }
    }
    public void JoyButtonHandler(string btnName)
    {

        switch(btnName)
        {
            case "Attack":
                player.SetData("Attack");
                player.play();
                break;
            case "Skill1":
                player.SetData("Skill1");
                player.play();
                break;
        }
    }
}
