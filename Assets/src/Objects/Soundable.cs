using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Soundable{
    AudioSource audioSource{get;set;}
    void play(AudioClip clip, float volume);
}