using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Anim : MonoBehaviour,IInteract
{
    [SerializeField] string nomeParametro;
    Animator _animator;
    public void Action()
    {
        Debug.Log("Vamos animar");
        _animator.SetTrigger(nomeParametro);
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
