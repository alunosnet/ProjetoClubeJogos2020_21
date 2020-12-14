using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Este script controla o acesso aos níveis anterior e próximo 
/// </summary>
public class Passagem : MonoBehaviour
{
    [SerializeField] bool proximo=true;
    [SerializeField] bool ativo = true;

    private void OnTriggerEnter(Collider other)
    {
        if (ativo == false) return;
        if (other.tag.Equals("Player"))
            if (proximo)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
