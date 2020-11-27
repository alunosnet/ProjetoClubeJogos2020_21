using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] int vida = 100;
    Color atual;
    [SerializeField]Color Cor_Perder_Vida = Color.red;
    [SerializeField] float tempoMudaCor = 1.0f;
    public void RetiraVida(int valor)
    {
        //TODO: mudar a cor do material para vermelho temporariamente
        vida -= valor;
        StartCoroutine("MudaCorTempo");
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator MudaCorTempo()
    {
        float currentTempo = tempoMudaCor;
        atual = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color=Cor_Perder_Vida;
        while (currentTempo > 0)
        {
            yield return null;
            currentTempo -= Time.deltaTime;
        }
        GetComponent<Renderer>().material.color=atual;
    }
    // Start is called before the first frame update
    void Start()
    {
        atual = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
