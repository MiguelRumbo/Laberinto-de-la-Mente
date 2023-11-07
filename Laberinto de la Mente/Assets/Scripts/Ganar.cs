using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Ganar : MonoBehaviour
{

        private int contador = 0;
        public TextMeshProUGUI contadorText;
         
        private void OnTriggerEnter(Collider hit)
    {

        if(hit.CompareTag("Llave"))
        {   
            Object.Destroy(hit.gameObject);
            contador++;
            contadorText.text = "" + contador;
            if(contador==3){
                 SceneManager.LoadScene("Ganaste");
            }
        }
    }
}
