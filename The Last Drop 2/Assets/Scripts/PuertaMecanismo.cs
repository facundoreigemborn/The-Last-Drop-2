using UnityEngine;

public class PuertaMecanismo : MonoBehaviour
{
    [SerializeField] private Animator animatorPuerta;
    [SerializeField] private Collider2D colliderPuerta;

    public void AbrirPuerta()
    {
        if (animatorPuerta != null)
        {
            animatorPuerta.Play("Door");
        }

        if (colliderPuerta != null)
        {
            colliderPuerta.enabled = false;
        }
    }
}