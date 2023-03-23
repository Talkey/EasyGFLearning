using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmManager : ManagerBase
{
    /// <summary>
    /// ����״̬�����ֵ䣨�����״̬���ӿڵ����þ���ʾ�����ˣ�
    /// </summary>
    private Dictionary<string, IFsm> m_Fsms;
    private List<IFsm> m_TempFsms;


    public override int Priority
    {
        get
        {
            return 60;
        }
    }


    public override void Init()
    {
        m_Fsms = new Dictionary<string, IFsm>();
        m_TempFsms = new List<IFsm>();
    }


    /// <summary>
    /// �رղ�����״̬��������
    /// </summary>
    public override void Shutdown()
    {
        foreach (KeyValuePair<string, IFsm> fsm in m_Fsms)
        {
            fsm.Value.Shutdown();
        }


        m_Fsms.Clear();
        m_TempFsms.Clear();
    }


    /// <summary>
    /// ��ѯ״̬��������
    /// </summary>
    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        m_TempFsms.Clear();
        if (m_Fsms.Count <= 0)
        {
            return;
        }


        foreach (KeyValuePair<string, IFsm> fsm in m_Fsms)
        {
            m_TempFsms.Add(fsm.Value);
        }


        foreach (IFsm fsm in m_TempFsms)
        {
            if (fsm.IsDestroyed)
            {
                continue;
            }
            //��ѯ״̬��
            fsm.Update(elapseSeconds, realElapseSeconds);
        }
    }

    /// <summary>
    /// �Ƿ����״̬��
    /// </summary>
    private bool HasFsm(string fullName)
    {
        return m_Fsms.ContainsKey(fullName);
    }

    /// <summary>
    /// �Ƿ����״̬��
    /// </summary>
    public bool HasFsm<T>()
    {
        return HasFsm(typeof(T));
    }

    /// <summary>
    /// �Ƿ����״̬��
    /// </summary>
    public bool HasFsm(Type type)
    {
        return HasFsm(type.FullName);
    }

    /// <summary>
    /// ����״̬����
    /// </summary>
    /// <typeparam name="T">״̬������������</typeparam>
    /// <param name="name">״̬������</param>
    /// <param name="owner">״̬��������</param>
    /// <param name="states">״̬��״̬����</param>
    /// <returns>Ҫ������״̬��</returns>
    public Fsm<T> CreateFsm<T>(T owner, string name = "", params FsmState<T>[] states) where T : class
    {
        if (HasFsm<T>())
        {
            Debug.LogError("Ҫ������״̬���Ѵ���");
        }
        if (name == "")
        {
            name = typeof(T).FullName;
        }
        Fsm<T> fsm = new Fsm<T>(name, owner, states);
        m_Fsms.Add(name, fsm);
        return fsm;
    }

    /// <summary>
    /// ����״̬��
    /// </summary>
    public bool DestroyFsm(string name)
    {
        IFsm fsm = null;
        if (m_Fsms.TryGetValue(name, out fsm))
        {
            fsm.Shutdown();
            return m_Fsms.Remove(name);
        }

        return false;
    }
    public bool DestroyFsm<T>() where T : class
    {
        return DestroyFsm(typeof(T).FullName);
    }
    public bool DestroyFsm(IFsm fsm)
    {
        return DestroyFsm(fsm.Name);
    }
}