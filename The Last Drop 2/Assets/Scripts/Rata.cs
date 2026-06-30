using UnityEngine;

public class Rata : MonoBehaviour
{
    // --- VARIABLES DEL DIAGRAMA ---
    [Header("Variables del Diagrama")]
    [SerializeField] private Vector3 puntoA;
    [SerializeField] private Vector3 puntoB;

    // --- CONFIGURACIÓN DE ATRIBUTOS ---
    [Header("Configuración de Atributos")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private int danoRata = 1;
    [SerializeField] private int vidaMaxima = 2; // Cantidad de disparos que aguanta

    private int vidaActual;
    private Vector3 destinoActual;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidaActual = vidaMaxima; // Arranca con la vida a tope

        // Si los puntos quedan en 0, autogenera una ruta a la derecha
        if (puntoA == Vector3.zero && puntoB == Vector3.zero)
        {
            puntoA = transform.position;
            puntoB = transform.position + new Vector3(3f, 0, 0);
        }

        destinoActual = puntoB;
        GirarSprite();
    }

    private void Update()
    {
        Patrullar();
    }

    // --- MÉTODOS OBLIGATORIOS DEL DIAGRAMA ---

    public void Patrullar()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = (destinoActual == puntoB) ? puntoA : puntoB;
            GirarSprite();
        }
    }

    public void Atacar()
    {
        Debug.Log("¡La rata está atacando al Player!");
    }

    // --- FUNCIÓN PARA RECIBIR DAÑO Y MORIR ---
    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Rata golpeada. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("¡La rata pasó a mejor vida!");
        Destroy(gameObject); // Esto elimina a la rata de la escena por completo
    }

    private void GirarSprite()
    {
        if (spriteRenderer != null)
        {
            if (destinoActual == puntoB)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // --- COLISIONES (DAÑO AL PLAYER Y DETECCIÓN DE BALAS CON TAG PROYECTIL) ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Si choca contra el Player, le hace daño
        if (collision.gameObject.CompareTag("Player"))
        {
            Player jugador = collision.gameObject.GetComponent<Player>();
            if (jugador != null)
            {
                Atacar();
                jugador.Damage(danoRata);
            }
        }

        // 2. Si choca contra tu gota (Tag: Proyectil)
        if (collision.gameObject.CompareTag("Proyectil") || collision.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1); // Le saca 1 de vida a la rata
            Destroy(collision.gameObject); // Destruye la gota para que no la traspase
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Por si tu Player o tus Gotas están configurados como Trigger
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();
            if (jugador != null)
            {
                Atacar();
                jugador.Damage(danoRata);
            }
        }

        if (other.CompareTag("Proyectil") || other.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1);
            Destroy(other.gameObject);
        }
    }
}