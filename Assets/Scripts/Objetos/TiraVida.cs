using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiraVida : MonoBehaviour
{
    [SerializeField] int valor = 10;
    [SerializeField] bool mata = false;
    private void OnTriggerEnter(Collider other)
    {
        Processa(other.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Processa(collision.gameObject);
    }
    private void Processa(GameObject other)
    {
        var vida = other.GetComponent<Vida>();
        if (vida != null)
        {
            if (mata)
                vida.Morre();
            else
                vida.RetiraVida(valor);
        }
    }


}
