using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Necesario para Array.Find

// Añadimos el componente AudioSource como requerido (opcional, ya que los creas por código)
[RequireComponent(typeof(AudioSource))]
public class ScriptAudio : MonoBehaviour
{
	public Sonido[] Sonidos;

	void Awake()
	{
		foreach (Sonido s in Sonidos)
		{
			// En Unity 6, añadir componentes en tiempo de ejecución sigue igual
			s.Source = gameObject.AddComponent<AudioSource>();
			s.Source.clip = s.Clip;

			s.Source.volume = s.volume;
			s.Source.pitch = s.pitch;

			// Importante: por defecto los AudioSource nuevos vienen con PlayOnAwake activo
			s.Source.playOnAwake = false;
		}
	}

	public void Play(string name)
	{
		// Usamos Array.Find para buscar por el string 'nombre' de tu clase
		Sonido s = Array.Find(Sonidos, sonido => sonido.nombre == name);

		if (s != null && s.Source != null)
		{
			s.Source.Play();
		}
		else
		{
			Debug.LogWarning($"[ScriptAudio] El sonido '{name}' no existe o no tiene AudioSource.");
		}
	}

	public void CambiarPitch(string name, float x)
	{
		Sonido s = Array.Find(Sonidos, sonido => sonido.nombre == name);

		if (s != null && s.Source != null)
		{
			// Tu fórmula: y = mx + b
			s.Source.pitch = s.m * x + s.b;
		}
	}

	// Método extra útil para detener sonidos
	public void Stop(string name)
	{
		Sonido s = Array.Find(Sonidos, sonido => sonido.nombre == name);
		if (s != null && s.Source.isPlaying)
		{
			s.Source.Stop();
		}
	}
}