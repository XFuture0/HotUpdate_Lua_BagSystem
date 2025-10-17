using System.IO;
using UnityEngine;
using XLua;
//Lua������
public class LuaManager : SingleTon<LuaManager>
{
    //���ʱҪ��.lua��Ϊ.txt
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
            return luaEnv.Global;//�õ�Lua�е�_G��
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
            Debug.Log("Luaδ��ʼ��");
            return;
        }
        luaEnv.DoString(str);
    }
    private byte[] MyCustomLoader(ref string FileName)
    {
        //����Ĳ�����requireִ�е�lua�ű��ļ���
        string path = Application.dataPath + "/Lua/" + FileName + ".lua";//ƴ��Lua�ļ���·��
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("�Ҳ���Lua�ļ���" + path);
        }
        // �Զ������Lua�ļ��Ĺ���
        return null;
    }
    private byte[] MyCustomABLoader(ref string FileName)
    {
        //string path = Application.streamingAssetsPath + "/lua";//ƴ��Lua�ļ���AB��·��
        //AssetBundle ab = AssetBundle.LoadFromFile(path);
        //TextAsset text = ab.LoadAsset<TextAsset>(FileName + ".lua");//��ȡluaתtext
        TextAsset Luatext = ABManager.Instance.LoadRes<TextAsset>("lua",FileName + ".lua");//ʹ��AB������������
        if (Luatext != null)
        {
            return Luatext.bytes;
        }
        else
        {
            Debug.Log("�ض���ʧ�ܣ�" + FileName);
            return null;
        }
    }
    public void Tick()
    {
        if (luaEnv == null)
        {
            Debug.Log("Luaδ��ʼ��");
            return;
        }
        luaEnv.Tick();
    }
    public void Dispose()
    {
        if (luaEnv == null)
        {
            Debug.Log("Luaδ��ʼ��");
            return;
        }
        luaEnv.Dispose();
        luaEnv = null;
    }
}
