using UnityEngine;
using System.Collections;

public class Boss : Enemigos
{
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private GameObject kamikazePrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float rangoMovimiento = 4f;
    [SerializeField] private float tiempoEntreMovimientos = 3f;
    [SerializeField] private float tiempoEntreTandas = 5f;
    [SerializeField] private int proyectilesPorTanda = 3;
    [SerializeField] private float tiempoEntreProyectiles = 0.3f;

    private Vector3 posicionCentral;
    private Vector3 destinoActual;
    private float timerMovimiento;
    private int contadorTandas = 0;

    private void Start()
    {
        posicionCentral = transform.position;
        destinoActual = posicionCentral;
        StartCoroutine(CicloAtaques());
    }

    private void Update()
    {
        // Si la vida heredada de Enemigos llega a 0, el Boss muere
        if (vida <= 0)
        {
            Morir();
            return;
        }

        timerMovimiento -= Time.deltaTime;
        if (timerMovimiento <= 0f)
        {
            ElegirNuevoDestino();
            timerMovimiento = tiempoEntreMovimientos;
        }

        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);
    }

    private void ElegirNuevoDestino()
    {
        Vector2 offset = Random.insideUnitCircle * rangoMovimiento;
        destinoActual = posicionCentral + new Vector3(offset.x, offset.y, 0f);
    }

    private IEnumerator CicloAtaques()
    {
        while (vida > 0)
        {
            yield return new WaitForSeconds(tiempoEntreTandas);
            yield return StartCoroutine(DispararTanda());

            contadorTandas++;
            if (contadorTandas >= 2)
            {
                contadorTandas = 0;
                InvocarKamikazes();
            }
        }
    }

    private IEnumerator DispararTanda()
    {
        for (int i = 0; i < proyectilesPorTanda; i++)
        {
            Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            yield return new WaitForSeconds(tiempoEntreProyectiles);
        }
    }

    private void InvocarKamikazes()
    {
        Instantiate(kamikazePrefab, transform.position + Vector3.left * 1.5f, Quaternion.identity);
        Instantiate(kamikazePrefab, transform.position + Vector3.right * 1.5f, Quaternion.identity);
    }

    // --- NUEVO MÉTODO: RECIBIR DAÑO ---
    public void RecibirDano(int cantidad)
    {
        vida -= cantidad; // Resta a la vida heredada
        Debug.Log("¡Boss golpeado! Vida restante: " + vida);
    }

    // --- DETECCIÓN DE IMPACTOS (TRIGGER Y COLLISION) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Si toca al jugador, le hace daño
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.Damage(daño);
        }

        // 2. Si lo toca tu gota de agua (Tag: Proyectil)
        if (other.CompareTag("Proyectil") || other.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1); // Le descuenta 1 de vida
            Destroy(other.gameObject); // Rompe la gotita del jugador
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Por si tus gotas usan colisión sólida en vez de Trigger
        if (collision.gameObject.CompareTag("Proyectil") || collision.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1);
            Destroy(collision.gameObject);
        }
    }

    public override void Atacar()
    {
        // El ataque se maneja por la corrutina CicloAtaques
    }

    public override void Morir()
    {
        Debug.Log("¡El Boss de fuego fue destruido!");
        Destroy(gameObject);
    }
}