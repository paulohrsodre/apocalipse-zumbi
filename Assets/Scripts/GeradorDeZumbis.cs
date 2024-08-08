using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float temporizador = 0;
    public float temporizadorDeZumbis = 1;
    [SerializeField] LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisGerados = 2;
    private int quantidadeDeZumbisGerados;
    private float tempoDoProximoAumentoDeDificuldade = 5;
    private float temporizadorDeDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        temporizadorDeDificuldade = tempoDoProximoAumentoDeDificuldade;
        for(int i = 0; i < quantidadeMaximaDeZumbisGerados; i++){
            StartCoroutine(GerarNovoZumbi());
        } 
    }

    void Update()
    {
        bool gerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (gerarZumbisPelaDistancia == true && quantidadeDeZumbisGerados < quantidadeMaximaDeZumbisGerados)
        {
            temporizador += Time.deltaTime;

            if (temporizador >= temporizadorDeZumbis)
            {
                StartCoroutine(GerarNovoZumbi());
                temporizador = 0;
            }
        }

        if(Time.timeSinceLevelLoad > temporizadorDeDificuldade){
            quantidadeMaximaDeZumbisGerados++;
            temporizadorDeDificuldade = Time.timeSinceLevelLoad + tempoDoProximoAumentoDeDificuldade;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }

        ControleDoInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControleDoInimigo>();
        zumbi.gerador = this;
        quantidadeDeZumbisGerados++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisGerados()
    {
        quantidadeDeZumbisGerados--;
    }
}
