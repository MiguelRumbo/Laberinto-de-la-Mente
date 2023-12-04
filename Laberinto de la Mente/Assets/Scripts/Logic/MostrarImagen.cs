using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MostrarImagen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image imagenAparecer;

    private void Start()
    {
        // Obtén el componente Image del objeto actual
        imagenAparecer = GetComponent<Image>();

        // Asegúrate de que la imagen esté apagada al inicio
        imagenAparecer.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // El mouse ha entrado en el área del botón
        imagenAparecer.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // El mouse ha salido del área del botón
        imagenAparecer.enabled = false;
    }
}
