using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiraVida : MonoBehaviour
{
    [SerializeField] int valor = 10;
    private void OnTriggerEnter(Collider other)
    {
        var vida = other.GetComponent<Vida>();
        if (vida != null)
            vida.RetiraVida(valor);
    }
}
