using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Atacar(bool estado)
    {
        animator.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorDeMovimento)
    {
        animator.SetFloat("Movendo", (valorDeMovimento));
    }

    public void Morrer()
    {
        animator.SetTrigger("Morrendo");
    }
}
