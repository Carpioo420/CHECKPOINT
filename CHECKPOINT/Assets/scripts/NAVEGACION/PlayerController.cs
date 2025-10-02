using UnityEngine;
using UnityEngine.InputSystem;
using CH = ControlesHumanos; // alias para simplificar

public class PlayerController : MonoBehaviour, CH.ControlesHumanos.IMapaAndarActions
{
    private CH.ControlesHumanos controles;
    private CH.ControlesHumanos.MapaAndarActions accionesJugador;
    private CharacterController controlador;
    private Camera camara;

    public float velocidad = 5f;
    public float sensibilidadMouse = 2f;
    public float fuerzaSalto = 5f;

    private Vector2 inputMovimiento;
    private Vector2 inputMirada;
    private float velocidadY;
    private float rotacionX;

    void Awake()
    {
        controles = new CH.ControlesHumanos();
        accionesJugador = controles.MapaAndar;
        accionesJugador.SetCallbacks(this);

        controlador = GetComponent<CharacterController>();
        camara = Camera.main;
    }

    void OnEnable() => accionesJugador.Enable();
    void OnDisable() => accionesJugador.Disable();

    void Update()
    {
        // Movimiento
        Vector3 mover = transform.right * inputMovimiento.x + transform.forward * inputMovimiento.y;
        controlador.Move(mover * velocidad * Time.deltaTime);

        // Gravedad
        if (controlador.isGrounded && velocidadY < 0)
            velocidadY = -2f;
        velocidadY += Physics.gravity.y * Time.deltaTime;
        controlador.Move(Vector3.up * velocidadY * Time.deltaTime);

        // Rotación cámara
        rotacionX -= inputMirada.y * sensibilidadMouse;
        rotacionX = Mathf.Clamp(rotacionX, -80f, 80f);
        camara.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);

        transform.Rotate(Vector3.up * inputMirada.x * sensibilidadMouse);
    }

    // === Implementación de IMapaAndarActions ===
    public void OnMoverse(InputAction.CallbackContext context)
    {
        inputMovimiento = context.ReadValue<Vector2>();
    }

    public void OnGirarVista(InputAction.CallbackContext context)
    {
        inputMirada = context.ReadValue<Vector2>();
    }

    public void OnSaltarLibre(InputAction.CallbackContext context)
    {
        if (context.performed && controlador.isGrounded)
            velocidadY = fuerzaSalto;
    }

    public void OnAgacharse(InputAction.CallbackContext context)
    {
        if (context.performed)
            controlador.height = 1f;
        else if (context.canceled)
            controlador.height = 2f;
    }
}
