using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ״̬���¼�����Ӧ����ģ��
/// </summary>
public delegate void FsmEventHandler<T>(Fsm<T> fsm, object sender, object userData) where T : class;


/// <summary>
/// ״̬����
/// </summary>
/// <typeparam name="T">״̬����������</typeparam>
public class FsmState<T> where T : class
{
    /// <summary>
    /// ״̬���ĵ��¼��ֵ�
    /// </summary>
    private Dictionary<int, FsmEventHandler<T>> m_EventHandlers;

    public FsmState()
    {
        m_EventHandlers = new Dictionary<int, FsmEventHandler<T>>();
    }


    /// <summary>
    /// ״̬��״̬��ʼ��ʱ����
    /// </summary>
    /// <param name="fsm">״̬������</param>
    public virtual void OnInit(Fsm<T> fsm)
    {

    }

    /// <summary>
    /// ״̬��״̬����ʱ����
    /// </summary>
    /// <param name="fsm">״̬������</param>
    public virtual void OnEnter(Fsm<T> fsm)
    {

    }

    /// <summary>
    /// ״̬��״̬��ѯʱ����
    /// </summary>
    /// <param name="fsm">״̬������</param>
    public virtual void OnUpdate(Fsm<T> fsm, float elapseSeconds, float realElapseSeconds)
    {

    }

    /// <summary>
    /// ״̬��״̬�뿪ʱ���á�
    /// </summary>
    /// <param name="fsm">״̬�����á�</param>
    /// <param name="isShutdown">�ǹر�״̬��ʱ����</param>
    public virtual void OnLeave(Fsm<T> fsm, bool isShutdown)
    {

    }

    /// <summary>
    /// ״̬��״̬����ʱ����
    /// </summary>
    /// <param name="fsm">״̬�����á�</param>
    public virtual void OnDestroy(Fsm<T> fsm)
    {
        m_EventHandlers.Clear();
    }

    /// <summary>
    /// ����״̬���¼���
    /// </summary>
    protected void SubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
    {
        if (eventHandler == null)
        {
            Debug.LogError("״̬���¼���Ӧ����Ϊ�գ��޷�����״̬���¼�");
        }

        if (!m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId] = eventHandler;
        }
        else
        {
            m_EventHandlers[eventId] += eventHandler;
        }
    }

    /// <summary>
    /// ȡ������״̬���¼���
    /// </summary>
    protected void UnsubscribeEvent(int eventId, FsmEventHandler<T> eventHandler)
    {
        if (eventHandler == null)
        {
            Debug.LogError("״̬���¼���Ӧ����Ϊ�գ��޷�ȡ������״̬���¼�");
        }

        if (m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId] -= eventHandler;
        }
    }

    /// <summary>
    /// ��Ӧ״̬���¼���
    /// </summary>
    public void OnEvent(Fsm<T> fsm, object sender, int eventId, object userData)
    {
        FsmEventHandler<T> eventHandlers = null;
        if (m_EventHandlers.TryGetValue(eventId, out eventHandlers))
        {
            if (eventHandlers != null)
            {
                eventHandlers(fsm, sender, userData);
            }
        }
    }

    /// <summary>
    /// �л�״̬
    /// </summary>
    protected void ChangeState<TState>(Fsm<T> fsm) where TState : FsmState<T>
    {
        ChangeState(fsm, typeof(TState));
    }

    /// <summary>
    /// �л�״̬
    /// </summary>
    protected void ChangeState(Fsm<T> fsm, Type type)
    {
        if (fsm == null)
        {
            Debug.Log("��Ҫ�л�״̬��״̬��Ϊ�գ��޷��л�");
        }

        if (type == null)
        {
            Debug.Log("��Ҫ�л�����״̬Ϊ�գ��޷��л�");
        }

        if (!typeof(FsmState<T>).IsAssignableFrom(type))
        {
            Debug.Log("Ҫ�л���״̬û��ֱ�ӻ���ʵ��FsmState<T>���޷��л�");
        }

        fsm.ChangeState(type);
    }

}