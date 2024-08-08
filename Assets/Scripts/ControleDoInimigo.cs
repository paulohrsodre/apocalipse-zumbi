using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoInimigo : MonoBehaviour, IMatavel
{
    [SerializeField]
    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusDoInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float chanceDeGerarKitMedico = 0.1f;
    public GameObject PrefabKitMedico;
    private ControladorDeInterface controladorDeInterface;
    [HideInInspector] public GeradorDeZumbis gerador;

    void Start() {
        Jogador = GameObject.FindWithTag(Tags.Jogador);
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        AleatorizarZumbis();
        statusDoInimigo = GetComponent<Status>();
        controladorDeInterface = GameObject.FindObjectOfType(typeof(ControladorDeInterface)) as ControladorDeInterface;
    }

    void FixedUpdate() {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
 
        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15) {
            Vagar();
        } else if (distancia > 2.5){
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusDoInimigo.velocidade);
            animacaoInimigo.Atacar(false);
        } else {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }

    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0) {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if(ficouPertoOSuficiente == false) {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusDoInimigo.velocidade);
        } 
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador() {
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControleDoJogador>().TomarDano(dano);
    }

    void AleatorizarZumbis()
    {     
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusDoInimigo.Vida -= dano;
        if(statusDoInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        this.enabled = false;
        movimentaInimigo.Morrer();
        ControleDeAudio.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoDoKitMedico(chanceDeGerarKitMedico);
        controladorDeInterface.AtualizarQuantidadeDeZumbiMmortos();
        gerador.DiminuirQuantidadeDeZumbisGerados();
    }

    void VerificarGeracaoDoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao) {
            Instantiate(PrefabKitMedico, transform.position, Quaternion.identity);
        }
    }
}
