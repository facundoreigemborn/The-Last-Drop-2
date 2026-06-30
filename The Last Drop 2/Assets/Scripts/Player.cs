using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int vida = 3;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int disparo;
    [SerializeField] private bool tieneBazooka = false;

    [Header("Configuración de la Gota")]
    [SerializeField] private GameObject prefabGota;
    [SerializeField] private float velocidadGota = 10f;

    [Header("Configuración del Bazooka")]
    [SerializeField] private GameObject prefabBazooka;
    [SerializeField] private float velocidadMisil = 12f;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioClip sonidoDano;

    private Rigidbody2D rb;
    private Vector2 direccionEntrada;
    private Vector2 ultimaDireccion = new Vector2(0, -1);

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public int Vida
    {
        get => vida;
        private set => vida = Mathf.Max(0, value);
    }

    public float Velocidad
    {
        get => velocidad;
        set => velocidad = value;
    }

    public int Disparo
    {
        get => disparo;
        set => disparo = value;
    }

    public bool TieneBazooka
    {
        get => tieneBazooka;
        set => tieneBazooka = value;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direccionEntrada.x = Input.GetAxisRaw("Horizontal");
        direccionEntrada.y = Input.GetAxisRaw("Vertical");

        if (direccionEntrada != Vector2.zero)
        {
            ultimaDireccion = direccionEntrada.normalized;
        }

        if (animator != null)
        {
            float velocidadActual = direccionEntrada.sqrMagnitude;
            animator.SetFloat("Speed", velocidadActual);

            if (velocidadActual > 0)
            {
                animator.SetFloat("moveX", direccionEntrada.x);
                animator.SetFloat("moveY", direccionEntrada.y);
            }
        }

        if (spriteRenderer != null)
        {
            if (direccionEntrada.x < 0) spriteRenderer.flipX = true;
            else if (direccionEntrada.x > 0) spriteRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(direccionEntrada.x, direccionEntrada.y, 0).normalized;
        Move(dir);
    }

    public void Move(Vector3 dir)
    {
        rb.linearVelocity = new Vector2(dir.x * velocidad, dir.y * velocidad);
    }

    public void Shoot()
    {
        if (tieneBazooka)
        {
            Bazooka();
        }
        else
        {
            Vector2 direccionDisparo = ultimaDireccion;

            if (direccionDisparo == Vector2.zero) direccionDisparo = Vector2.down;

            if (prefabGota != null)
            {
                GameObject gotaNueva = Instantiate(prefabGota, transform.position, Quaternion.identity);
                Gota scriptGota = gotaNueva.GetComponent<Gota>();
                if (scriptGota != null)
                {
                    scriptGota.ConfigurarBala(direccionDisparo, velocidadGota);
                }
            }
        }
    }

    public void Bazooka()
    {
        Vector2 direccionDisparo = ultimaDireccion;

        if (direccionDisparo == Vector2.zero) direccionDisparo = Vector2.down;

        if (prefabBazooka != null)
        {
            GameObject misilNuevo = Instantiate(prefabBazooka, transform.position, Quaternion.identity);
            Gota scriptMisil = misilNuevo.GetComponent<Gota>();
            if (scriptMisil != null)
            {
                scriptMisil.ConfigurarBala(direccionDisparo, velocidadMisil);
            }
        }
    }

    public void ActivarBazooka()
    {
        tieneBazooka = true;
        Debug.Log("¡El NPC me dio el Bazooka! Disparo activado.");
    }

    public void Damage(int cantidadDano)
    {
        Vida -= cantidadDano;
        Debug.Log($"Player recibió daño. Vida restante: {Vida}");

        if (sonidoDano != null)
        {
            Vector3 posicion2D = new Vector3(transform.position.x, transform.position.y, 0f);
            AudioSource.PlayClipAtPoint(sonidoDano, posicion2D);
        }

        if (Vida <= 0)
        {
            Murio();
        }
    }

    private void Murio()
    {
        Debug.Log("Player Muerto - Game Over");
        rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.enabled = false;
        this.enabled = false;
    }
}