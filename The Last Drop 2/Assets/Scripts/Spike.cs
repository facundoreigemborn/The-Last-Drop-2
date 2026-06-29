using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int dano = 1;

    // Hace daño al jugador
    public void HacerDano(Player p)
    {
        p.Damage(dano);
    }

    // Activa la trampa
    public void Activar(Player p)
    {
        HacerDano(p);
    }

    // Cuando el jugador toca la spike
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player jugador = other.GetComponent<Player>();

        if (jugador != null)
        {
            Activar(jugador);
        }
    }
}