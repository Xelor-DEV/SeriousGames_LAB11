using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class BallThrower : MonoBehaviour
{
    /*
    [Header("References")]
    public OculusInputHandler leftController;
    public OculusInputHandler rightController;
    public GameObject ball;
    public Transform playerCamera;

    [Header("Throw Settings")]
    public float throwForceMultiplier = 1.5f;

    [Header("Teleport Settings")]
    public float teleportDistance = 2f;
    public float teleportHeightOffset = 0.5f;
    public Color gizmoColor = Color.cyan;

    private Rigidbody ballRigidbody;
    private XRGrabInteractable ballGrabInteractable;
    private bool isHoldingBall = false;
    private IXRSelectInteractor currentInteractor; // Cambiado a interfaz

    private void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
        ballGrabInteractable = ball.GetComponent<XRGrabInteractable>();

        // Usar Action<SelectEnterEventArgs> en lugar de UnityAction
        ballGrabInteractable.selectEntered.AddListener(HandleGrab);
        ballGrabInteractable.selectExited.AddListener(HandleRelease);
    }

    private void OnDestroy()
    {
        if (ballGrabInteractable != null)
        {
            ballGrabInteractable.selectEntered.RemoveListener(HandleGrab);
            ballGrabInteractable.selectExited.RemoveListener(HandleRelease);
        }
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        isHoldingBall = true;
        currentInteractor = args.interactorObject;
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        isHoldingBall = false;
        currentInteractor = null;
    }

    private void Update()
    {
        // Detectar trigger para lanzar
        if (isHoldingBall && currentInteractor != null)
        {
            Transform interactorTransform = currentInteractor.transform;

            if (interactorTransform == leftController.transform && leftController.IsTriggerPressed())
            {
                ThrowBall();
            }
            else if (interactorTransform == rightController.transform && rightController.IsTriggerPressed())
            {
                ThrowBall();
            }
        }

        // Detectar botón primario izquierdo para teletransporte
        if (leftController.IsPrimaryButtonPressed())
        {
            TeleportBall();
        }
    }

    public void ThrowBall()
    {
        if (!isHoldingBall || ballRigidbody == null || currentInteractor == null) return;

        // Calcular fuerza basada en velocidad del controlador
        Vector3 throwVelocity = CalculateControllerVelocity();
        ballRigidbody.velocity = throwVelocity * throwForceMultiplier;

        // Liberar la pelota usando la nueva API
        ballGrabInteractable.interactionManager.SelectExit(
            currentInteractor,
            ballGrabInteractable
        );

        isHoldingBall = false;
        currentInteractor = null;
    }

    private Vector3 CalculateControllerVelocity()
    {
        // Obtener velocidad a través del componente XRBaseController
        var controller = currentInteractor.transform.GetComponent<XRBaseController>();
        return controller != null ? controller.velocity : Vector3.zero;
    }

    public void TeleportBall()
    {
        if (ball == null) return;

        Vector3 teleportPosition = CalculateTeleportPosition();
        ball.transform.position = teleportPosition;

        // Resetear física
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }

        // Si estaba agarrada, liberarla
        if (isHoldingBall && currentInteractor != null)
        {
            ballGrabInteractable.interactionManager.SelectExit(
                currentInteractor,
                ballGrabInteractable
            );
            isHoldingBall = false;
            currentInteractor = null;
        }
    }

    private Vector3 CalculateTeleportPosition()
    {
        Vector3 direction = playerCamera.forward;
        direction.y = 0; // Mantener horizontal
        direction.Normalize();

        return playerCamera.position +
               direction * teleportDistance +
               Vector3.up * teleportHeightOffset;
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = gizmoColor;
            Vector3 position = CalculateTeleportPosition();
            Gizmos.DrawSphere(position, 0.1f);
            Gizmos.DrawWireSphere(position, 0.25f);
            Gizmos.DrawLine(playerCamera.position, position);
        }
    }
    */
}