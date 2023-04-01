using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace EasyGF
{
    /// <summary>
    /// ���̹�����
    /// </summary>
    public class ProcedureManager : ManagerBase
    {
        /// <summary>
        /// ״̬��������
        /// </summary>
        private FsmManager m_FsmManager;

        /// <summary>
        /// ���̵�״̬��
        /// </summary>
        private Fsm<ProcedureManager> m_ProcedureFsm;

        /// <summary>
        /// �������̵��б�
        /// </summary>
        private List<ProcedureBase> m_procedures;
        private FsmState<ProcedureManager> m_Procedures;

        /// <summary>
        /// �������
        /// </summary>
        private ProcedureBase m_EntranceProcedure;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    Debug.LogError("����״̬��Ϊ�գ��޷���ȡ��ǰ����");
                }
                return (ProcedureBase)m_ProcedureFsm.CurrentState;
            }
        }

        public override int Priority
        {
            get
            {
                return -10;
            }
        }

        public ProcedureManager()
        {
            m_FsmManager = FrameworkEntry.Instance.GetManager<FsmManager>();
            m_ProcedureFsm = null;
            m_procedures = new List<ProcedureBase>();
        }

        public override void Init()
        {

        }

        public override void Shutdown()
        {

        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }


        /// <summary>
        /// �������
        /// </summary>
        public void AddProcedure(ProcedureBase procedure)
        {
            if (procedure == null)
            {
                Debug.LogError("Ҫ��ӵ�����Ϊ��");
                return;
            }
            m_procedures.Add(procedure);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="procedure"></param>
        public void SetEntranceProcedure(ProcedureBase procedure)
        {
            m_EntranceProcedure = procedure;
        }


        /// <summary>
        /// ��������״̬��
        /// </summary>
        public void CreateProceduresFsm()
        {
            m_ProcedureFsm = m_FsmManager.CreateFsm(this, "", m_procedures.ToArray());

            if (m_EntranceProcedure == null)
            {
                Debug.LogError("�������Ϊ�գ��޷���ʼ����");
                return;
            }

            //��ʼ����
            m_ProcedureFsm.Start(m_EntranceProcedure.GetType());
        }


    }
}