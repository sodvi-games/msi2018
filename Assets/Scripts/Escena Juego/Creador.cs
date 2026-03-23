using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creador : MonoBehaviour
{
	public float TieLleg = 1;
	private int CanE;
	public static int CanET;
	public GameObject Enemigo;

	private GameObject EnemigoC;
	private float dx, dy;
	private ConEspectro espectro; // Caché del componente

	void Start()
	{
		// Guardamos la referencia para no buscarla cada frame
		espectro = GetComponent<ConEspectro>();
	}

	void Update()
	{
		// REGLA DE ORO: Si está en pausa o no estamos en juego, no crear nada
		if (MenuPausa.EnPausa || !MenuPausa.EnJuego) return;

		// Verificamos la explosión del espectro (pico de audio)
		if (espectro.exp && espectro.Luz.range >= 13.0f)
		{
			if (CanET < 50)
			{
				GenerarOleada();
			}
		}
	}

	void GenerarOleada()
	{
		CanE = Random.Range(1, 4);
		CanET += CanE;

		Transform PEsfe = espectro.Esf.transform;

		for (int i = 0; i < CanE; i++)
		{
			// Instanciamos en la posición de la esfera central
			EnemigoC = Instantiate(Enemigo, PEsfe.position, Quaternion.identity);

			// Intentamos obtener el script del enemigo de forma segura
			if (EnemigoC.TryGetComponent<Enemigo>(out var scriptEnemigo))
			{
				// Definimos destino aleatorio
				scriptEnemigo.x = Random.Range(-14.0f, 14.0f);
				scriptEnemigo.y = Random.Range(0.9f, 5.5f);

				// Calculamos distancia y velocidad para llegar en 'TieLleg' segundos
				dx = scriptEnemigo.x - PEsfe.position.x;
				dy = scriptEnemigo.y - PEsfe.position.y;

				scriptEnemigo.velx = dx / TieLleg;
				scriptEnemigo.vely = dy / TieLleg;
			}
		}

		// Evitamos que genere múltiples veces en el mismo pico de audio
		// bajando la bandera de explosión temporalmente si es necesario
		espectro.exp = false;
	}
}