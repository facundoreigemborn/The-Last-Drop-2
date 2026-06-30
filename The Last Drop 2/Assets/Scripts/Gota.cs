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
        if (col.gameObject.CompareTag("Enemigo"))
        {
            Boss boss = col.GetComponent<Boss>();
            if (boss != null)
            {
                boss.RecibirDano(dano);
            }
            else
            {
                Destroy(col.gameObject);
            }
            ReproducirSonidoImpacto();
            Destroy(gameObject);
        }
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
            Vector3 posicion2D = new Vector3(transform.position.x, transform.position.y, 0f);
            AudioSource.PlayClipAtPoint(sonidoImpacto, posicion2D);
        }
    }
}