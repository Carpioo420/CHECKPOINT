using UnityEngine;

public class TriggerSwap : MonoBehaviour
{
    [Header("Objetos a alternar")]
    public GameObject objetoDesactivar;  // El que se oculta
    public GameObject objetoActivar;     // El que aparece

    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (objetoDesactivar != null) objetoDesactivar.SetActive(false);
            if (objetoActivar != null) objetoActivar.SetActive(true);

            Debug.Log("Objeto desactivado y otro activado con la tecla E!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            Debug.Log("Jugador dentro del trigger, presiona E.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            Debug.Log("Jugador salió del trigger.");
        }
    }
}
