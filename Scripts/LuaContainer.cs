using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaContainer : MonoBehaviour
{
    //lua�ļ�
    public TextAsset luaFile;
    //lua����
    LuaEnv luaEnv;

    void Start()
    {
        luaEnv = new LuaEnv();
        if (luaFile != null)
        {
            Debug.Log("���ز�Ϊ��");
            luaEnv.DoString(luaFile.text);
        }
    }

}
