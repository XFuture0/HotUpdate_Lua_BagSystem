using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABManager : MonoBehaviour
{
    private static ABManager instance;
    public static ABManager Instance { get; private set; }
    private AssetBundle MainAB = null;//��AB��
    private AssetBundleManifest manifest = null;//��AB���嵥
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();//�ֵ�洢AB��
    private string PathUrl//��AB��·��
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    private string MainABName//��AB�����ƣ���ͬƽ̨��
    {
        get
        {
#if   UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void LoadABDepend(string abname)
    {
        if (MainAB == null)//������AB��
        {
            MainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = MainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        string[] strs = manifest.GetAllDependencies(abname);
        foreach (string str in strs)//��ȡ��ǰab������
        {
            if (!abDic.ContainsKey(str))
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + str);
                abDic.Add(str, bundle);
            }
        }
        if (!abDic.ContainsKey(abname))//���ص�ǰab��
        {
            AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + abname);
            abDic.Add(abname, ab);
        }
    }
    public Object LoadRes(string abname, string resname)//ͬ��������Դ
    {
        LoadABDepend(abname);
        Object obj = abDic[abname].LoadAsset(resname);
        return obj;
    }
    public Object LoadRes(string abname, string resname,System.Type type)//ͬ��������Դ(����)
    {
        LoadABDepend(abname);
        Object obj = abDic[abname].LoadAsset(resname,type);
        if (type == typeof(GameObject))
        {
            return Instantiate(obj);
        }
        return obj;
    }
    public T LoadRes<T>(string abname, string resname) where T : Object//ͬ��������Դ
    {
        LoadABDepend(abname);
        T obj = abDic[abname].LoadAsset<T>(resname);//���ͼ���
        return obj;
    }
    public void LoadRedAsync(string abname, string resname, UnityAction<Object> callback)//�첽������Դ
    {
        StartCoroutine(ReallyLoadRedAsync(abname, resname, callback));
    }
    private IEnumerator ReallyLoadRedAsync(string abname, string resname, UnityAction<Object> callback)
    {
        LoadABDepend(abname);
        AssetBundleRequest abr = abDic[abname].LoadAssetAsync(resname);
        yield return abr;
        callback(abr.asset);//�ص�
    }
    public void LoadRedAsync(string abname, string resname, UnityAction<Object> callback, System.Type type)//�첽������Դ
    {
        StartCoroutine(ReallyLoadRedAsync(abname, resname, callback,type));
    }
    private IEnumerator ReallyLoadRedAsync(string abname, string resname, UnityAction<Object> callback, System.Type type)
    {
        LoadABDepend(abname);
        AssetBundleRequest abr = abDic[abname].LoadAssetAsync(resname,type);
        yield return abr;
        callback(abr.asset);//�ص�
    }
    public void LoadRedAsync<T>(string abname, string resname, UnityAction<T> callback, System.Type type)where T:Object//�첽������Դ
    {
        StartCoroutine(ReallyLoadRedAsync<T>(abname, resname, callback, type));
    }
    private IEnumerator ReallyLoadRedAsync<T>(string abname, string resname, UnityAction<T> callback, System.Type type)where T:Object//�첽������Դ
    {
        LoadABDepend(abname);
        AssetBundleRequest abr = abDic[abname].LoadAssetAsync<T>(resname);
        yield return abr;
        callback(abr.asset as T);//�ص�
    }
    public void UnLoad(string abname)//ж����Դ
    {
        if (abDic.ContainsKey(abname))
        {
            abDic[abname].Unload(false);
            abDic.Remove(abname);
        }
    }
    public void ClearAB()//���������Դ
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        MainAB = null;
        manifest = null;
    } 
}
