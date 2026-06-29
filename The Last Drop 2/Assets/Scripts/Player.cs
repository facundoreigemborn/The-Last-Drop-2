using UnityEngine;

public class Player : MonoBehaviour
{
    // --- VARIABLES ENCAPSULADAS (Ocultas en la clase, editables en Unity) ---
    [SerializeField] private int vida = 3;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int disparo;
    [SerializeField] private bool tieneBazooka = false;

    // Variables de control de físicas internas
    private Rigidbody2D rb;
    private Vector2 direccionEntrada;

    // Componentes para la animación
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // --- PROPIEDADES PÚBLICAS ---
    public int Vida
    {
        get => vida;
        private set => vida = Mathf.Max(0, value); // Evita que la vida sea menor a 0
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

        // Inicializamos los componentes del Animator y SpriteRenderer
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Dirección inicial por defecto del Idle (mirando abajo)
        if (animator != null)
        {
            animator.SetFloat("idleY", -1f);
        }
    }

    private void Update()
    {
        // Entrada por teclado
        direccionEntrada.x = Input.GetAxisRaw("Horizontal");
        direccionEntrada.y = Input.GetAxisRaw("Vertical");

        // --- CONTROL DE ANIMACIONES ---
        if (animator != null)
        {
            // Calculamos la magnitud de movimiento (0 quieto, >0 moviéndose)
            float velocidadActual = direccionEntrada.sqrMagnitude;
            animator.SetFloat("Speed", velocidadActual);

            if (velocidadActual > 0)
            {
                // Si hay input, actualizamos caminar
                animator.SetFloat("moveX", direccionEntrada.x);
                animator.SetFloat("moveY", direccionEntrada.y);

                // Y guardamos la dirección para cuando pase a Idle
                animator.SetFloat("idleX", direccionEntrada.x);
                animator.SetFloat("idleY", direccionEntrada.y);
            }
        }

        // Espejar el sprite si va a la izquierda usando la animación de la derecha
        if (spriteRenderer != null)
        {
            if (direccionEntrada.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direccionEntrada.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        // ------------------------------

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Enviamos el Vector3 al método Move como pide tu diagrama
        Vector3 dir = new Vector3(direccionEntrada.x, direccionEntrada.y, 0).normalized;
        Move(dir);
    }

    // --- MÉTODOS DEL DIAGRAMA ---
    public void Move(Vector3 dir)
    {
        // Usamos la variable privada encapsulada para las físicas
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
            Debug.Log("Disparo normal!");
        }
    }

    public void Bazooka()
    {
        Debug.Log("BOOM! Disparo de Bazooka");
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
        this.enabled = false; // Desactiva el script de movimiento
    }
}
