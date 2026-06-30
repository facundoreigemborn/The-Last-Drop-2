using UnityEngine;

public class Kamikaze : Enemigos
{
    [SerializeField] private float rangoDesaparicion = 8f;
    private Transform player;
    private Player playerScript;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vida = 1;
        daño = 1;
    }

    private void Update()
    {
        if (playerScript == null)
            playerScript = player.GetComponent<Player>();

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > rangoDesaparicion)
        {
            Morir();
            return;
        }

        ChasePlayer();
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * velocidad * Time.deltaTime;

        if (direction.x < -0.01f)
            spriteRenderer.flipX = true;
        else if (direction.x > 0.01f)
            spriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Atacar();
        }
    }

    public override void Atacar()
    {
        if (playerScript != null)
            playerScript.Damage(daño);
        Morir();
    }

    public override void Morir()
    {
        Destroy(gameObject);
    }
}
