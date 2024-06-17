using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private GameObject root = null;
    public void Init()
    {
        if (root == null)
            root = GameObject.Find("@SoundRoot");
        if (root == null)
        {
            root = new GameObject { name = "@SoundRoot" };
            UnityEngine.Object.DontDestroyOnLoad(root);
            string[] soundTypeNames = Enum.GetNames(typeof(Define.Sound));
            for (int cnt = 0; cnt < soundTypeNames.Length; cnt++)
            {
                GameObject go = new GameObject { name = soundTypeNames[cnt] };
                audioSources[cnt] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            audioSources[(int)Define.Sound.BGM].loop = true;
        }
    }
}