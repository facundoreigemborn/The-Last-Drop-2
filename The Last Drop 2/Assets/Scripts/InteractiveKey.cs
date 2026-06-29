using UnityEngine;

public class InteractiveKey : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public string interactKey = "e";
    public Collider2D playerCollider;

    [Header("Referencias de Objetos")]
    [SerializeField] private GameObject textoPressE; // <-- NUEVO: Arrastrá acá el Canvas o Texto hijo
    public GameObject blockedPipePortal;

    [Header("Estado")]
    public bool hasBeenCollected = false;

    private bool jugadorCerca = false;

    private void Start()
    {
        // Nos aseguramos de que el cartel arranque apagado al empezar el juego
        if (textoPressE != null)
        {
            textoPressE.SetActive(false);
        }
    }

    private void Update()
    {
        // Si el jugador está cerca, aprieta la E, y la llave no fue recogida
        if (jugadorCerca && Input.GetKeyDown(interactKey) && !hasBeenCollected)
        {
            hasBeenCollected = true;
            Debug.Log("¡Llave recogida con éxito!");

            // Ocultamos el cartel inmediatamente al agarrar la llave
            if (textoPressE != null)
            {
                textoPressE.SetActive(false);
            }

            // Buscamos el script del caño en el objeto que arrastraste y lo desbloqueamos
            if (blockedPipePortal != null)
            {
                TeleportPipe scriptCano = blockedPipePortal.GetComponent<TeleportPipe>();
                if (scriptCano != null)
                {
                    scriptCano.estaDesbloqueado = true;
                    Debug.Log("¡Caño desbloqueado!");
                }
            }

            // Ocultamos la llave del mapa
            gameObject.SetActive(false);
        }
    }

    // Esto detecta cuando el jugador TOCA la llave
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.GetComponent<Player>() != null)
        {
            jugadorCerca = true;
            Debug.Log("El jugador está tocando la llave. ¡Apretá la E!");

            // <-- NUEVO: Mostramos el cartel cuando entra el jugador
            if (textoPressE != null && !hasBeenCollected)
            {
                textoPressE.SetActive(true);
            }
        }
    }

    // Esto detecta cuando el jugador SE ALEJA de la llave
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.GetComponent<Player>() != null)
        {
            jugadorCerca = false;

            // <-- NUEVO: Ocultamos el cartel cuando se va
            if (textoPressE != null)
            {
                textoPressE.SetActive(false);
            }
        }
    }
}