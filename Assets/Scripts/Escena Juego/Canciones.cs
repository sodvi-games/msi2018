using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Necesario para UnityWebRequest
using System.IO;

public class Canciones : MonoBehaviour
{
	public string FileDirectory;

	public static AudioSource Source;
	public static List<string> Files = new List<string>();
	public static string[] NomClips = new string[20];
	public static AudioClip[] ClipsC = new AudioClip[20];

	public static string url;
	public static int CanSelec;

	void Awake()
	{
		Source = GetComponent<AudioSource>();

		// Corregido: Asegurarnos de que el directorio no esté vacío antes de operar
		if (!MenuPausa.EnJuego) ObtenerLista();

		if (NomClips.Length > 0)
		{
			CrearUrl();
			StartCoroutine(CargarCan());
		}
		else
		{
			Debug.LogWarning("No se encontraron archivos .wav en el directorio especificado.");
		}
	}

	IEnumerator CargarCan()
	{
		// UnityWebRequest es el reemplazo moderno de WWW
		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
		{
			yield return www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError("Error al cargar audio: " + www.error + " en la URL: " + url);
			}
			else
			{
				// Obtenemos el clip directamente
				ClipsC[0] = DownloadHandlerAudioClip.GetContent(www);

				// Esperar a que el clip esté listo (aunque GetContent suele manejarlo)
				while (ClipsC[0].loadState == AudioDataLoadState.Loading)
					yield return null;

				if (ClipsC[0].loadState == AudioDataLoadState.Loaded)
				{
					Source.clip = ClipsC[0];
					Source.Play();

					if (TryGetComponent<ConEspectro>(out var espectro))
					{
						espectro.ConfCancion();
					}
				}
			}
		}
	}

	void ObtenerLista()
	{
		// Combinar rutas de forma segura
		string fullPath = Path.Combine(Application.dataPath, "..", FileDirectory);

		if (!Directory.Exists(fullPath))
		{
			Debug.LogError("La carpeta no existe: " + fullPath);
			return;
		}

		string[] files = Directory.GetFiles(fullPath, "*.wav");

		Files.Clear();
		foreach (string file in files)
		{
			// Guardamos solo el nombre del archivo o la ruta relativa necesaria
			Files.Add(file);
		}

		NomClips = Files.ToArray();

		if (NomClips.Length > 0)
			CanSelec = Random.Range(0, NomClips.Length);
	}

	void CrearUrl()
	{
		// En Unity 6, para archivos locales, es más seguro usar el formato absoluto de ruta
		string path = NomClips[CanSelec];

		// Reemplazar diagonales inversas por normales para URL
		path = path.Replace("\\", "/");

		// UnityWebRequest prefiere el prefijo file://
		if (!path.StartsWith("file://"))
		{
			url = "file://" + path;
		}
		else
		{
			url = path;
		}
	}
}