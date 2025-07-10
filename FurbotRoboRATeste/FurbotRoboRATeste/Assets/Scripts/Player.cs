using UnityEngine;

public class Player : MonoBehaviour
{
    public float blockSize = 0.1f; // Tamanho do bloco no plano
    public float moveSpeed = 1.0f; // Velocidade de transição

    private bool isMoving = false;
    private Vector3 targetLocalPosition;

    private void Start()
    {
        // Armazena a posição local inicial
        targetLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.localPosition, targetLocalPosition) < 0.001f)
            {
                transform.localPosition = targetLocalPosition; // Garante alinhamento perfeito
                isMoving = false;
            }
        }
    }

    public void MoveUp()
    {
        if (!isMoving)
        {
            targetLocalPosition += new Vector3(0, 0, blockSize);
            isMoving = true;
        }
    }

    public void MoveDown()
    {
        if (!isMoving)
        {
            targetLocalPosition += new Vector3(0, 0, -blockSize);
            isMoving = true;
        }
    }

    public void MoveLeft()
    {
        if (!isMoving)
        {
            targetLocalPosition += new Vector3(-blockSize, 0, 0);
            isMoving = true;
        }
    }

    public void MoveRight()
    {
        if (!isMoving)
        {
            targetLocalPosition += new Vector3(blockSize, 0, 0);
            isMoving = true;
        }
    }
}
