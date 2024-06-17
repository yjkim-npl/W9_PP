using UnityEngine;

public class Util 
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if(component is null)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform is null)
            return null;
        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false)
        where T : UnityEngine.Object
    {
        if (go is null) return null;
        if(!recursive)
        {
            for(int i=0; i<go.transform.childCount; i++)
            {
                Transform tr = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T comp = tr.GetComponent<T>();
                    if(comp is not null)
                        return comp;
                }
            }
        }
        else
        {
            foreach( T comp in go.GetComponentsInChildren<T>() )
            {
                if(string.IsNullOrEmpty(name) || comp.name == name)
                    return comp;
            }
        }
        return null;
    }
}
