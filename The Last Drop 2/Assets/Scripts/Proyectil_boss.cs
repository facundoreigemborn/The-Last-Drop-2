using UnityEngine;

public class Proyectil_boss : MonoBehaviour
{
    [SerializeField] private float velocidad = 4f;
    [SerializeField] private float distanciaSeguimiento = 3f;
    [SerializeField] private int daño = 1;
    [SerializeField] private float tiempoVida = 3f; // Tiempo de vida corto regulable

    private Transform player;
    private Vector3 direccionActual;
    private float distanciaRecorrida = 0f;
    private Vector3 posicionInicial;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            direccionActual = (player.position - transform.position).normalized;
        }
        else
        {
            direccionActual = Vector3.down;
        }

        posicionInicial = transform.position;

        // Destrucción garantizada por tiempo. Al bajarlo en el Inspector a 2 o 3, duran re poco
        Destroy(gameObject, tiempoVida);
    }

    private void Update()
    {
        if (player == null) return;

        distanciaRecorrida = Vector3.Distance(posicionInicial, transform.position);

        if (distanciaRecorrida < distanciaSeguimiento)
        {
            direccionActual = (player.position - transform.position).normalized;
        }

        transform.position += direccionActual * velocidad * Time.deltaTime;
    }

    // --- DETECCIÓN POR TRIGGER (¡Is Trigger activado en tu Collider!) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Damage(daño); // Te saca vida
            }

            Destroy(gameObject); // Se autodestruye al golpearte
        }
    }

    // --- DETECCIÓN POR COLISIÓN ESTÁNDAR ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Damage(daño);
            }

            Destroy(gameObject);
        }
    }
}