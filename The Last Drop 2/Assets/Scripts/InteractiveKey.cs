using UnityEngine;

public class InteractiveKey : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public string interactKey = "e";
    public Collider2D playerCollider;

    [Header("Referencias de Objetos")]
    [SerializeField] private GameObject textoPressE;
    [SerializeField] private GameObject blockedPipePortal;

    [Header("Puerta")]
    [SerializeField] private Animator animatorPuerta;
    [SerializeField] private Collider2D colliderPuerta;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioClip sonidoColeccion;

    [Header("Estado")]
    public bool hasBeenCollected = false;
    private bool jugadorCerca = false;

    private void Start()
    {
        if (textoPressE != null)
            textoPressE.SetActive(false);
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(interactKey) && !hasBeenCollected)
        {
            hasBeenCollected = true;
            Debug.Log("¡Llave recogida con éxito!");

            if (sonidoColeccion != null)
            {
                Vector3 posicion2D = new Vector3(transform.position.x, transform.position.y, 0f);
                AudioSource.PlayClipAtPoint(sonidoColeccion, posicion2D);
            }

            if (textoPressE != null)
                textoPressE.SetActive(false);

            if (animatorPuerta != null)
            {
                animatorPuerta.Play("Door");
                colliderPuerta.enabled = false;
            }

            if (blockedPipePortal != null)
            {
                TeleportPipe scriptCano = blockedPipePortal.GetComponent<TeleportPipe>();
                if (scriptCano != null)
                {
                    scriptCano.estaDesbloqueado = true;
                    Debug.Log("¡Caño desbloqueado!");
                }
            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.GetComponent<Player>() != null)
        {
            jugadorCerca = true;
            Debug.Log("El jugador está tocando la llave. ¡Apretá la E!");

            if (textoPressE != null && !hasBeenCollected)
                textoPressE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.GetComponent<Player>() != null)
        {
            jugadorCerca = false;

            if (textoPressE != null)
                textoPressE.SetActive(false);
        }
    }
}