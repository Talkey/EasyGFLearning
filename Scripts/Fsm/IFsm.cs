using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬���ӿ�
/// </summary>
public interface IFsm
{
    /// <summary>
    /// ״̬������
    /// </summary>
    string Name { get; }

    /// <summary>
    /// ״̬������������
    /// </summary>
    Type OwnerType { get; }

    /// <summary>
    /// ״̬���Ƿ�����
    /// </summary>
    bool IsDestroyed { get; }

    /// <summary>
    /// ��ǰ״̬����ʱ��
    /// </summary>
    float CurrentStateTime { get; }

    /// <summary>
    /// ״̬����ѯ
    /// </summary>
    void Update(float elapseSeconds, float realElapseSeconds);

    /// <summary>
    /// �رղ�����״̬����
    /// </summary>
    void Shutdown();

}