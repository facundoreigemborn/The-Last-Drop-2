using UnityEngine;
using TMPro;

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

    public override void Interactuar()
    {
        dialogoAbierto = !dialogoAbierto;
        panelDialogo.SetActive(dialogoAbierto);
        textoDialogo.text = elTextoQueVaADecir;

        if (dialogoAbierto)
        {
            if (puertaAAbrir != null)
            {
                PuertaMecanismo scriptPuerta = puertaAAbrir.GetComponent<PuertaMecanismo>();
                if (scriptPuerta != null)
                {
                    scriptPuerta.AbrirPuerta();
                }
                else
                {
                    puertaAAbrir.SetActive(false);
                }
            }

            Player elJugador = FindAnyObjectByType<Player>();
            if (elJugador != null)
            {
                EntregarItem(elJugador);
            }
        }
    }
}