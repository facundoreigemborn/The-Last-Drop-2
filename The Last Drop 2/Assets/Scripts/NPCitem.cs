using UnityEngine;
using TMPro;

// Heredamos de NPCs para estar ligados como pide tu compañero
public class NPCitem : NPCs
{
    [Header("Variables del Diagrama")]
    [SerializeField] private string item = "Bazooka";
    private bool entregado = false;

    [Header("UI del Diálogo")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI textoDialogo;
    [SerializeField] private GameObject textoPressE;
    [SerializeField, TextArea] private string elTextoQueVaADecir = "Perfect. The machine room door is now open... and your drop is a bazooka. Go create some chaos!";

    [Header("Eventos")]
    [SerializeField] private GameObject puertaAAbrir;

    private bool playerCerca = false;
    private bool dialogoAbierto = false;

    private void Start()
    {
        if (panelDialogo != null) panelDialogo.SetActive(false);
        if (textoPressE != null) textoPressE.SetActive(false);
    }

    private void Update()
    {
        if (playerCerca && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            if (textoPressE != null) textoPressE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
            if (textoPressE != null) textoPressE.SetActive(false);
            if (panelDialogo != null) panelDialogo.SetActive(false);
            dialogoAbierto = false;
        }
    }

    // --- LA FUNCIÓN QUE PIDE EL DIAGRAMA ---
    public void EntregarItem(Player p)
    {
        if (entregado == false)
        {
            if (item == "Bazooka")
            {
                p.ActivarBazooka();
            }
            entregado = true;
        }
    }

    // --- LA ACCIÓN AL APRETAR LA E ---
    public override void Interactuar()
    {
        // 1. Mostrar/Ocultar el panel igual que en tu otro script
        dialogoAbierto = !dialogoAbierto;
        panelDialogo.SetActive(dialogoAbierto);
        textoDialogo.text = elTextoQueVaADecir;

        // 2. Ejecutar los eventos SOLO cuando el diálogo se abre (no cuando se cierra)
        if (dialogoAbierto)
        {
            // Desaparecemos la puerta
            if (puertaAAbrir != null)
            {
                puertaAAbrir.SetActive(false);
            }

            // Buscamos al jugador y le damos el item
            Player elJugador = FindAnyObjectByType<Player>();
            if (elJugador != null)
            {
                EntregarItem(elJugador);
            }
        }
    }
}