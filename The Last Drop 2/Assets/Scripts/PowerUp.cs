using UnityEngine;
public enum TipoPowerUp
{
    Curacion,
    MejoraArma,
    Inmunidad
}

public abstract class PowerUp : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float duracion;
    [SerializeField] protected TipoPowerUp tipo;

    public abstract void Aplicar(Player jugador);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player jugador = collision.GetComponent<Player>();

        if (jugador != null)
        {
            Aplicar(jugador);
            Destroy(gameObject);
        }
    }
}