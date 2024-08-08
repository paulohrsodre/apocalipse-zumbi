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
        switch (objetoDeColisao.tag){
            case "Inimigo":
                objetoDeColisao.GetComponent<ControleDoInimigo>().TomarDano(danoDoTiro);
            break;
            case "ChefeDeFase":
                objetoDeColisao.GetComponent<ControleDoChefe>().TomarDano(danoDoTiro);
            break;
        }
        Destroy(gameObject);
    }
}
