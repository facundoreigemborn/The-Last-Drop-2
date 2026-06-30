using UnityEngine;

public class ActivadorBoss : MonoBehaviour
{
    [SerializeField] private Boss boss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boss.Activar();
        }
    }
}
