using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float velocidade = 20;
    private Rigidbody rigidbodyTiro;
    public AudioClip SomDeMorte;
    private int danoDoTiro = 1;

    private void Start() {
        rigidbodyTiro = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbodyTiro.MovePosition(rigidbodyTiro.position + transform.forward * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao) {
        if (objetoDeColisao.tag == "Inimigo"){
            objetoDeColisao.GetComponent<ControleDoInimigo>().TomarDano(danoDoTiro);
        }
        Destroy(gameObject);
    }
}
