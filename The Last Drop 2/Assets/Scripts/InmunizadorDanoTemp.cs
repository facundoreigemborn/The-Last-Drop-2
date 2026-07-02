using System.Collections;
using UnityEngine;

public class InmunizadorDanoTemp : PowerUp
{
    [Header("Configuración de Inmunidad/Velocidad")]
    [SerializeField] private float multiplicadorVelocidad = 2f;

    public override void Aplicar(Player jugador)
    {
        jugador.StartCoroutine(EfectoInmunidad(jugador));
    }

    private IEnumerator EfectoInmunidad(Player jugador)
    {
        Debug.Log("¡Inmunidad/Súper velocidad activada!");

        float velocidadOriginal = jugador.Velocidad;

        jugador.Velocidad = velocidadOriginal * multiplicadorVelocidad;

        yield return new WaitForSeconds(duracion);

        jugador.Velocidad = velocidadOriginal;

        Debug.Log("El efecto del Power-up ha terminado.");
    }
}