using UnityEngine;

public abstract class Enemigos : MonoBehaviour
{
    [SerializeField] protected int vida;
    [SerializeField] protected int daño;
    [SerializeField] protected float velocidad;

    public abstract void Atacar();
    public abstract void Morir();
}
