using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyGF
{
    public class ProcedureBase : FsmState<ProcedureManager>
    {
        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            base.OnEnter(fsm);
            Debug.Log("�������̣�" + GetType().FullName);
        }

        public override void OnLeave(Fsm<ProcedureManager> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Debug.Log("�뿪���̣�" + GetType().FullName);
        }
    }

}