using UnityEngine;

public class BolaFuegoBoss : MonoBehaviour
{
    public float velocidad = 6f;
    private Vector2 direccion;

    void Start()
    {
        // Al nacer, la bola busca dónde está el jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Calcula la ruta directa hacia el jugador
            direccion = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // Si el player no existe (ya murió), cae hacia abajo
            direccion = Vector2.down;
        }

        // Se destruye sola a los 4 segundos para no llenar la memoria
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        // Se mueve en la dirección calculada
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si choca contra el jugador...
        if (col.gameObject.tag == "Player")
        {
            Player playerScript = col.GetComponent<Player>();
            if (playerScript != null)
            {
                // Llama al método Damage de tu Player (del UML)
                playerScript.Damage(1);
            }

            // La bola de fuego se destruye al golpear
            Destroy(gameObject);
        }
    }
}