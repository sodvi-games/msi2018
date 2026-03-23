using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paneles : MonoBehaviour
{
	// Arreglo de cubos que forman el dígito (matriz de 3x5 generalmente)
	public GameObject[] cubos;

	/// <summary>
	/// Enciende todos los cubos para prepararlos para un nuevo número.
	/// </summary>
	public void Reiniciar()
	{
		// Usamos cubos.Length en lugar de 14 para evitar errores de índice
		for (int i = 0; i < cubos.Length; i++)
		{
			if (cubos[i] != null)
				cubos[i].SetActive(true);
		}
	}

	/// <summary>
	/// Desactiva cubos específicos para formar el dígito X.
	/// </summary>
	public void Imprimir(int x)
	{
		// Verificación de seguridad
		if (cubos == null || cubos.Length < 15) return;

		switch (x)
		{
			case 0:
				SetStates(false, 4, 7, 10);
				break;
			case 1:
				SetStates(false, 2, 3, 5, 6, 8, 9, 11);
				break;
			case 2:
				SetStates(false, 3, 4, 10, 11);
				break;
			case 3:
				SetStates(false, 3, 4, 6, 9, 10);
				break;
			case 4:
				SetStates(false, 1, 4, 9, 10, 12, 13);
				break;
			case 5:
				SetStates(false, 4, 5, 9, 10);
				break;
			case 6:
				SetStates(false, 4, 5, 10);
				break;
			case 7:
				SetStates(false, 3, 4, 6, 9, 10, 12, 13);
				break;
			case 8:
				SetStates(false, 4, 10);
				break;
			case 9:
				SetStates(false, 4, 9, 10, 12, 13);
				break;
		}
	}

	/// <summary>
	/// Función auxiliar para apagar múltiples índices de una sola vez.
	/// </summary>
	private void SetStates(bool state, params int[] indices)
	{
		foreach (int index in indices)
		{
			if (index < cubos.Length && cubos[index] != null)
			{
				cubos[index].SetActive(state);
			}
		}
	}
}