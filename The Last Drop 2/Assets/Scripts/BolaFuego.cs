using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    public float velocidad = 6f;
    public int dano = 1; // Ajusta el daño que quieras que haga

    private Vector2 direccion;

    void Start()
    {
        // Busca al jugador por su Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Calcula la dirección exacta hacia el jugador en este instante
            direccion = (player.transform.position - transform.position).normalized;
        }

        // Se destruye solo en 5 segundos si falla el tiro para liberar memoria
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Comprueba si colisionó con el Player
        if (col.CompareTag("Player"))
        {
            Player playerScript = col.GetComponent<Player>();
            if (playerScript != null)
            {
                // Conexión directa con el método de tu Player
                playerScript.Damage(dano);
            }

            Destroy(gameObject); // La bola desaparece al impactar
        }
    }
}