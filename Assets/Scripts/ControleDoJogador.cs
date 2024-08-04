using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleDoJogador : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    private Vector3 direcao;
    public LayerMask mascaraChao;
    public GameObject TextoGameOver;
    public bool Vivo = true;
    private Rigidbody rigidbodyjogador;
    private Animator animatorJogador;
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
        rigidbodyjogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if(direcao != Vector3.zero) {
            animatorJogador.SetBool("Movendo", true);
        } else {
            animatorJogador.SetBool("Movendo", false);
        }

        if(Vivo == false) {
            if(Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate() {
        rigidbodyjogador.MovePosition(rigidbodyjogador.position + (direcao * velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, mascaraChao)) {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            rigidbodyjogador.MoveRotation(novaRotacao);
        }
    }
}
