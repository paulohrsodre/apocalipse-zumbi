using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            ControleDeAudio.instancia.PlayOneShot(SomDoTiro);
        }
    }
}
