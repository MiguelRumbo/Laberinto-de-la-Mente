using System.Collections;
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
            SelectObject(hit.transform);
            if (hit.collider.CompareTag("DoorHorizontal"))
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    DoorHorizontal doorHorizontal = hit.collider.GetComponent<DoorHorizontal>();
                    if (doorHorizontal != null)
                    {
                        doorHorizontal.ChangeDoorState();
                    }
                }
            }
            else if (hit.collider.CompareTag("DoorVertical"))
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    DoorVertical doorVertical = hit.collider.GetComponent<DoorVertical>();
                    if (doorVertical != null)
                    {
                        doorVertical.ChangeDoorState();
                    }
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            Deselect();
        }
    }

    void SelectObject(Transform objTransform)
    {
        Renderer renderer = objTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
            ultimoDetectado = objTransform.gameObject;
        }
    }

    void Deselect()
    {
        if (ultimoDetectado != null)
        {
            Renderer renderer = ultimoDetectado.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
                ultimoDetectado = null;
            }
        }
    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        TextDetect.SetActive(ultimoDetectado != null);
    }
}
