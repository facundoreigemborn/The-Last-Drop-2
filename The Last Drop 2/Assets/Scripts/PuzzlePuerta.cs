using UnityEngine;

public class PuzzlePuerta : Trampas
{
    [SerializeField] private ZonaBloque[] zonas;
    [SerializeField] private Animator animatorPuerta;
    [SerializeField] private Collider2D colliderPuerta;

    public void VerificarPuzzle()
    {
        foreach (ZonaBloque zona in zonas)
        {
            if (!zona.CajaEnLugar()) return;
        }
        animatorPuerta.Play("Door");
        colliderPuerta.enabled = false;
    }

    public override void Activar(GameObject objetivo) { }
}
