
using UnityEngine.Audio;
using System;
using UnityEngine;

public class Music : MonoBehaviour
{

    public Sound[] sounds;


    // Start is called before the first frame update
    void Awake() {

        
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            if (s.name == "song") s.source.clip = LoadMaps.musicClip;
            else s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        
    }


    // Update is called once per frame
    public AudioSource Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound \"" + name + "\"does not exist.");
            return null;
        }
        s.source.Play();
        return s.source;
    }

}
