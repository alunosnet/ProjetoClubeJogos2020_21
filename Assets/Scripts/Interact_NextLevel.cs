using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact_NextLevel : MonoBehaviour, IInteract
{
    [SerializeField] bool proximo = true;
    [SerializeField] bool ativo = true;
    public void Action()
    {
        if (ativo == false) return;
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
