using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaContainer : MonoBehaviour
{
    //lua文件
    public TextAsset luaFile;
    //lua环境
    LuaEnv luaEnv;

    void Start()
    {
        luaEnv = new LuaEnv();
        if (luaFile != null)
        {
            Debug.Log("加载不为空");
            luaEnv.DoString(luaFile.text);
        }
    }

}
