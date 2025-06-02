using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private string teleportTag;
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == teleportTag)
        {
            gameManager.Respawn(gameObject);
        }
    }
}
