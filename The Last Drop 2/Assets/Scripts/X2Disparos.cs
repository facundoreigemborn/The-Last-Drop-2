using UnityEngine;

public class X2Disparos : PowerUp
{
    public override void Aplicar(Player jugador)
    {
        // Activamos la habilidad que ya programaste en tu Player
        jugador.ActivarBazooka();
        Debug.Log("¡Bazooka desbloqueada por PowerUp!");
    }
}