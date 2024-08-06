using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioSource instancia;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instancia = audioSource;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
