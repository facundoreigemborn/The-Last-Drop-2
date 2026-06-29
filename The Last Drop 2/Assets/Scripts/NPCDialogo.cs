using TMPro;
using UnityEngine;

public class NPCDialogo : NPCs
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI textoDialogo;
    [SerializeField] private GameObject textoPressE;
    [SerializeField] private string pista = "They say the truth hides in plain sight… maybe you should trust your true colors.";

    private bool playerCerca = false;
    private bool dialogoAbierto = false;

    private void Start()
    {
        panelDialogo.SetActive(false);
        textoPressE.SetActive(false);
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
            textoPressE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
            textoPressE.SetActive(false);
            panelDialogo.SetActive(false);
            dialogoAbierto = false;
        }
    }

    public override void Interactuar()
    {
        dialogoAbierto = !dialogoAbierto;
        panelDialogo.SetActive(dialogoAbierto);
        textoDialogo.text = pista;
    }
}
