using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControleDoChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentaChefe;
    public GameObject PrefabKitMedico; 

    private void Start() 
    {
        jogador = GameObject.FindWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentaChefe = GetComponent<MovimentoPersonagem>();
    }

    private void Update() 
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if(agente.hasPath == true){
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if(estouPertoDoJogador){
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentaChefe.Rotacionar(direcao);
            } else {
                animacaoChefe.Atacar(false);
            }
        }

    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControleDoJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        if(statusChefe.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        this.enabled = false;
        movimentaChefe.Morrer();
        agente.enabled = false;
        Instantiate(PrefabKitMedico, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);
    }
}
