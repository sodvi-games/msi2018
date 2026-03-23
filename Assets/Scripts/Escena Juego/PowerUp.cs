using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject[] Power;
    private Transform[] cubos;
    private int e, golpes;
    private float vx, vy;
    private float ang;

    // Límites de pantalla consistentes con tu juego
    private const float LIMITE_X = 14f;
    private const float LIMITE_Y_SUP = 6f;
    private const float LIMITE_Y_INF = -7f;

    void Start()
    {
        SacarVel(0.0f, 360.0f);

        if (Power.Length == 0) return;

        e = Random.Range(0, Power.Length);
        GameObject creado = Instantiate(Power[e], transform.position, Quaternion.identity);
        creado.transform.SetParent(transform);

        // Obtenemos los hijos para cambiarles el color de MK Glow
        cubos = creado.GetComponentsInChildren<Transform>();

        switch (e)
        {
            case 0: // Caso para el PowerUp de Vida (Verde)
                SetCuboColor(Color.green);

                // FIX: En lugar de añadir dos Colliders, añadimos uno y ajustamos
                // O mejor, asegúrate de que el objeto base tenga un Rigidbody2D en modo Kinematic
                var col1 = gameObject.AddComponent<BoxCollider2D>();
                col1.size = new Vector2(0.6f, 0.6f); // Unificado para cubrir el área
                col1.isTrigger = false;
                break;
        }
    }

    void Update()
    {
        if (MenuPausa.EnPausa) return;

        ManejarRebotesYLimites();

        // Movimiento simple por velocidad
        transform.Translate(Time.deltaTime * vx, Time.deltaTime * vy, 0);
    }

    private void ManejarRebotesYLimites()
    {
        // Rebote Derecha
        if (transform.position.x > LIMITE_X)
        {
            transform.position = new Vector3(LIMITE_X, transform.position.y, -2.31f);
            SacarVel(120.0f, 240.0f);
        }
        // Rebote Izquierda
        else if (transform.position.x < -LIMITE_X)
        {
            transform.position = new Vector3(-LIMITE_X, transform.position.y, -2.31f);
            SacarVel(-60f, 60f);
        }

        // Rebote Superior
        if (transform.position.y > LIMITE_Y_SUP)
        {
            transform.position = new Vector3(transform.position.x, LIMITE_Y_SUP, -2.31f);
            SacarVel(210f, 330f);
        }
        // Destrucción Inferior
        else if (transform.position.y < LIMITE_Y_INF)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bala"))
        {
            golpes++;
            Color colorN = Color.green;

            if (golpes == 1) colorN = Color.yellow;
            else if (golpes == 2) colorN = Color.red;

            SetCuboColor(colorN);

            if (golpes >= 3)
            {
                AplicarPowerUp();
            }
        }
    }

    private void SetCuboColor(Color c)
    {
        if (cubos == null) return;

        for (int i = 0; i < cubos.Length; i++)
        {
            // El i=0 suele ser el padre, saltamos si no tiene renderer
            if (cubos[i].TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.material.SetColor("_MKGlowColor", c);
            }
        }
    }

    private void AplicarPowerUp()
    {
        // Aumentar vida (Asegúrate de que MovJug.vida esté bien sincronizado)
        MovJug.vida += 10;

        if (MenuPausa.BSonido != null)
            MenuPausa.BSonido.Play("Toma");

        Destroy(gameObject);
    }

    void SacarVel(float min, float max)
    {
        ang = Random.Range(min, max);
        // Usamos Mathf.Deg2Rad que es más limpio en Unity 6
        vx = 3 * Mathf.Cos(ang * Mathf.Deg2Rad);
        vy = 3 * Mathf.Sin(ang * Mathf.Deg2Rad);
    }
}