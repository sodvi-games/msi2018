using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creador : MonoBehaviour {

	public float TieLleg = 1;
	private int CanE;
    public static int CanET;
	public GameObject Enemigo;
	private GameObject EnemigoC;
	private float dx, dy;
	private float dis;

	private Transform PEsfe;

	// Use this for initialization
	void Start () {

		PEsfe = GetComponent<ConEspectro>().Esf.transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (!MenuPausa.EnPausa)
		{
			if (GetComponent<ConEspectro>().exp && GetComponent<ConEspectro>().Luz.range == 13.0f && !MenuPausa.EnPausa)
			{
                if (CanET < 50)
                {
                    CanE = Random.Range(1, 4);
                    CanET += CanE;

                    PEsfe = GetComponent<ConEspectro>().Esf.transform;
                    for (int i = 0; i < CanE; i++)
                    {
                        EnemigoC = Instantiate(Enemigo, PEsfe.transform.position, new Quaternion(0, 0, 0, 0));
                        EnemigoC.GetComponent<Enemigo>().x = Random.Range(-14.0f, 14.0f);
                        EnemigoC.GetComponent<Enemigo>().y = Random.Range(0.9f, 5.5f);
                        dx = (EnemigoC.GetComponent<Enemigo>().x - PEsfe.transform.position.x);
                        dy = (EnemigoC.GetComponent<Enemigo>().y - PEsfe.transform.position.y);
                        EnemigoC.GetComponent<Enemigo>().velx = dx / TieLleg;
                        EnemigoC.GetComponent<Enemigo>().vely = dy / TieLleg;
                    }
                }
			}
		}
	}
}
