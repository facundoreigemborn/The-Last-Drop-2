using UnityEngine;

public class TeleportPipe : MonoBehaviour
{
    [Header("Configuración de Destino")]
    public Transform destinoPipe;

    [Header("Estado del Caño")]
    public bool estaDesbloqueado = true;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioClip sonidoTeleport;

    private static float tiempoProximoTeleport = 0f;
    private float cooldown = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!estaDesbloqueado || Time.time < tiempoProximoTeleport) return;

        Player jugador = collision.GetComponent<Player>();

        if (jugador != null)
        {
            tiempoProximoTeleport = Time.time + cooldown;

            if (sonidoTeleport != null)
            {
                Vector3 posicion2D = new Vector3(transform.position.x, transform.position.y, 0f);
                AudioSource.PlayClipAtPoint(sonidoTeleport, posicion2D);
            }

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.position = destinoPipe.position;
            }
            else
            {
                collision.transform.position = destinoPipe.position;
            }

            Debug.Log("¡Jugador teletransportado con éxito!");
        }
    }
}