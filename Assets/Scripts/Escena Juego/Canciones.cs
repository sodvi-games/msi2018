using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


	void Awake () {

		Source = GetComponent<AudioSource>();

        if (!MenuPausa.EnJuego) ObtenerLista();

        CrearUrl();

		StartCoroutine(CargarCan());

	}

	// Update is called once per frame
	IEnumerator CargarCan()
	{
		WWW www = new WWW(url);
		if (www.error != null)
		{
			Debug.Log(www.error);
		}
		else
		{
			ClipsC[0] = www.GetAudioClipCompressed();
			while (ClipsC[0].loadState != AudioDataLoadState.Loaded)
				yield return new WaitForSeconds(0.1f);
			Source.clip = ClipsC[0];
			Source.Play();
			GetComponent<ConEspectro>().ConfCancion();
		}
	}

    void ObtenerLista()
    {
        string[] files;
        files = Directory.GetFiles(FileDirectory);

        Files.Clear();

        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".wav"))
            {
                Files.Add(files[i]);
            }
        }

        NomClips = Files.ToArray();

        CanSelec = Random.Range(0, NomClips.Length);
    }

    void CrearUrl()
    {
        url = Application.dataPath;

        if (url.EndsWith("Assets")) url = url.Substring(0, url.Length - 6);
        else url = url.Substring(0, url.Length - 10);

        url = url.Replace((char)92, '/');

        url = url + NomClips[CanSelec];
        url = "File:///" + url;
    }

}
