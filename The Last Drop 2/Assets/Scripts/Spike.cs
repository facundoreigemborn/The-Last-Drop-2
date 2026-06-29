using UnityEngine;

public class Spike : Trampas
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(1);
                print("hizo daño");
            }
        }
    }

    public override void Activar(GameObject objetivo) { }
}