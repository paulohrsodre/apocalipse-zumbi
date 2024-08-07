using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoJogador : MonoBehaviour, IMatavel, ICuravel
{
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject TextoGameOver;
    public ControladorDeInterface controladorDeInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador movimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    // Start is called before the first frame update
    private void Start()
    {
        movimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate() {
        movimentoJogador.Movimentar(direcao, statusJogador.velocidade);

        movimentoJogador.RotacaoJogador(mascaraChao);
    }

    public void TomarDano(int dano) {
        statusJogador.Vida -= dano;
        controladorDeInterface.AtualizaSliderVidaDoJogador();
        ControleDeAudio.instancia.PlayOneShot(SomDeDano);
        if(statusJogador.Vida <= 0) {
            Morrer();
        }
    }

    public void Morrer() 
    {
        controladorDeInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial){
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        controladorDeInterface.AtualizaSliderVidaDoJogador();
    }
}
