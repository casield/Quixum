using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;
using UnityEngine.EventSystems;

public class GolfBall:MonoBehaviour,Soundable
{
    public AudioSource audioSource { get;set;}
    public ObjectState state;

    private AudioClip collitionSound;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Client.Instance.addReadyListener(init);
        collitionSound = Resources.Load<AudioClip>("Sounds/BallCollition");
        audioSource =gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.88f;
       // audioSource.clip = collitionSound;
    }

    public void play(AudioClip clip, float volume)
    {
        
        audioSource.PlayOneShot(clip,volume);
    }
    public void setState(ObjectState ob){
        this.state = ob;
    }

    // Start is called before the first frame update

    void init(){
       // state.sound.OnChange+=onSoundChange;
    }

    private void onSoundChange(List<DataChange> changes)
    {
        
        foreach (var item in changes)
        {
           // Debug.Log("Change: "+item.Field + " / "+item.Value + " / "+ state.sound.volume);
            var ob = item.Field;
           // play(collitionSound,state.sound.volume);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
