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
        string[] strs = Directory.GetFiles(path,"*.lua");//获取所有带lua后缀的文件
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
            //得到该路径中所有.txt的文件进行删除
        }
        string fileName;
        foreach (string str in strs)
        {
            //获得新文件名用于拷贝
            fileName = newPath + str.Substring(str.LastIndexOf("/")+1) + ".txt";//截取文件名获得新文件名
            File.Copy(str, fileName);
        }
        AssetDatabase.Refresh();//刷新Unity
        string[] Newstrs = Directory.GetFiles(newPath, "*.txt");
        foreach (string str in Newstrs)
        {
            AssetImporter importer = AssetImporter.GetAtPath(str.Substring(str.IndexOf("Assets")));//根据路径获得对象(路径必须是Assets/.../...)
            if(importer != null)
            {
                importer.assetBundleName = "lua";//把所有txt打包
            }
        }
        AssetDatabase.Refresh();//刷新Unity
    }
}
