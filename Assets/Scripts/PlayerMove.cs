using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float velocidadeAndar = 5;
    [SerializeField]
    float velocidadeRodar = 5;
    [SerializeField]
    bool RodarComTeclado = true;
    [SerializeField]
    float speedFactor = 1.5f;
    CharacterController _characterController;
    Animator _animator;
    float inputAndar;
    float inputRodar;
    float inputSprint;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputAndar = CrossPlatformInputManager.GetAxis("Vertical");
        inputRodar = CrossPlatformInputManager.GetAxis("Horizontal");
        inputSprint = CrossPlatformInputManager.GetAxis("Fire3");

        inputAndar = velocidadeAndar * inputAndar + (velocidadeAndar * inputSprint);

        Vector3 novaPosicao = transform.forward *inputAndar;
        novaPosicao.y += Physics.gravity.y;

        _characterController.Move(novaPosicao * Time.deltaTime);

        if (RodarComTeclado == false)
        {
            novaPosicao = transform.right * velocidadeRodar * inputRodar;
            _characterController.Move(novaPosicao * Time.deltaTime);
        }
        else
        {
            _characterController.transform.Rotate(_characterController.transform.up * velocidadeRodar * inputRodar);
        }

        if(_animator!=null) _animator.SetFloat("velocidade", inputAndar);
    }
}
