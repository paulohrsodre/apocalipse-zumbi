using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeInterface : MonoBehaviour
{
    private ControleDoJogador controleDoJogador;
    public Slider SliderVidaDoJogador;
    // Start is called before the first frame update
    void Start()
    {
        controleDoJogador = GameObject.FindWithTag("Player").GetComponent<ControleDoJogador>();
        SliderVidaDoJogador.maxValue = controleDoJogador.Vida;
        AtualizaSliderVidaDoJogador();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AtualizaSliderVidaDoJogador()
    {
        SliderVidaDoJogador.value = controleDoJogador.Vida;
    }
}
