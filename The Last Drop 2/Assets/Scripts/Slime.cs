using UnityEngine;

public class Slime : Enemigos
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectDistance = 5f;
    [SerializeField] private float explodeDistance = 0.6f;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float bounceInterval = 1f;

    private Player playerScript;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private float bounceTimer = 0f;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // --- EL SEGURO: Si te olvidaste de poner el AudioSource en el Inspector, se agrega solo ---
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Evitamos que haga ruido al iniciar el nivel
        }
    }

    private void Start()
    {
        // Inicializamos el timer con el intervalo para que el sonido de salto no se sature al principio
        bounceTimer = bounceInterval;
    }

    private void Update()
    {
        if (playerScript == null && player != null)
            playerScript = player.GetComponent<Player>();

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectDistance)
        {
            ChasePlayer();

            if (distance <= explodeDistance)
                Atacar();
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (vida <= 0)
            Morir();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil") || other.gameObject.name.Contains("Gota"))
        {
            vida--;
            Destroy(other.gameObject);

            if (vida <= 0)
                Morir();
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * velocidad * Time.deltaTime;

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        bool isMoving = direction.sqrMagnitude > 0.0001f;
        animator.SetBool("IsMoving", isMoving);

        // Control del sonido de rebote rítmico
        if (isMoving)
        {
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0f)
            {
                if (bounceSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(bounceSound);
                }
                bounceTimer = bounceInterval;
            }
        }

        if (direction.x < -0.01f)
            spriteRenderer.flipX = true;
        else if (direction.x > 0.01f)
            spriteRenderer.flipX = false;
    }

    public override void Atacar()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        if (playerScript != null)
            playerScript.Damage(daño);

        Morir();
    }

    public override void Morir()
    {
        if (isDead) return;
        isDead = true;
        Destroy(gameObject);
    }
}