using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyController.Agro = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyController.Agro = false;
        }
    }
}
