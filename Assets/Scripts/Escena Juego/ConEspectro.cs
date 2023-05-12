using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConEspectro : MonoBehaviour {

    private AudioSource Cancion;

    public GameObject Esf;
    private GameObject[] cubos;

    public static float[] samples = new float[64];
    public int sen;
    public float aumsenC;
    public float senC;
    private float r, g, b;
    public static float ra, ga, ba;
    private float res, ges, bes;
    private float resa, gesa, besa;
    private MeshRenderer Mat;
    public Light Luz;

    private float TiempoCC;

    private float total;
    private float TotalT;
    private float tam;
    public bool exp;

    [Range(0, 0.03f)]
    public float limite = 0.02f;

    private float vol;

	private void PintaCubos()
	{
        for (int i = 0; i < cubos.Length; i++)
		{
			cubos[i].GetComponent<MeshRenderer>().material.SetColor("_MKGlowTexColor", new Color(ra, ga, ba));
		}
	}

	public void ConfCancion()
	{
		float[] samplesT = new float[Cancion.clip.samples * Cancion.clip.channels];
		Cancion.clip.GetData(samplesT, 0);
		for (int i = 0; i < samplesT.Length; i++)
		{
			TotalT += Mathf.Abs(samplesT[i]);
		}
		TotalT /= samplesT.Length;
		limite = 0.07567060557005f * TotalT + 0.0061403438825f;
	}

	// Use this for initialization
	void Start () {
		Cancion = GetComponent<AudioSource>();
		Mat = Esf.GetComponent<MeshRenderer>();
		exp = false;
		cubos = GameObject.FindGameObjectsWithTag("Pared");
        MezclarCubos(ref cubos);
		r = Random.Range(0.0f, 1.0f);
		g = Random.Range(0.0f, 1.0f);
		b = Random.Range(0.0f, 1.0f);
		ra = r;
		ga = g;
		ba = b;
		PintaCubos();
		res = Random.Range(0.0f, 1.0f);
		ges = Random.Range(0.0f, 1.0f);
		bes = Random.Range(0.0f, 1.0f);
		resa = res;
		gesa = ges;
		besa = bes;
		Mat.material.SetColor("_MKGlowTexColor", new Color(res, ges, bes));
		Luz.color = new Color(res, ges, bes);
		res = Random.Range(0.0f, 1.0f);
		ges = Random.Range(0.0f, 1.0f);
		bes = Random.Range(0.0f, 1.0f);
		r = Random.Range(0.0f, 1.0f);
		g = Random.Range(0.0f, 1.0f);
		b = Random.Range(0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!MenuPausa.EnPausa)
		{
			if (TiempoCC >= 0.02f)
			{
				VariarColor(ref ra, ref r);
				VariarColor(ref ga, ref g);
				VariarColor(ref ba, ref b);
				VariarColor(ref resa, ref res);
				VariarColor(ref gesa, ref ges);
				VariarColor(ref besa, ref bes);
				Mat.material.SetColor("_MKGlowTexColor", new Color(resa, gesa, besa));
				Mat.material.SetColor("_MKGlowColor", new Color(resa, gesa, besa));
				Luz.color = new Color(resa, gesa, besa);

				PintaCubos();

				TiempoCC -= 0.02f;
			}
			else TiempoCC += Time.deltaTime;


			Cancion.GetSpectrumData(samples, 0, FFTWindow.Triangle);
			for (int i = 0; i < 64; i++)
			{
				total += samples[i];
				if (i < cubos.Length)
				{
					if (samples[i] > 0.166f) samples[i] = 0.055f;
					cubos[i].transform.localScale = new Vector3(1, 1, (senC * samples[i]) + 1.0f);
					senC += aumsenC;
				}
			}
			senC = 8;

			if (!exp)
			{

				total /= 64.0f;

				if (total > limite && MenuPausa.EnJuego)
				{
					tam = (sen * 3 * total) + 1.3f;
					exp = true;
					Luz.range = 13.0f;
				}
				else tam = (sen * total) + 1.3f;
				Esf.transform.localScale = new Vector3(tam, tam, tam);
			}
			else
			{
				if (Esf.transform.localScale.x > 1)
				{
					tam -= 8.0f * Time.deltaTime;
					if (Luz.range > 7) Luz.range -= 20.0f * Time.deltaTime;
					else Luz.range = 7;
					Esf.transform.localScale = new Vector3(tam, tam, tam);
				}
				else
				{
					exp = false;
					Luz.range = 7;
				}
			}
			total = 0;
		}
	}

    private void MezclarCubos(ref GameObject[] g)
    {
        GameObject[] lista1 = new GameObject[60];
        GameObject[] lista2 = new GameObject[60];
        int h1 = 0,h2 = 0,pos = 0;

        for (int i = 0; i < g.Length; i++)
        {
            if (Random.Range(0, 2) == 0) {
                lista1[h1] = g[i];
                h1++;
            }
            else
            {
                lista2[h2] = g[i];
                h2++;
            }
        }

        for (int i = 0; i < h1; i++)
        {
            g[pos] = lista1[i];
            pos++;
        }

        for (int i = 0; i < h2; i++)
        {
            g[pos] = lista2[i];
            pos++;
        }

    }

	public static void VariarColor(ref float ca, ref float c)
	{
		if (c > ca)
		{
			ca += 0.005f;
			if (c < ca) c = Random.Range(0.0f, 1.0f);
		}
		else
		{
			ca -= 0.005f;
			if (c > ca) c = Random.Range(0.0f, 1.0f);
		}
	}

}
