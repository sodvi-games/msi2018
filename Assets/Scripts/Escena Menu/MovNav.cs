using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovNav : MonoBehaviour
{
    [Header("Configuración de Órbita")]
    public float radio = 10f;
    public float velocidadGiro = 0.5f;

    private float anguloActual;
    private float alturaOriginal; // Guardará la Y que pusiste en el editor

    void Start()
    {
        // 1. Guardamos la altura Y actual para que no se mueva de ahí
        alturaOriginal = transform.position.y;

        // 2. Calculamos el ángulo inicial basado en su posición en la escena
        // para que la órbita empiece exactamente donde dejaste el objeto
        anguloActual = Mathf.Atan2(transform.position.x, transform.position.z);
    }

    void Update()
    {
        if (MenuPausa.EnPausa) return;

        // 3. Incrementar el ángulo según el tiempo
        anguloActual += Time.deltaTime * velocidadGiro;

        // 4. Calcular nueva posición (X y Z cambian, Y se queda igual)
        float x = Mathf.Sin(anguloActual) * radio;
        float z = Mathf.Cos(anguloActual) * radio;

        transform.position = new Vector3(x, alturaOriginal, z);

        // 5. Lógica de Rotación "Nivelada" (Mirar al centro sin inclinarse)
        Vector3 direccionAlCentro = Vector3.zero - transform.position;

        // IMPORTANTE: Forzamos que la diferencia de altura sea 0 para el cálculo de rotación
        // Esto evita que el objeto "mire hacia abajo" si está muy alto
        direccionAlCentro.y = 0;

        if (direccionAlCentro != Vector3.zero)
        {
            // Creamos la rotación mirando al centro pero solo en el eje horizontal
            Quaternion lookRotation = Quaternion.LookRotation(direccionAlCentro);
            transform.rotation = lookRotation;
        }
    }
}