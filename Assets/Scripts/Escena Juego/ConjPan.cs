using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjPan : MonoBehaviour {

	public List<GameObject> PanelH = new List<GameObject>();
	public GameObject[] PanelM = new GameObject[20];
	private int CanPan = 2;
	private int PanAct = 2;
	public int BasDiez = 1000;
	public int mult = 1;
	public int NPT;


	// Use this for initialization
	void Awake () {
		foreach (Transform child in this.gameObject.transform)
		{
			PanelH.Add(child.gameObject);
		}
		PanelM = PanelH.ToArray();
		CanPan = PanelM.Length - 1;
	}
	
	public void DetNum(int NP)
	{
		if (NP >= BasDiez)
		{
			BasDiez *= 10;
			PanAct++;
			PanelM[PanAct].SetActive(true);
		}

		NPT = NP;

		for (int i = 0; i <= CanPan; i++)
		{
			PanelM[i].GetComponent<Paneles>().Reiniciar();
			NP = ((NPT % (mult * 10)) - (NPT % mult)) / mult;
			PanelM[i].GetComponent<Paneles>().Imprimir(NP);
			mult *= 10;
		}

		mult = 1;
	}

	public void PonerColor(Color C)
	{
		for (int i = 0; i < PanelM.Length; i++)
		{
			for (int p = 0; p < 15; p++)
			{
				GetComponent<ConjPan>().PanelM[i].GetComponent<Paneles>().cubos[p].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", C);
			}
		}
	}

}
