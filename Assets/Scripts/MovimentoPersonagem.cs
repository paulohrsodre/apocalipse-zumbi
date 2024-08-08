using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    private void Awake() {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Movimentar(Vector3 direcao, float velocidade)
    {
        meuRigidbody.MovePosition(meuRigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(rotacao);
    }

    public void Morrer()
    {
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
