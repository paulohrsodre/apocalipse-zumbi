using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDaCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject Jogador;
    private Vector3 compesarDistancia;
    // Start is called before the first frame update
    void Start()
    {
        compesarDistancia = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jogador.transform.position + compesarDistancia;
    }
}
