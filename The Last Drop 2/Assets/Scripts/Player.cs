using UnityEngine;

public class Player : MonoBehaviour
{
    // --- VARIABLES ENCAPSULADAS (Ocultas en la clase, editables en Unity) ---
    [SerializeField] private int vida = 3;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int disparo;
    [SerializeField] private bool tieneBazooka = false;

    // Variables de control de f�sicas internas
    private Rigidbody2D rb;
    private Vector2 direccionEntrada;

    // --- PROPIEDADES P�BLICAS (Para leer o modificar de forma segura desde afuera) ---
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

    // --- MeTODOS DE UNITY ---
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Entrada por teclado
        direccionEntrada.x = Input.GetAxisRaw("Horizontal");
        direccionEntrada.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Enviamos el Vector3 al m�todo Move como pide tu diagrama
        Vector3 dir = new Vector3(direccionEntrada.x, direccionEntrada.y, 0).normalized;
        Move(dir);
    }

    // --- MTODOS DEL DIAGRAMA ---
    public void Move(Vector3 dir)
    {
        // Usamos la variable privada encapsulada para las fsicas
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
        // Modificamos la vida a trav�s de su propiedad para mantener las reglas de negocio
        Vida -= cantidadDano;
        Debug.Log($"Player recibi� da�o. Vida restante: {Vida}");

        if (Vida <= 0)
        {
            Murio();
        }
    }

    private void Murio()
    {
        Debug.Log("Player Muerto - Game Over");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false; // Desactiva el script de movimiento
    }
}
