using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 用于处理文件和Asset
/// 
/// path 指无法区分的路径
/// 
/// relativePath 指 相对于Assets/下的路径
/// assetPath 指加上"Assets/"的路径
/// fullPath 指 完整路径
/// 
/// </summary>
public class FileTools : Editor {

    /// <summary>
    /// 获取指定路径下指定类型的Assets
    /// </summary>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public static UnityEngine.Object[] GetAssetsAtPath(string assetPath)
    {
        return GetAssetsAtPath<UnityEngine.Object>(assetPath);
    }

    public static T[] GetAssetsAtPath<T>(string assetPath) where T : UnityEngine.Object
    {
        string fullPath = Asset2Full(assetPath);
        if (!Directory.Exists(fullPath))
        {
            return null;
        }

        List<T> al = new List<T>();
        string[] fileEntries = Directory.GetFiles(fullPath);

        foreach (string fileName in fileEntries)
        {
            string fileAssetPath = Full2Asset(fileName);
            T t = AssetDatabase.LoadAssetAtPath<T>(fileAssetPath);

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];

        return result;
    }


#region 路径转换
    public static string Full2Asset(string fullPath)
    {
        int assetPathIndex = fullPath.IndexOf("Assets");
        string assetPath = fullPath.Substring(assetPathIndex);
        return assetPath;
    }
    public static string Full2Relative(string fullPath)
    {
        return fullPath.Replace(Application.dataPath + "/", "");
    }

    public static string Asset2Full(string assetPath)
    {
        return Application.dataPath + "/" + Asset2Relative(assetPath);
    }

    public static string Asset2Relative(string assetPath)
    {
        return assetPath.Replace("Assets/", "");
    }

    public static string Relative2Full(string relativePath)
    {
        return Application.dataPath + "/" + relativePath;
    }

    public static string Relative2Asset(string relativePath)
    {
        return "Assets/" + relativePath;
    }
#endregion


    #region Error Tips
    #endregion
}
