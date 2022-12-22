using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobbysys :UIbase
{
    private Image m_head;
    private List<Image> m_buffs;

    public override void DoCreate(string path)
    {
        m_buffs = new List<Image>();
        base.DoCreate(path);

    }

    public override void DoShow(bool active)
    {
        base.DoShow(active);
        m_head = m_go.transform.Find("head").GetComponent<Image>();
        m_head.sprite = Resources.Load<Sprite>("icon/touxiangkuang1");
        Transform buffgo = m_go.transform.Find("bufflayout").transform;
        for (int i = 0; i < buffgo.childCount; i++)
        {
            m_buffs.Add(buffgo.GetChild(i).GetComponent<Image>());
        }
        foreach(var item in m_buffs)
        {
            item.sprite = Resources.Load<Sprite>("icon/宝箱");
        }
    }

    public override void Destory()
    {
        base.Destory();
    }
}
