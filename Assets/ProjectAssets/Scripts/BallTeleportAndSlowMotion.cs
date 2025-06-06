using UnityEngine;

public class BallTeleportAndSlowMotion : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject ball;              // Pelota a controlar
    public Transform playerTransform;    // Transform del jugador

    [Header("Configuración")]
    public float teleportRadius = 2f;    // Radio de teletransporte
    public float slowMotionFactor = 0.3f;// Factor de ralentización

    private Rigidbody ballRigidbody;
    private Vector3 originalGravity;    // Gravedad original
    private float originalDrag;         // Drag original
    private bool isSlowMotion = false;   // Estado del tiempo bala

    void Start()
    {
        // Obtener componentes esenciales
        ballRigidbody = ball.GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        originalDrag = ballRigidbody.linearDamping;
    }

    // Teletransporta la pelota frente al jugador
    public void TeleportBall()
    {
        // Calcular posición en el radio del jugador
        Vector3 teleportDirection = playerTransform.forward;
        Vector3 targetPosition = playerTransform.position + teleportDirection * teleportRadius;

        ballRigidbody.MovePosition(targetPosition);
        ballRigidbody.linearVelocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }

    // Alterna el modo tiempo bala
    public void ToggleSlowMotion()
    {
        isSlowMotion = !isSlowMotion;

        if (isSlowMotion)
        {
            // Activar tiempo bala
            ballRigidbody.linearVelocity *= slowMotionFactor;
            ballRigidbody.angularVelocity *= slowMotionFactor;
            ballRigidbody.linearDamping = 2f; // Mayor resistencia
            Physics.gravity = originalGravity * slowMotionFactor;
        }
        else
        {
            // Restaurar valores normales
            ballRigidbody.linearVelocity /= slowMotionFactor;
            ballRigidbody.angularVelocity /= slowMotionFactor;
            ballRigidbody.linearDamping = originalDrag;
            Physics.gravity = originalGravity;
        }
    }

    // Visualización en el editor
    private void OnDrawGizmosSelected()
    {
        if (playerTransform == null) return;

        // 1. Gizmo de radio (esfera contorno)
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerTransform.position, teleportRadius);

        // 2. Punto exacto de teletransporte
        Vector3 teleportPoint = playerTransform.position + playerTransform.forward * teleportRadius;

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(teleportPoint, 0.1f);

        // 3. Línea guía
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerTransform.position, teleportPoint);
    }
}