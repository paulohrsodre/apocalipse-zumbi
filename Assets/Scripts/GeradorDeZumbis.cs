using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float temporizador = 0;
    public float temporizadorDeZumbis = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temporizador += Time.deltaTime;

        if(temporizador >= temporizadorDeZumbis){
            Instantiate(Zumbi, transform.position, transform.rotation);
            temporizador = 0;
        }
    }
}
