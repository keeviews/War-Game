using UnityEngine;

public class PreventOverlapScript : MonoBehaviour
{
    Vector3 pseudopositionXPos;
    Vector3 pseudopositionYPos;
    Vector3 pseudopositionXNeg;
    Vector3 pseudopositionYNeg;



    int num = 0;

    private void Start()
    {
        pseudopositionXPos = gameObject.transform.position;
        pseudopositionYPos = gameObject.transform.position;
        pseudopositionXNeg = gameObject.transform.position;
        pseudopositionYNeg = gameObject.transform.position;
    }

    private void Update()
    {
        Collider2D ColXPos = Physics2D.OverlapBox(pseudopositionXPos, GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("PickUp", "Obstacles"));
        Collider2D ColYPos = Physics2D.OverlapBox(pseudopositionYPos, GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("PickUp", "Obstacles"));
        Collider2D ColXNeg = Physics2D.OverlapBox(pseudopositionXNeg, GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("PickUp", "Obstacles"));
        Collider2D ColYNeg = Physics2D.OverlapBox(pseudopositionYNeg, GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("PickUp", "Obstacles"));

        if (ColXPos != null && ColXPos != gameObject.GetComponent<BoxCollider2D>())
        {
            Debug.Log("Overlap Detected");
            pseudopositionXPos = new Vector3(pseudopositionXPos.x + 0.1f, pseudopositionXPos.y, pseudopositionXPos.z);
        }
        else
        {
            num += 1;
        }

        if (ColYPos != null && ColYPos != gameObject.GetComponent<BoxCollider2D>())
        {
            Debug.Log("Overlap Detected");
            pseudopositionYPos = new Vector3(pseudopositionYPos.x, pseudopositionYPos.y + 0.1f, pseudopositionYPos.z);
        }
        else { num += 1; }

        if (ColXNeg != null && ColXNeg != gameObject.GetComponent<BoxCollider2D>())
        {
            Debug.Log("Overlap Detected");
            pseudopositionXNeg = new Vector3(pseudopositionXNeg.x - 0.1f, pseudopositionXNeg.y, pseudopositionXNeg.z);
        }
        else { num += 1; }

        if (ColYNeg != null && ColYNeg != gameObject.GetComponent<BoxCollider2D>())
        {
            Debug.Log("Overlap Detected");
            pseudopositionYNeg = new Vector3(pseudopositionYNeg.x, pseudopositionYNeg.y - 0.1f, pseudopositionYNeg.z);
        }
        else 
        { 
            num += 1; 
        }

        if (num == 4)
        {
            
        }
            
    }
}
