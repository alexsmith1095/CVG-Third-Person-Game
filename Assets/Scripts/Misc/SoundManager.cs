using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource effectSource;
	public static SoundManager Main = null;

	// Create a singleton instance and prevent it from being destroyed on scene loads
	void Awake ()
	{
		if (Main == null) {
			Main = this;
		} else if (Main != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}

	/// <summary>
	///	Plays sound effect clip once
	/// </summary>
	public void Play (AudioClip clip)
	{
		effectSource.clip = clip;
		effectSource.Play();
	}
}
