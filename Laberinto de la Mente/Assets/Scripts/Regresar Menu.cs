using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CambioMenu : MonoBehaviour
{

    public void CambiarASiguienteEscena()
    {
         SceneManager.LoadScene("Escena instrucciones");
    }

}
