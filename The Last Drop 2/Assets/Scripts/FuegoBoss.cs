using UnityEngine;

public class FuegoBoss : MonoBehaviour
{
    [Header("Atributos del UML")]
    public int fase = 1;
    public int vidaExtra = 0;

    [Header("Salud del Boss")]
    public int vida = 20; // Los 20 de vida que pediste

    [Header("Configuración de Ataques")]
    public GameObject bolaFuegoPrefab;
    public GameObject bichitoPrefab;
    public Transform puntoDisparo; // De dónde sale la bola naranja
    public Transform[] puntosInvocacion; // De dónde salen los bichitos

    public float tiempoEntreAtaques = 3f;
    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreAtaques)
        {
            DecidirAtaque();
            temporizador = 0f;
        }
    }

    private void DecidirAtaque()
    {
        // 50% de probabilidad para cada ataque
        if (Random.value > 0.5f)
        {
            Atacar();
        }
        else
        {
            AtaqueEspecial();
        }
    }

    public void Atacar()
    {
        if (bolaFuegoPrefab != null && puntoDisparo != null)
        {
            Instantiate(bolaFuegoPrefab, puntoDisparo.position, Quaternion.identity);
        }
    }

    public void AtaqueEspecial()
    {
        InvocarEnemigos();
    }

    public void InvocarEnemigos()
    {
        if (bichitoPrefab != null)
        {
            foreach (Transform punto in puntosInvocacion)
            {
                Instantiate(bichitoPrefab, punto.position, Quaternion.identity);
            }
        }
    }

    // --- NUEVO: MÉTODO PARA RECIBIR DAÑO ---
    public void TomarDano(int cantidad)
    {
        vida -= cantidad;
        Debug.Log($"El Jefe recibió daño. Vida restante: {vida}");

        if (vida <= 0)
        {
            Debug.Log("¡JEFE DERROTADO!");
            Destroy(gameObject);
        }
    }
}