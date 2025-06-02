using UnityEngine;

public class NetManager : MonoBehaviour
{
    [SerializeField] private string ballTag;
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ballTag)
        {
            gameManager.Score = gameManager.Score + 1;
        }
    }
}
