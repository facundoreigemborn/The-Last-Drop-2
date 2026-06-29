using UnityEngine;

// 1. El nombre de la clase respeta el diagrama: NPC_item
public class NPC_item : MonoBehaviour
{
    [Header("Atributos del Diagrama UML")]
    // 2. Las variables exactamente como las pide el diagrama
    public string item;
    public bool entregado = false;

    [Header("Variables internas de Unity")]
    private bool jugadorCerca = false;
    private Player scriptDelJugador; // Nota: El diagrama dice "Personaje", asumo que es tu script "Player"

    private void Update()
    {
        // Si estás cerca, apretás la E, y el item NO fue entregado aún
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !entregado)
        {
            // Ejecutamos el método del diagrama pasándole el jugador
            EntregarItem(scriptDelJugador);
        }
    }

    // 3. El método exactamente como lo pide el diagrama: EntregarItem(p: Personaje)
    public void EntregarItem(Player p)
    {
        entregado = true;
        Debug.Log("¡El NPC te entregó: " + item + "!");

        // Acá evaluamos qué texto pusiste en el Inspector para darte la recompensa real
        if (item == "Bazooka")
        {
            p.TieneBazooka = true;
            Debug.Log("¡Bazooka activado en el jugador!");
        }
        else if (item == "Llave")
        {
            // Acá podrías activar la llave, o sumarla al inventario
            Debug.Log("¡Conseguiste una llave importante!");
        }
    }

    // --- Lógica de físicas (Trigger) para detectar al jugador ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player jugador = collision.GetComponent<Player>();

        if (jugador != null)
        {
            jugadorCerca = true;
            scriptDelJugador = jugador;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            jugadorCerca = false;
            scriptDelJugador = null;
        }
    }
}