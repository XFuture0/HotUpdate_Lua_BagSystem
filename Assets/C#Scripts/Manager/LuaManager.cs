using System.IO;
using UnityEngine;
using XLua;
//Lua管理器
public class LuaManager : SingleTon<LuaManager>
{
    //打包时要把.lua改为.txt
    private LuaEnv luaEnv;
    public void Init()
    {
        if(luaEnv != null)
        {
            return;
        }
        luaEnv = new LuaEnv();
       // luaEnv.AddLoader(MyCustomLoader);
        luaEnv.AddLoader(MyCustomABLoader);

    }
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;//得到Lua中的_G表
        }
    }
    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require('{0}')", fileName);
        DoString(str);
    }
    public void DoString(string str)
    {
        if(luaEnv == null)
        {
            Debug.Log("Lua未初始化");
            return;
        }
        luaEnv.DoString(str);
    }
    private byte[] MyCustomLoader(ref string FileName)
    {
        //传入的参数是require执行的lua脚本文件名
        string path = Application.dataPath + "/Lua/" + FileName + ".lua";//拼接Lua文件的路径
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("找不到Lua文件：" + path);
        }
        // 自定义加载Lua文件的规则
        return null;
    }
    private byte[] MyCustomABLoader(ref string FileName)
    {
        //string path = Application.streamingAssetsPath + "/lua";//拼接Lua文件的AB包路径
        //AssetBundle ab = AssetBundle.LoadFromFile(path);
        //TextAsset text = ab.LoadAsset<TextAsset>(FileName + ".lua");//获取lua转text
        TextAsset Luatext = ABManager.Instance.LoadRes<TextAsset>("lua",FileName + ".lua");//使用AB包管理器查找
        if (Luatext != null)
        {
            return Luatext.bytes;
        }
        else
        {
            Debug.Log("重定向失败：" + FileName);
            return null;
        }
    }
    public void Tick()
    {
        if (luaEnv == null)
        {
            Debug.Log("Lua未初始化");
            return;
        }
        luaEnv.Tick();
    }
    public void Dispose()
    {
        if (luaEnv == null)
        {
            Debug.Log("Lua未初始化");
            return;
        }
        luaEnv.Dispose();
        luaEnv = null;
    }
}
