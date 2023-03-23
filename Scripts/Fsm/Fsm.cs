using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ״̬��
/// </summary>
/// <typeparam name="T">״̬������������</typeparam>
public class Fsm<T> : IFsm where T : class
{
    public string Name { get; private set; }

    public Type OwnerType
    {
        get
        {
            return typeof(T);
        }
    }

    public bool IsDestroyed { get; private set; }

    public float CurrentStateTime { get; private set; }

    private Dictionary<string, FsmState<T>> m_States;

    private Dictionary<string, object> m_Datas;

    public FsmState<T> CurrentState { get; private set; }
    public T Owner { get; private set; }


    public Fsm(string name, T owner, params FsmState<T>[] states)
    {
        if (owner == null)
        {
            Debug.LogError("״̬��������Ϊ��");
        }

        if (states == null || states.Length < 1)
        {
            Debug.LogError("û��Ҫ��ӽ�״̬����״̬");
        }

        Name = name;
        Owner = owner;
        m_States = new Dictionary<string, FsmState<T>>();
        m_Datas = new Dictionary<string, object>();

        foreach (FsmState<T> state in states)
        {
            if (state == null)
            {
                Debug.LogError("Ҫ��ӽ�״̬����״̬Ϊ��");
            }

            string stateName = state.GetType().FullName;
            if (m_States.ContainsKey(stateName))
            {
                Debug.LogError("Ҫ��ӽ�״̬����״̬�Ѵ��ڣ�" + stateName);
            }

            m_States.Add(stateName, state);
            state.OnInit(this);
        }

        CurrentStateTime = 0f;
        CurrentState = null;
        IsDestroyed = false;

    }

    public TState GetState<TState>() where TState : FsmState<T>
    {
        return GetState(typeof(TState)) as TState;
    }

    public FsmState<T> GetState(Type stateType)
    {
        if (stateType == null)
        {
            Debug.LogError("Ҫ��ȡ��״̬Ϊ��");
        }

        if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
        {
            Debug.LogError("Ҫ��ȡ��״̬" + stateType.FullName + "û��ֱ�ӻ��ӵ�ʵ��" + typeof(FsmState<T>).FullName);
        }

        FsmState<T> state = null;
        if (m_States.TryGetValue(stateType.FullName, out state))
        {
            return state;
        }

        return null;
    }


    public void ChangeState<TState>() where TState : FsmState<T>
    {
        ChangeState(typeof(TState));
    }


    public void ChangeState(Type type)
    {
        if (CurrentState == null)
        {
            Debug.LogError("��ǰ״̬��״̬Ϊ�գ��޷��л�״̬");
        }

        FsmState<T> state = GetState(type);
        if (state == null)
        {
            Debug.Log("��ȡ����״̬Ϊ�գ��޷��л���" + type.FullName);
        }
        CurrentState.OnLeave(this, false);
        CurrentStateTime = 0f;
        CurrentState = state;
        CurrentState.OnEnter(this);
    }

    public void Shutdown()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLeave(this, true);
            CurrentState = null;
            CurrentStateTime = 0f;
        }

        foreach (KeyValuePair<string, FsmState<T>> state in m_States)
        {
            state.Value.OnDestroy(this);
        }

        m_States.Clear();
        m_Datas.Clear();

        IsDestroyed = true;
    }
    /// <summary>
    /// ��ʼ״̬��
    /// </summary>
    /// <typeparam name="TState">��ʼ��״̬����</typeparam>
    public void Start<TState>() where TState : FsmState<T>
    {
        Start(typeof(TState));
    }
    /// <summary>
    /// ��ʼ״̬��
    /// </summary>
    /// <param name="stateType">Ҫ��ʼ��״̬���͡�</param>
    public void Start(Type stateType)
    {
        if (CurrentState != null)
        {
            Debug.LogError("��ǰ״̬���ѿ�ʼ���޷��ٴο�ʼ");
        }

        if (stateType == null)
        {
            Debug.LogError("Ҫ��ʼ��״̬Ϊ�գ��޷���ʼ");
        }

        FsmState<T> state = GetState(stateType);
        if (state == null)
        {
            Debug.Log("��ȡ����״̬Ϊ�գ��޷���ʼ");
        }

        CurrentStateTime = 0f;
        CurrentState = state;
        CurrentState.OnEnter(this);
    }

    /// <summary>
    /// �׳�״̬���¼�
    /// </summary>
    /// <param name="sender">�¼�Դ</param>
    /// <param name="eventId">�¼����</param>
    public void FireEvent(object sender, int eventId)
    {
        if (CurrentState == null)
        {
            Debug.Log("��ǰ״̬Ϊ�գ��޷��׳��¼�");
        }


        CurrentState.OnEvent(this, sender, eventId, null);
    }
    public void Update(float elapseSeconds, float realElapseSeconds)
    {
        if (CurrentState == null)
        {
            return;
        }

        CurrentStateTime += elapseSeconds;
        CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
    }

    /// <summary>
    /// �Ƿ����״̬������
    /// </summary>
    public bool HasData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Ҫ��ѯ��״̬����������Ϊ��");
        }

        return m_Datas.ContainsKey(name);
    }

    /// <summary>
    /// ��ȡ״̬������
    /// </summary>
    public TDate GetData<TDate>(string name)
    {
        return (TDate)GetData(name);
    }

    /// <summary>
    /// ��ȡ״̬������
    /// </summary>
    public object GetData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Ҫ��ȡ��״̬����������Ϊ��");
        }

        object data = null;
        m_Datas.TryGetValue(name, out data);
        return data;
    }

    /// <summary>
    /// ����״̬������
    /// </summary>
    public void SetData(string name, object data)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Ҫ���õ�״̬����������Ϊ��");
        }

        m_Datas[name] = data;
    }

    /// <summary>
    /// �Ƴ�״̬������
    /// </summary>
    public bool RemoveData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Ҫ�Ƴ���״̬����������Ϊ��");
        }

        return m_Datas.Remove(name);
    }


}