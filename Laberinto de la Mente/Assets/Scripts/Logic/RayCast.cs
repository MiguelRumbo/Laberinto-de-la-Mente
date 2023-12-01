using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 1.5f;

    public Texture2D puntero;
    public GameObject TextDetect;
    GameObject ultimoDetectado = null;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        TextDetect.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            Deselect();
            SelectecObject(hit.transform);
            if(hit.collider.tag == "DoorHorizontal")
            {
                if(Input.GetKeyUp(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<DoorHorizontal>().ChangeDoorState();
                }
            }
            if(hit.collider.tag == "DoorVertical")
            {
                if(Input.GetKeyUp(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<DoorVertical>().ChangeDoorState();
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            Deselect();
        }
    }

    void SelectecObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        ultimoDetectado = transform.gameObject;
    }

    void Deselect()
    {
        if (ultimoDetectado)
        {
            ultimoDetectado.GetComponent<Renderer>().material.color = Color.white;
            ultimoDetectado = null;
        }
    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        if(ultimoDetectado)
        {
            TextDetect.SetActive(true);
        }
        else
        {
            TextDetect.SetActive(false);
        }
    }
}
