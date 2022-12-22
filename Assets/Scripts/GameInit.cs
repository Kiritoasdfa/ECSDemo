using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInit : MonoBehaviour
{
    public GameObject[] DontDestory;

    public List<ETCButton> Attack;

    public ETCJoystick Joystick;


    void Start()
    {
        foreach(var item in DontDestory)
        {
            GameObject.DontDestroyOnLoad(item);
        }
        GameSceneUtils.LoadSceneAsync("Battle", () =>
        {
            JoyStickMgr.Instance.m_joyGo = DontDestory[0];
            JoyStickMgr.Instance.m_joystick = Joystick;
            JoyStickMgr.Instance.m_skillBtn = Attack;

            //EnemyMgr.Instance.Init();
            GameData.Instance.InitByRoleName("Teddy");
            //GameData.Instance.InitTaskData();

            World.Instance.Init();
        });

    }

    
}
