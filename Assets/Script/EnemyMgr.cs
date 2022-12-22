using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlGame;

public class EnemyMgr : Singleton<EnemyMgr>
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public void Init()
    {
        for(int i=0;i<10;i++)
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);
            GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("TeddyBear"));
            obj.transform.position = new Vector3(x, 0, y);
            obj.tag = "Enemy";
            EnemyList.Add(obj);
        }
    }
}
