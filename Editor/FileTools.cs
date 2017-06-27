using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 用于处理文件和Asset
/// 
/// path 指 相对于Assets/下的路径
/// assetpath 指 完整路径
/// 
/// </summary>
public class FileTools : Editor {

    /// <summary>
    /// 获取指定路径下指定类型的Assets
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static UnityEngine.Object[] GetAssetsAtPath(string path)
    {
        return GetAssetsAtPath<UnityEngine.Object>(path);
    }

    public static T[] GetAssetsAtPath<T>(string path) where T : UnityEngine.Object
    {
        List<T> al = new List<T>();
        string[] fileEntries = Directory.GetFiles(GetAssetPath(path));

        foreach (string fileName in fileEntries)
        {
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);

            T t = AssetDatabase.LoadAssetAtPath<T>(localPath);

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];

        return result;
    }

    /// <summary>
    /// 获取完整路径
    /// </summary>
    /// <returns></returns>
    public static string GetAssetPath(string path)
    {
        return Application.dataPath + "/" + path;
    }
}
