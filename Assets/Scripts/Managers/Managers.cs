using System.Data;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    DataManager data = new DataManager();
    UIManager ui = new UIManager();
    SoundManager sound = new SoundManager();
    public static DataManager Data { get { return Instance?.data; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static SoundManager Sound { get {  return Instance?.sound; } }
    static void Init()
    {
        if(instance is null)
        {
            GameObject go = GameObject.Find("@Managers");
            if( go is null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.AddComponent<Managers>();
            Sound.Init();
        }
    }
}