using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance
    {
        get
        {
            if(instance == null)
                Init();
            return instance;
        }
    }

    DataManager data = new DataManager();
    UIManager ui = new UIManager();
    SoundManager sound = new SoundManager();
    GameManager game;
    public static DataManager Data { get { return Instance?.data; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static SoundManager Sound { get {  return Instance?.sound; } }
    public static GameManager Game 
    { 
        get 
        {
            return Instance?.game; 
        } 
    }

    private void Awake()
    {
        game = Util.GetOrAddComponent<GameManager>(gameObject);
        game.Init();
    }
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