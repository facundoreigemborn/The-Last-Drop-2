using UnityEngine;

public class PuzzlePuerta : Trampas
{
    [SerializeField] private ZonaBloque[] zonas;
    [SerializeField] private GameObject puerta;

    public void VerificarPuzzle()
    {
        foreach (ZonaBloque zona in zonas)
        {
            if (!zona.CajaEnLugar()) return;
        }
        puerta.SetActive(false);
    }

    public override void Activar(GameObject objetivo) { }
}
