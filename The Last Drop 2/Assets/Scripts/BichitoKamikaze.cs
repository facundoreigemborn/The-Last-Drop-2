using UnityEngine;

public class BichitoKamikaze : MonoBehaviour
{
    public float velocidad = 3.5f;
    public int dano = 1; // Ajusta el daño

    private Transform playerTransform;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Persigue constantemente la posición actual del Player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, velocidad * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player playerScript = col.GetComponent<Player>();
            if (playerScript != null)
            {
                // Conexión directa con el método de tu Player
                playerScript.Damage(dano);
            }

            Destroy(gameObject); // El bichito explota/desaparece al tocar al jugador
        }
    }
}