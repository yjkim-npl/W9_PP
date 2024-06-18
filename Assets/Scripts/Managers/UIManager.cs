using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    private Dictionary<string, UIBase> UIdic = new Dictionary<string, UIBase>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UIRoot");
            if (root == null)
            {
                root = new GameObject { name = "@UIRoot" };
            }
            return root;
        }
    }

    public void SetDic<T>(T value) where T : UIBase
    {
        UIdic.Add(typeof(T).Name, value);
    }
    public T GetDic<T>(string key) where T : UIBase
    {
        return UIdic[key] as T;
    }
}
