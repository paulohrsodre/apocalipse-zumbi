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

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao)
        {
            temporizador += Time.deltaTime;

            if (temporizador >= temporizadorDeZumbis)
            {
                StartCoroutine(GerarNovoZumbi());
                temporizador = 0;
            }
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

        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
