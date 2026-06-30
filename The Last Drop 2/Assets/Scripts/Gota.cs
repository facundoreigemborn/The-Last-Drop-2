using UnityEngine;

public class Gota : MonoBehaviour
{
    private Vector2 direccion;
    private float velocidad;
    public int dano = 1;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioClip sonidoImpacto;

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
        // Si choca contra un enemigo
        if (col.gameObject.CompareTag("Enemigo"))
        {
            FuegoBoss boss = col.GetComponent<FuegoBoss>();

            if (boss != null)
            {
                boss.TomarDano(dano);
            }
            else
            {
                Destroy(col.gameObject);
            }

            ReproducirSonidoImpacto();
            Destroy(gameObject);
        }
        // Si choca contra el mapa (muros o suelos)
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground") || col.gameObject.name.Contains("Muro"))
        {
            ReproducirSonidoImpacto();
            Destroy(gameObject);
        }
    }

    private void ReproducirSonidoImpacto()
    {
        if (sonidoImpacto != null)
        {
            // Forzamos Z en 0f para que el sonido no se reproduzca lejos de la cámara
            Vector3 posicion2D = new Vector3(transform.position.x, transform.position.y, 0f);
            AudioSource.PlayClipAtPoint(sonidoImpacto, posicion2D);
        }
    }
}