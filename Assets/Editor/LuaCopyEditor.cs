using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class LuaCopyEditor : Editor
{
    [MenuItem("XLua/CopyLuaToText")]
    public static void CopyLuaToText()
    {
        string path = Application.dataPath + "/Lua/";
        if (!Directory.Exists(path))
        {
            return;
        }
        string[] strs = Directory.GetFiles(path,"*.lua");//��ȡ���д�lua��׺���ļ�
        string newPath = Application.dataPath + "/LuaTxt/";
        if (!Directory.Exists(newPath))
        {
            Directory.CreateDirectory(newPath);
        }
        else
        {
            string[] oldstrs = Directory.GetFiles(newPath, "*.txt");
            foreach (string str in oldstrs)
            {
                File.Delete(str);
            }
            //�õ���·��������.txt���ļ�����ɾ��
        }
        string fileName;
        foreach (string str in strs)
        {
            //������ļ������ڿ���
            fileName = newPath + str.Substring(str.LastIndexOf("/")+1) + ".txt";//��ȡ�ļ���������ļ���
            File.Copy(str, fileName);
        }
        AssetDatabase.Refresh();//ˢ��Unity
        string[] Newstrs = Directory.GetFiles(newPath, "*.txt");
        foreach (string str in Newstrs)
        {
            AssetImporter importer = AssetImporter.GetAtPath(str.Substring(str.IndexOf("Assets")));//����·����ö���(·��������Assets/.../...)
            if(importer != null)
            {
                importer.assetBundleName = "lua";//������txt���
            }
        }
        AssetDatabase.Refresh();//ˢ��Unity
    }
}
