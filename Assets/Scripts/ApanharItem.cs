using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApanharItem : MonoBehaviour
{
    public string nome;
    public Texture2D imagem;
    public bool desapareceQuandoApanhado;
    public bool desapareceQuandoUtilizado;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
        item.nome = nome;
        item.imagem = imagem;
        item.desapareceQuandoApanhado = desapareceQuandoApanhado;
        item.desapareceQuandoUtilizado = desapareceQuandoUtilizado;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Inventario>().Adicionar(item);
            if (desapareceQuandoApanhado)
                Destroy(this.gameObject);
        }
    }
}
