using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] RawImage _mira;
    [SerializeField] Color _default;
    [SerializeField] Color _interactColor;
    [SerializeField] float maxDistance=5;
    Camera _camera;
    RaycastHit HitInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (_mira == null)
            Debug.Log("Não tem mira");
        _camera = GetComponentInChildren<Camera>();
        if (_camera == null)
            Debug.Log("Não encontrei uma camara");
    }

    // Update is called once per frame
    void Update()
    {
        if (_mira == null) return;
        _mira.color = _default;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out HitInfo, maxDistance))
        {
            var componente = HitInfo.transform.GetComponent<IInteract>();
            if (componente!= null)
                _mira.color = _interactColor;
            if (Input.GetButtonDown("Fire1") && componente != null)
                componente.Action();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var outro = other.GetComponent<IInteract>();
            if (outro != null)
                outro.Action();
        }
    }
}
