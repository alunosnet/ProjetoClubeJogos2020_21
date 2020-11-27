using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] List<Item> _inventario;
    [SerializeField] GameObject _panel;
    [SerializeField] Image[] _imagens;
    GameObject firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        _inventario = new List<Item>();
        _imagens =Utils.GetComponentsInChildWithoutRoot<Image>(_panel);
        firstPersonController = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _panel.SetActive(!_panel.activeSelf);
            if (_panel.activeSelf)
            {
                ShowItems();
            }
        }
        if (_panel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //firstPersonController.m_MouseLook.SetCursorLock(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //firstPersonController.m_MouseLook.SetCursorLock(true);
        }
    }

    internal void UsaItem(string chave)
    {
        Item item=new Item();

        bool t=HasItem(chave, ref item);
        if (t)
            Remover(item);
    }

    public void ShowItems()
    {
        //limpar inventario
        for (int i = 0; i < _imagens.Length; i++)
        {
            _imagens[i].sprite = null;
        }
        //mostrar inventario
        for (int i = 0; i < _inventario.Count; i++)
        {
            Rect rect = new Rect(0, 0, _inventario[i].imagem.width, _inventario[i].imagem.height);
            _imagens[i].sprite = Sprite.Create(_inventario[i].imagem, rect, new Vector2(0, 0));
        }
    }
    public void Adicionar(Item item)
    {
        if (_inventario.Count >= _imagens.Length) return;
        Item novo = new Item();
        novo.imagem = item.imagem;
        novo.nome = item.nome;
        novo.desapareceQuandoUtilizado = item.desapareceQuandoUtilizado;
        _inventario.Add(novo);
    }

    public void Remover(Item item)
    {
        _inventario.Remove(item);
    }

    public void Remover(string nome)
    {
        for (int i = 0; i < _inventario.Count; i++)
        {
            if(_inventario[i].nome==nome)
            {
                _inventario.RemoveAt(i);
                return;
            }    
        }
    }

    public bool HasItem(Item item)
    {
        return _inventario.Contains(item);
    }

    public bool HasItem(string nome)
    {
        for (int i = 0; i < _inventario.Count; i++)
        {
            if (_inventario[i].nome == nome)
                return true;
        }
        return false;
    }

    public bool HasItem(string nome,ref Item item)
    {
        for (int i = 0; i < _inventario.Count; i++)
        {
            if (_inventario[i].nome == nome)
            {
                item = _inventario[i];
                return true;
            }
        }
        
        return false;
    }
}
