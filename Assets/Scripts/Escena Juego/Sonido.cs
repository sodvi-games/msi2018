using UnityEngine;
// UnityEngine.Audio solo es necesario si usas AudioMixerGroups. 
// Si no los usas, puedes borrar esa línea.

[System.Serializable]
public class Sonido
{
	public string nombre;
	public AudioClip Clip;

	[Range(0f, 1.0f)]
	public float volume = 1.0f; // Valor por defecto
	[Range(0.1f, 3.0f)]
	public float pitch = 1.0f;  // Valor por defecto

	public float m = 1.0f;
	public float b = 0.0f;

	// Inicializar en null quita el warning de "nunca asignado"
	[HideInInspector]
	public AudioSource Source = null;
}