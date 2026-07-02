using UnityEngine;

public class CapsulaVida : PowerUp
{
    [SerializeField] private int curacion = 1;

    public override void Aplicar(Player jugador)
    {
        jugador.Damage(-curacion);
        Debug.Log("¡Curado!");
    }
}