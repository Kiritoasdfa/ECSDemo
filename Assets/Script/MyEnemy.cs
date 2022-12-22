using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    public int Hp = 100;

    public int SetHP(int num, GameObject item)
    {
        Hp -= num;
        if(Hp<=0)
        {
            EnemyMgr.Instance.EnemyList.Remove(item);
            Destroy(item);
            GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Enemy"));
            
            obj.transform.position = item.transform.position;
            obj.tag = "WuPin";
        }
        return Hp;
    }

}
