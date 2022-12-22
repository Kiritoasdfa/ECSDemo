using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlGame;
using System;

public class JoyStickMgr : Singleton<JoyStickMgr>
{
    public GameObject m_joyGo;
    public ETCJoystick m_joystick;
    public List<ETCButton> m_skillBtn;
    HostPlayer m_target;
    Player m_player;

    public bool JoyActive
    {
        set
        {
            if(m_joyGo.activeSelf!=value)
            {
                m_joyGo.SetActive(value);
            }
        }
    }

    internal void SetJoyArg(Camera m_main, Player play)
    {
        m_player = play;

        SetJoyticck();
        
    }

    public void SetJoyArg(Camera camera, HostPlayer target)
    {
        m_target = target;
        m_joystick.cameraLookAt = target.m_go.transform;
        m_joystick.cameraTransform = camera.transform;
        SetJoyticck2();
    }

    private void SetJoyticck2()
    {
        if(m_joystick&&m_target.m_go)
        {
            m_joystick.OnPressLeft.AddListener(() => m_target.JoystickHandlerMoving(m_joystick.axisX.axisValue, m_joystick.axisY.axisValue));
            m_joystick.OnPressRight.AddListener(() => m_target.JoystickHandlerMoving(m_joystick.axisX.axisValue, m_joystick.axisY.axisValue));
            m_joystick.OnPressUp.AddListener(() => m_target.JoystickHandlerMoving(m_joystick.axisX.axisValue, m_joystick.axisY.axisValue));
            m_joystick.OnPressDown.AddListener(() => m_target.JoystickHandlerMoving(m_joystick.axisX.axisValue, m_joystick.axisY.axisValue));
        }

        if(m_skillBtn.Count!=0&&m_target.m_go)
        {
            foreach(var item in m_skillBtn)
            {
                item.onDown.AddListener(() => m_target.JoyButtonHandler(item.name));
            }
        }
    }

    private void SetJoyticck()
    {

        if (m_skillBtn.Count!=0&&m_player)
        {
            for(int i=0;i< m_player.skillPlay.Count;i++)
            {
                int j = i;
                m_skillBtn[i].gameObject.SetActive(true);
                m_skillBtn[i].onDown.AddListener(() =>
                {
                    foreach(var item in m_player.skillPlay[j])
                    {
                        item.Init();
                        item.Play();
                    }
                });
            }
        }
    }
}
