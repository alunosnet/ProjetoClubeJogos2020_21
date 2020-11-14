using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Este script serve para atualizar o estado de um objeto
/// entre diferentes níveis
/// Quando o nível estiver a terminar guarda o estado
/// e quando o nível é carregado atualiza o estado para o que foi guardado
/// </summary>
public class ObjetoMultiNivel : MonoBehaviour
{
    [SerializeField] string MyID;
    [SerializeField] bool SaveOnEnd = false;
    [SerializeField] bool LoadOnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnDestroy()
    {
        Debug.Log("Vou ser destruido");
        //guardar estado
        if (MyID != "" && SaveOnEnd)
        {
            string posicao = String.Format("{0};{1};{2}", transform.position.x, transform.position.y, transform.position.z);
            Debug.Log("Guardei a minha posicao " + posicao);
            PlayerPrefs.SetString(MyID, posicao);
            PlayerPrefs.Save();
        }
    }
    private void OnSceneUnloaded(Scene arg0)
    {
        Debug.Log("Nível terminou "+arg0.name);
        //guardar estado
        if (MyID != "" && SaveOnEnd)
        {
            string posicao = String.Format("{0};{1};{2}", transform.position.x, transform.position.y, transform.position.z);
            Debug.Log("Guardei a minha posicao " + posicao);
            PlayerPrefs.SetString(MyID, posicao);
            PlayerPrefs.Save();
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Nível começou " + arg0.name);
        if (MyID != "" && LoadOnStart)
        {
            string posicao=PlayerPrefs.GetString(MyID,"");
            Debug.Log("A minha posicao "+posicao);
            if (posicao != "")
            {
                string[] pos = posicao.Split(';');
                transform.position = new Vector3(float.Parse(pos[0]),
                    float.Parse(pos[1]), float.Parse(pos[2]));
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
