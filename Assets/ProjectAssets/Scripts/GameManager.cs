using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private int score;
    [SerializeField] private UIManager uiManager;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            uiManager.UpdatePointsText(score);
        }
    }

    public void Respawn(GameObject respawnObject)
    {
        respawnObject.transform.position = respawnPoint.position;
        Rigidbody rigidbody = respawnObject.GetComponent<Rigidbody>();
        rigidbody.linearVelocity = Vector3.zero;
    }

    public void Start()
    {
        Score = 0;
    }
}
