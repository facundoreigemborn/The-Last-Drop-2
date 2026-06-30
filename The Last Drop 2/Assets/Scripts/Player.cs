using UnityEngine;

public class Player : MonoBehaviour
{
    // --- VARIABLES ENCAPSULADAS ---
    [SerializeField] private int vida = 3;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int disparo;
    [SerializeField] private bool tieneBazooka = false;

    [Header("Configuración de la Gota")]
    [SerializeField] private GameObject prefabGota;
    [SerializeField] private float velocidadGota = 10f;

    private Rigidbody2D rb;
    private Vector2 direccionEntrada;
    private Vector2 ultimaDireccion = new Vector2(0, -1); // Guarda a dónde miró por última vez (empieza mirando abajo)

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // --- PROPIEDADES PÚBLICAS ---
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

    // --- MÉTODOS DE UNITY ---
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

        // Si el jugador se está moviendo, actualizamos la última dirección
        if (direccionEntrada != Vector2.zero)
        {
            ultimaDireccion = direccionEntrada.normalized;
        }

        // --- CONTROL DE ANIMACIONES ---
        if (animator != null)
        {
            float velocidadActual = direccionEntrada.sqrMagnitude;
            animator.SetFloat("Speed", velocidadActual);

            if (velocidadActual > 0)
            {
                // Solo usamos moveX y moveY, que son los que sí tienes en tu Blend Tree
                animator.SetFloat("moveX", direccionEntrada.x);
                animator.SetFloat("moveY", direccionEntrada.y);
            }
        }

        if (spriteRenderer != null)
        {
            if (direccionEntrada.x < 0) spriteRenderer.flipX = true;
            else if (direccionEntrada.x > 0) spriteRenderer.flipX = false;
        }

        // --- DISPARO ---
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

    // --- MÉTODOS DEL DIAGRAMA ---
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
            // Usamos la variable de código en lugar del Animator para saber a dónde disparar
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
        Debug.Log("BOOM! Disparo de Bazooka");
    }

    // --- ¡ACÁ ESTÁ LA FUNCIÓN NUEVA QUE BUSCABA EL NPC! ---
    public void ActivarBazooka()
    {
        tieneBazooka = true;
        Debug.Log("¡El NPC me dio el Bazooka! Disparo activado.");
    }

    public void Damage(int cantidadDano)
    {
        Vida -= cantidadDano;
        Debug.Log($"Player recibió daño. Vida restante: {Vida}");

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