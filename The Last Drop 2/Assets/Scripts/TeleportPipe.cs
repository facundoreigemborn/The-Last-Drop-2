using UnityEngine;

public class TeleportPipe : MonoBehaviour
{
    [Header("Configuración de Destino")]
    public Transform destinoPipe; // El otro caño al que se conecta

    [Header("Estado del Caño")]
    // ACÁ ESTÁ LA MAGIA: Por defecto está activado, pero en el inspector se lo vas a apagar al caño bloqueado
    public bool estaDesbloqueado = true;

    // Variable estática global para que todos los caños sepan si alguien acaba de teletransportarse
    private static float tiempoProximoTeleport = 0f;
    private float cooldown = 0.5f; // Medio segundo de espera antes de poder volver a usar un caño

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el caño está bloqueado o si hay que esperar por el cooldown
        if (!estaDesbloqueado || Time.time < tiempoProximoTeleport) return;

        // Comprobamos si el objeto que entró tiene tu script 'Player'
        Player jugador = collision.GetComponent<Player>();

        if (jugador != null)
        {
            // Actualizamos el tiempo global para bloquear el re-teletransporte instantáneo
            tiempoProximoTeleport = Time.time + cooldown;

            // Buscamos el Rigidbody2D que ya sabemos que tu Player tiene asignado internamente
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Movemos el Rigidbody2D directamente a la posición del caño destino
                rb.position = destinoPipe.position;
            }
            else
            {
                // Por si las moscas, fallback clásico
                collision.transform.position = destinoPipe.position;
            }

            Debug.Log("¡Jugador teletransportado con éxito!");
        }
    }
}