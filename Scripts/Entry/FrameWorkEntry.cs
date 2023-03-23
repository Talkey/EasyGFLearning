using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����ڣ�ά������ģ�������
/// </summary>
public class FrameworkEntry : ScriptSingleton<FrameworkEntry>
{
    /// <summary>
    /// ����ģ�������������
    /// </summary>
    private LinkedList<ManagerBase> m_Managers = new LinkedList<ManagerBase>();

    void Update()
    {
        //��ѯ���й�����
        foreach (ManagerBase manager in m_Managers)
        {
            manager.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }

    void OnDestroy()
    {
        //�رղ��������й�����
        for (LinkedListNode<ManagerBase> current = m_Managers.Last; current != null; current = current.Previous)
        {
            current.Value.Shutdown();
        }
        m_Managers.Clear();
    }

    /// <summary>
    /// ��ȡָ��������
    /// </summary>
    public T GetManager<T>() where T : ManagerBase
    {
        Type managerType = typeof(T);
        foreach (ManagerBase manager in m_Managers)
        {
            if (manager.GetType() == managerType)
            {
                return manager as T;
            }
        }

        //û�ҵ��ʹ���
        return CreateManager(managerType) as T;
    }

    /// <summary>
    /// ����ģ�������
    /// </summary>
    private ManagerBase CreateManager(Type managerType)
    {

        ManagerBase manager = (ManagerBase)Activator.CreateInstance(managerType);

        if (manager == null)
        {
            Debug.LogError("ģ�����������ʧ�ܣ�" + manager.GetType().FullName);
        }


        //����ģ�����ȼ����������������λ��
        LinkedListNode<ManagerBase> current = m_Managers.First;
        while (current != null)
        {

            if (manager.Priority > current.Value.Priority)
            {
                break;
            }

            current = current.Next;
        }
        if (current != null)
        {

            m_Managers.AddBefore(current, manager);
        }
        else
        {

            m_Managers.AddLast(manager);
        }

        //��ʼ��ģ�������
        manager.Init();
        return manager;
    }
}