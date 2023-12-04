using UnityEngine;

public class LimpiarPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
