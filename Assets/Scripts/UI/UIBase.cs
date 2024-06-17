using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> objects= new Dictionary<Type, UnityEngine.Object[]>();
    public abstract void Init();
    private void Start()
    {
        Init();
    }

    protected void Bind<T> (Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        objects.Add(typeof(T), objs);
        for(int i=0; i<names.Length; i++)
        {
            if(typeof(T) == typeof(GameObject))
                objs[i] = Util.FindChild(gameObject, names[i],true);
            else
                objs[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objs[i] == null)
                Debug.Log($"Failed to bind({names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;
        if (objects.TryGetValue(typeof(T), out objs) == false)
            return null;
        return objs[idx] as T;
    }
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(go);
        evt.actions[(int)type] -= action;
        evt.actions[(int)type] += action;
    }

}