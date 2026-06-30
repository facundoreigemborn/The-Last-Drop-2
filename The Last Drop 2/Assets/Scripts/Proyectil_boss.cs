using UnityEngine;

public class Proyectil_boss : MonoBehaviour
{
    [SerializeField] private float velocidad = 4f;
    [SerializeField] private float distanciaSeguimiento = 3f;
    [SerializeField] private int daño = 1;
    [SerializeField] private float tiempoVida = 5f;

    private Transform player;
    private Vector3 direccionActual;
    private float distanciaRecorrida = 0f;
    private Vector3 posicionInicial;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        posicionInicial = transform.position;
        direccionActual = (player.position - transform.position).normalized;
        Destroy(gameObject, tiempoVida);
    }

    private void Update()
    {
        distanciaRecorrida = Vector3.Distance(posicionInicial, transform.position);

        if (distanciaRecorrida < distanciaSeguimiento)
        {
            direccionActual = (player.position - transform.position).normalized;
        }

        transform.position += direccionActual * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
                playerScript.Damage(daño);

            Destroy(gameObject);
        }
    }
}
