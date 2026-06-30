using UnityEngine;

public class Rata : Enemigos
{
    [Header("Variables del Diagrama")]
    [SerializeField] private Vector3 puntoA;
    [SerializeField] private Vector3 puntoB;

    private Vector3 destinoActual;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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

    public void Patrullar()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = (destinoActual == puntoB) ? puntoA : puntoB;
            GirarSprite();
        }
    }

    public override void Atacar()
    {
        Debug.Log("¡La rata está atacando al Player!");
    }

    public void RecibirDano(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Rata golpeada. Vida restante: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    public override void Morir()
    {
        Debug.Log("¡La rata pasó a mejor vida!");
        Destroy(gameObject);
    }

    private void GirarSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = destinoActual != puntoB;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player jugador = collision.gameObject.GetComponent<Player>();
            if (jugador != null)
            {
                Atacar();
                jugador.Damage(daño);
            }
        }

        if (collision.gameObject.CompareTag("Proyectil") || collision.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();
            if (jugador != null)
            {
                Atacar();
                jugador.Damage(daño);
            }
        }

        if (other.CompareTag("Proyectil") || other.gameObject.name.Contains("Gota"))
        {
            RecibirDano(1);
            Destroy(other.gameObject);
        }
    }
}