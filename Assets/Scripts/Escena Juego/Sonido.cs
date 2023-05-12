using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Sonido {

	public string nombre;

	public AudioClip Clip;

	[Range(0f,1.0f)]
	public float volume;
	[Range(.1f,3.0f)]
	public float pitch;
	public float m, b;

	[HideInInspector]
	public AudioSource Source;
}
