using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    private bool activado = false;

    private void Start()
    {
        posicionCentral = transform.position;
        destinoActual = posicionCentral;
    }

    private void Update()
    {
        if (!activado) return;

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

    public void Activar()
    {
        if (activado) return;
        activado = true;
        StartCoroutine(CicloAtaques());
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

    public void RecibirDano(int cantidad)
    {
        if (vida <= 0) return;
        vida -= cantidad;
        Debug.Log("¡Boss golpeado! Vida restante: " + vida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.Damage(daño);
        }
    }

    public override void Atacar() { }

    public override void Morir()
    {
        Debug.Log("¡El Boss de fuego fue destruido!");
        SceneManager.LoadScene(3);
        Destroy(gameObject);
    }
}