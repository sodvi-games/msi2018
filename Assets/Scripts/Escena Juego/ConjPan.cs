using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjPan : MonoBehaviour
{
	public List<GameObject> PanelH = new List<GameObject>();
	public GameObject[] PanelM = new GameObject[20];

	private int CanPan = 2;
	public int BasDiez = 1000;
	private int mult = 1;

	// Caché de componentes para evitar buscar cada vez
	private Paneles[] scriptsPaneles;

	void Awake()
	{
		PanelH.Clear();
		foreach (Transform child in transform)
		{
			PanelH.Add(child.gameObject);
		}

		PanelM = PanelH.ToArray();
		CanPan = PanelM.Length - 1;

		// Guardamos los scripts de los paneles de una vez por todas
		scriptsPaneles = new Paneles[PanelM.Length];
		for (int i = 0; i < PanelM.Length; i++)
		{
			scriptsPaneles[i] = PanelM[i].GetComponent<Paneles>();
		}
	}

	public void DetNum(int NP)
	{
		// Si el número crece más allá de lo esperado, habilitamos un nuevo dígito
		if (NP >= BasDiez && (CanPan + 1) < PanelM.Length)
		{
			BasDiez *= 10;
			// Si el panel estaba oculto, lo activamos
			// Nota: Aquí se asume que los paneles están ordenados por jerarquía
		}

		int NPT = NP;
		mult = 1;

		for (int i = 0; i <= CanPan; i++)
		{
			if (scriptsPaneles[i] == null) continue;

			// Reiniciamos el panel (apaga todos los cubos)
			scriptsPaneles[i].Reiniciar();

			// Matemática para extraer el dígito específico
			int digito = ((NPT % (mult * 10)) - (NPT % mult)) / mult;

			// Le decimos al panel que imprima ese dígito
			scriptsPaneles[i].Imprimir(digito);

			mult *= 10;
		}
	}

	public void PonerColor(Color C)
	{
		// Ya no usamos GetComponent dentro de los for, usamos el array scriptsPaneles
		for (int i = 0; i < scriptsPaneles.Length; i++)
		{
			if (scriptsPaneles[i] == null) continue;

			// Accedemos directamente a los cubos del script Paneles
			for (int p = 0; p < scriptsPaneles[i].cubos.Length; p++)
			{
				if (scriptsPaneles[i].cubos[p] != null)
				{
					scriptsPaneles[i].cubos[p].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", C);
				}
			}
		}
	}
}