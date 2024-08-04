using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoInimigo : MonoBehaviour
{
    [SerializeField]
    public GameObject Jogador;

    [SerializeField]
    private float velocidade;
    private Vector3 direcao;
    private Rigidbody rigidbodyInimigo;
    private Animator animatorInimigo;

    void Start() {
        Jogador = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
        rigidbodyInimigo = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        direcao = Jogador.transform.position - transform.position;

        Quaternion rotacao = Quaternion.LookRotation(direcao);
        rigidbodyInimigo.MoveRotation(rotacao);

        if (distancia > 2.5){
            rigidbodyInimigo.MovePosition(rigidbodyInimigo.position + direcao.normalized * velocidade * Time.deltaTime);
            animatorInimigo.SetBool("Atacando", false);
        } else {
            animatorInimigo.SetBool("Atacando", true);
        }

    }

    void AtacaJogador() {
        Time.timeScale = 0;
        Jogador.GetComponent<ControleDoJogador>().TextoGameOver.SetActive(true);
        Jogador.GetComponent<ControleDoJogador>().Vivo = false;
    }
}
