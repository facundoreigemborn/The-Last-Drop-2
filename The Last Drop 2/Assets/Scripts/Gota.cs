using UnityEngine;

public class Gota : MonoBehaviour
{
    private Vector2 direccion;
    private float velocidad;
    public int dano = 1; // Le quitará 1 de vida por cada golpe

    public void ConfigurarBala(Vector2 dir, float vel)
    {
        direccion = dir;
        velocidad = vel;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo")
        {
            // Intentamos buscar si el enemigo es el Boss
            FuegoBoss boss = col.GetComponent<FuegoBoss>();

            if (boss != null)
            {
                // Si ES el Boss, le restamos vida
                boss.TomarDano(dano);
            }
            else
            {
                // Si NO es el boss (es decir, es un bichito kamikaze), lo destruimos de 1 tiro
                Destroy(col.gameObject);
            }

            // La gota se destruye al chocar
            Destroy(gameObject);
        }
    }
}