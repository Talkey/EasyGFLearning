using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ű��ĵ���ģ�����
/// </summary>
public abstract class ScriptSingleton<T> : MonoBehaviour where T : ScriptSingleton<T>
{

    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //�ӳ�������T�ű��Ķ���
                _instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogError("�����еĵ����ű����� > 1:" + _instance.GetType().ToString());
                    return _instance;
                }

                //�������Ҳ��������
                if (_instance == null)
                {
                    string instanceName = typeof(T).Name;
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                    {
                        instanceGO = new GameObject(instanceName);
                        DontDestroyOnLoad(instanceGO);
                        _instance = instanceGO.AddComponent<T>();
                        DontDestroyOnLoad(_instance);
                    }
                    else
                    {
                        //�������Ѵ���ͬ����Ϸ����ʱ�ʹ�ӡ��ʾ
                        Debug.LogError("�������Ѵ��ڵ����ű������ص���Ϸ����:" + instanceGO.name);
                    }
                }
            }

            return _instance;
        }
    }

    void OnDestroy()
    {
        _instance = null;
    }

}