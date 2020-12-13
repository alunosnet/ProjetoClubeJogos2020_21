using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] float forca;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.isStatic || other.gameObject.CompareTag("Player")==true) return;
        var rb = other.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Debug.Log("Collidi com " + other.transform.name);
            Vector3 direcao = other.transform.position - transform.position;
            rb.AddForce(direcao.normalized * forca);
        }
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
