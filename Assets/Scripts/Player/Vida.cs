using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] int vida = 100;
    Color atual;
    [SerializeField] Color Cor_Perder_Vida = Color.red;
    [SerializeField] float tempoMudaCor = 1.0f;
    [SerializeField] bool destroyWhenDead = false;
    Renderer _renderer;
    public void RetiraVida(int valor)
    {

        vida -= valor;
        StartCoroutine("MudaCorTempo");
        if (vida <= 0)
        {
            Morre();
        }
    }
    IEnumerator MudaCorTempo()
    {
        float currentTempo = tempoMudaCor;
        atual = _renderer.material.color;
        _renderer.material.color = Cor_Perder_Vida;
        while (currentTempo > 0)
        {
            yield return null;
            currentTempo -= Time.deltaTime;
        }
        _renderer.material.color = atual;
    }

    internal void Morre()
    {
        vida = 0;

        Messages.instance.showMessage("You are dead!",Color.red);
        if(destroyWhenDead) Destroy(this.gameObject);

    }
    public int GetVida()
    {
        return vida;
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        atual = _renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
