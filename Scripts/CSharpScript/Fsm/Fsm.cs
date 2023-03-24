using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 状态机
/// </summary>
/// <typeparam name="T">状态机持有者类型</typeparam>
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
            Debug.LogError("状态机持有者为空");
        }

        if (states == null || states.Length < 1)
        {
            Debug.LogError("没有要添加进状态机的状态");
        }

        Name = name;
        Owner = owner;
        m_States = new Dictionary<string, FsmState<T>>();
        m_Datas = new Dictionary<string, object>();

        foreach (FsmState<T> state in states)
        {
            if (state == null)
            {
                Debug.LogError("要添加进状态机的状态为空");
            }

            string stateName = state.GetType().FullName;
            if (m_States.ContainsKey(stateName))
            {
                Debug.LogError("要添加进状态机的状态已存在：" + stateName);
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
            Debug.LogError("要获取的状态为空");
        }

        if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
        {
            Debug.LogError("要获取的状态" + stateType.FullName + "没有直接或间接的实现" + typeof(FsmState<T>).FullName);
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
            Debug.LogError("当前状态机状态为空，无法切换状态");
        }

        FsmState<T> state = GetState(type);
        if (state == null)
        {
            Debug.Log("获取到的状态为空，无法切换：" + type.FullName);
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
    /// 开始状态机
    /// </summary>
    /// <typeparam name="TState">开始的状态类型</typeparam>
    public void Start<TState>() where TState : FsmState<T>
    {
        Start(typeof(TState));
    }
    /// <summary>
    /// 开始状态机
    /// </summary>
    /// <param name="stateType">要开始的状态类型。</param>
    public void Start(Type stateType)
    {
        if (CurrentState != null)
        {
            Debug.LogError("当前状态机已开始，无法再次开始");
        }

        if (stateType == null)
        {
            Debug.LogError("要开始的状态为空，无法开始");
        }

        FsmState<T> state = GetState(stateType);
        if (state == null)
        {
            Debug.Log("获取到的状态为空，无法开始");
        }

        CurrentStateTime = 0f;
        CurrentState = state;
        CurrentState.OnEnter(this);
    }

    /// <summary>
    /// 抛出状态机事件
    /// </summary>
    /// <param name="sender">事件源</param>
    /// <param name="eventId">事件编号</param>
    public void FireEvent(object sender, int eventId)
    {
        if (CurrentState == null)
        {
            Debug.Log("当前状态为空，无法抛出事件");
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
    /// 是否存在状态机数据
    /// </summary>
    public bool HasData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("要查询的状态机数据名字为空");
        }

        return m_Datas.ContainsKey(name);
    }

    /// <summary>
    /// 获取状态机数据
    /// </summary>
    public TDate GetData<TDate>(string name)
    {
        return (TDate)GetData(name);
    }

    /// <summary>
    /// 获取状态机数据
    /// </summary>
    public object GetData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("要获取的状态机数据名字为空");
        }

        object data = null;
        m_Datas.TryGetValue(name, out data);
        return data;
    }

    /// <summary>
    /// 设置状态机数据
    /// </summary>
    public void SetData(string name, object data)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("要设置的状态机数据名字为空");
        }

        m_Datas[name] = data;
    }

    /// <summary>
    /// 移除状态机数据
    /// </summary>
    public bool RemoveData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("要移除的状态机数据名字为空");
        }

        return m_Datas.Remove(name);
    }


}