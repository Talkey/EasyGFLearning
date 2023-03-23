using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Procedure_Start : ProcedureBase
{
    public override void OnUpdate(Fsm<ProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState<Procedure_Play>(fsm);
        }
    }

}

public class Procedure_Play : ProcedureBase
{
    public override void OnUpdate(Fsm<ProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (Input.GetMouseButtonDown(0))
        {
            ChangeState<Procedure_Over>(fsm);
        }
    }

}

public  class Procedure_Over:ProcedureBase
{
    public override void OnUpdate(Fsm<ProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

    }
}

public class ProcedureTest : MonoBehaviour
{

    private void Start()
    {
        ProcedureManager procedureManager = FrameworkEntry.Instance.GetManager<ProcedureManager>();

        //添加入口流程
        Procedure_Start entranceProcedure = new Procedure_Start();
        procedureManager.AddProcedure(entranceProcedure);
        procedureManager.SetEntranceProcedure(entranceProcedure);

        //添加其他流程
        procedureManager.AddProcedure(new Procedure_Play());
        procedureManager.AddProcedure(new Procedure_Over());

        //创建流程状态机
        procedureManager.CreateProceduresFsm();
    }

}
