using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCircleCheck : MonoBehaviour
{
    public float m_checkRangeActive = 3f;  
    public float m_tirggerRange = 0.2f;
    public GameObject m_taget;
    public Action<bool> m_call;
    public bool m_isTirgger = true;

    private CricleCollision cricle1;
    private CricleCollision cricle2;
    void Start()
    {
        cricle1 = new CricleCollision(this.transform.position.x, this.transform.position.z, m_tirggerRange);
        cricle2 = new CricleCollision(0, 0, m_tirggerRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_taget)
        {
            if (Vector3.Distance(this.transform.position, m_taget.transform.position) <= m_checkRangeActive)
            {
                cricle2.RefreshPos(m_taget.transform.position.x, m_taget.transform.position.z);
                if (CircleCollision.CricleCollisionCheck(cricle1, cricle2))
                {
                    if (m_isTirgger)
                    {
                        m_call(true);
                        m_isTirgger = false;
                    }
                }
                else
                {
                    if (!m_isTirgger)
                    {
                        m_isTirgger = true;
                    }
                }
            }

        }
    }
}
