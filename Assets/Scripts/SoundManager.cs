using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    public AudioSource MainSource;
    
    public AudioClip loopedClip;
    

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MainSource.clip = loopedClip;
        MainSource.loop = true;
        MainSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
