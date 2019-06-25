using UnityEngine.Audio;
using System;
using UnityEngine;

public class LS_AudioManager : MonoBehaviour {

    public AudioMixer audioMixer;

    public LS_Sound[] sounds;

	public static LS_AudioManager instance;
		
	void Awake () {

			DontDestroyOnLoad(gameObject);

			if (instance == null)
				instance = this;
			else												
			{
				Destroy(gameObject);
				return;
			}

			foreach(LS_Sound s in sounds)
			{
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;

				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
			}
	}

    public void Volume(float v)
    {
        LS_Sound sVolume = Array.Find(sounds, sounds => sounds.volume == v);
        foreach(LS_Sound sV in sounds)
        {
            sV.source.volume = v;
        }
    }

    public void Play (string name)
	{
		LS_Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}

    public void Stop(string name)
    {
        LS_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }


}
