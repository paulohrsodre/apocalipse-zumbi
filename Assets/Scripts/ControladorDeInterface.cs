using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorDeInterface : MonoBehaviour
{
    private ControleDoJogador controleDoJogador;
    public Slider SliderVidaDoJogador;
    public GameObject PainelDeGameOver;
    public TMP_Text TextoTempoDeSobrevivência;
    public TMP_Text TextoTempoDeSobrevivênciaMaximo;
    private float tempoMaximoSalvo;
    private int quantidadeDeZumbisMortos;
    public TMP_Text TextoQuantidadeDeZumbisMortos;

    // Start is called before the first frame update
    void Start()
    {
        controleDoJogador = GameObject.FindWithTag("Player").GetComponent<ControleDoJogador>();
        SliderVidaDoJogador.maxValue = controleDoJogador.statusJogador.Vida;
        AtualizaSliderVidaDoJogador();
        Time.timeScale = 1;
        tempoMaximoSalvo = PlayerPrefs.GetFloat("TempoMaximo");
    }

    public void AtualizaSliderVidaDoJogador()
    {
        SliderVidaDoJogador.value = controleDoJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbiMmortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoDeSobrevivência.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarTempoMaximo(minutos, segundos);
    }

    void AjustarTempoMaximo(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoMaximoSalvo) {
            tempoMaximoSalvo = Time.timeSinceLevelLoad;
            TextoTempoDeSobrevivênciaMaximo.text = string.Format("Seu melhor tempo é: {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("TempoMaximo", tempoMaximoSalvo);
        }
        if(TextoTempoDeSobrevivênciaMaximo.text == ""){
            min = (int)tempoMaximoSalvo / 60;
            seg = (int)tempoMaximoSalvo % 60;
            TextoTempoDeSobrevivênciaMaximo.text = string.Format("Seu melhor tempo é: {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }
}
