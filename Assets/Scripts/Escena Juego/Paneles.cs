using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paneles : MonoBehaviour {

	public GameObject[] cubos;

	public void Reiniciar()
	{
		for (int i = 0; i <= 14; i++)
		{
			cubos[i].SetActive(true);
		}
	}

	public void Imprimir(int x)
	{
		switch (x)
		{
			case 0:
				cubos[4].SetActive(false);
				cubos[7].SetActive(false);
				cubos[10].SetActive(false);
				break;
			case 1:
				cubos[2].SetActive(false);
				cubos[3].SetActive(false);
				cubos[5].SetActive(false);
				cubos[6].SetActive(false);
				cubos[8].SetActive(false);
				cubos[9].SetActive(false);
				cubos[11].SetActive(false);
				break;
			case 2:
				cubos[3].SetActive(false);
				cubos[4].SetActive(false);
				cubos[10].SetActive(false);
				cubos[11].SetActive(false);
				break;
			case 3:
				cubos[3].SetActive(false);
				cubos[4].SetActive(false);
				cubos[6].SetActive(false);
				cubos[9].SetActive(false);
				cubos[10].SetActive(false);
				break;
			case 4:
				cubos[1].SetActive(false);
				cubos[4].SetActive(false);
				cubos[9].SetActive(false);
				cubos[10].SetActive(false);
				cubos[12].SetActive(false);
				cubos[13].SetActive(false);
				break;
			case 5:
				cubos[4].SetActive(false);
				cubos[5].SetActive(false);
				cubos[9].SetActive(false);
				cubos[10].SetActive(false);
				break;
			case 6:
				cubos[4].SetActive(false);
				cubos[5].SetActive(false);
				cubos[10].SetActive(false);
				break;
			case 7:
				cubos[3].SetActive(false);
				cubos[4].SetActive(false);
				cubos[6].SetActive(false);
				cubos[9].SetActive(false);
				cubos[10].SetActive(false);
				cubos[12].SetActive(false);
				cubos[13].SetActive(false);
				break;
			case 8:
				cubos[4].SetActive(false);
				cubos[10].SetActive(false);
				break;
			case 9:
				cubos[4].SetActive(false);
				cubos[9].SetActive(false);
				cubos[10].SetActive(false);
				cubos[12].SetActive(false);
				cubos[13].SetActive(false);
				break;

		}
	}
}
