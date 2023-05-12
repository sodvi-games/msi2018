using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAudio : MonoBehaviour {

	public Sonido[] Sonidos;

	void Awake() {
		foreach(Sonido s in Sonidos) {
			s.Source = gameObject.AddComponent<AudioSource>();
			s.Source.clip = s.Clip;

			s.Source.volume = s.volume;
			s.Source.pitch = s.pitch;
		}

	}
	
	public void Play(string name) {
		Sonido s = System.Array.Find(Sonidos, Sonido => Sonido.nombre == name);
		s.Source.Play();
	}
	public void CambiarPitch(string name, float x) {
		Sonido s = System.Array.Find(Sonidos, Sonido => Sonido.nombre == name);
		s.Source.pitch = s.m * x + s.b;
	}
}
