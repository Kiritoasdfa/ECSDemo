using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.script.common
{
    public  enum stateE
    {
        ide_State,//待机
        XL_State,//巡逻
        Attack_State,//攻击
        End_State//结束
    }
    public class stateBase //状态基类
    {
        public Player m_player;

        public void init(Player pPlayer)
        {
            m_player = pPlayer;
        }

        public virtual void SetState()
        {

        }
        public virtual void Do()
        {

        }
    }

    class ideState : stateBase
    {
        public override void Do()
        {
            base.Do();
        }
    }

    class XLState : stateBase
    {
        public override void Do()
        {
            base.Do();
        }
    }

}
