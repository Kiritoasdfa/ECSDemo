using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.script.common
{
    public class StateControl
    {
        stateE m_State = stateE.ide_State;//默认是待机
        Dictionary<stateE, Action> m_DicAction = new Dictionary<stateE, Action>();
        public void Update()
        {
            if(m_State==stateE.ide_State)//如果是待机
            {
                m_DicAction[stateE.ide_State].Invoke();
            }
            else if(m_State==stateE.XL_State)//如果是巡逻
            {
                m_DicAction[stateE.XL_State].Invoke();
            }
            else if (m_State == stateE.Attack_State)//如果是战斗
            {
                m_DicAction[stateE.Attack_State].Invoke();
            }
            else if (m_State == stateE.End_State)//如果是战斗
            {
                m_DicAction[stateE.End_State].Invoke();
            }
        }
        public void init()
        {

        }
        public void AddAction(stateE state, Action action)
        {
            m_DicAction.Add(state, action);
        }
        public void SetState(stateE state)
        {
            m_State = state;
        }
    }
}

