using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConjPan))]
public class MarcadorGlobal : MonoBehaviour
{
	// Variable estática que guarda los puntos en toda la sesión
	public static int Marcador = 0;

	private ConjPan gestorPaneles;

	void Awake()
	{
		// Cacheamos el componente para que el acceso sea instantáneo
		gestorPaneles = GetComponent<ConjPan>();
	}

	void Start()
	{
		// Al iniciar la escena, aseguramos que el marcador visual empiece en 0
		// y con su color por defecto (Blanco)
		if (gestorPaneles != null)
		{
			gestorPaneles.DetNum(0);
			gestorPaneles.PonerColor(Color.white);
		}
	}

	/// <summary>
	/// Método estático útil para añadir puntos desde otros scripts (como Enemigo)
	/// </summary>
	public static void SumarPuntos(int cantidad)
	{
		Marcador += cantidad;

		// Buscamos el objeto del marcador en la escena para actualizarlo visualmente
		// Nota: FindFirstObjectByType es el estándar de Unity 6
		var visual = Object.FindFirstObjectByType<MarcadorGlobal>();
		if (visual != null && visual.gestorPaneles != null)
		{
			visual.gestorPaneles.DetNum(Marcador);
		}
	}
}