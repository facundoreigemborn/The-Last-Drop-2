using UnityEngine;


public class ZonaBloque : Trampas
{
    [SerializeField] private string tagCajaCorrecta;
    [SerializeField] private PuzzlePuerta puzzle;

    private bool cajaEnLugar = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagCajaCorrecta))
        {
            cajaEnLugar = true;
            puzzle.VerificarPuzzle();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagCajaCorrecta))
        {
            cajaEnLugar = false;
        }
    }

    public bool CajaEnLugar() => cajaEnLugar;

    public override void Activar(GameObject objetivo) { }
}
