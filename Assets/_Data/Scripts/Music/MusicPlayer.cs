using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : NhoxBehaviour
{
    public AudioSource introSource, loopSource;
    
    protected override void Start()
    {
        base.Start();
        introSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadIntroSource();
    }

    protected virtual void LoadIntroSource()
    {
        if (introSource != null) return;
        introSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + " LoadIntroSource", gameObject);
    }
}
