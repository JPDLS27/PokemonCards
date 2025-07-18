using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    public float blockSizeY = 1.0f;
    public float blockSizeX = 0.7f;
    public float moveSpeed = 1.0f;

    private bool isMoving = false;
    private Vector3 targetLocalPosition;
    private Vector3 lastLocalPosition;
    private bool reverting = false;

    [Header("Status")]

    [SerializeField]
    private bool isElectrified = false;
    
    [SerializeField]
    private bool isWet = false;

    private void Start()
    {
        targetLocalPosition = transform.localPosition;
        lastLocalPosition = targetLocalPosition;
    }

    private void Update()
    {
        HandleMovement();
    }

    #region Movimentação

    public void MoveUp() => TryMove(Vector3.forward);
    public void MoveDown() => TryMove(Vector3.back);
    public void MoveLeft() => TryMove(Vector3.left);
    public void MoveRight() => TryMove(Vector3.right);

    private void TryMove(Vector3 worldDirection)
    {
        if (isMoving || reverting) return;

        lastLocalPosition = targetLocalPosition;

        Vector3 localDirection = transform.parent.InverseTransformDirection(worldDirection);
        localDirection.x *= blockSizeX;
        localDirection.z *= blockSizeY;

        targetLocalPosition += localDirection;
        isMoving = true;
    }

    private void HandleMovement()
    {
        if (!isMoving && !reverting) return;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, targetLocalPosition) < 0.001f)
        {
            transform.localPosition = targetLocalPosition;

            if (reverting)
            {
                reverting = false;
                Debug.Log("Retornou à posição anterior.");
            }

            isMoving = false;
        }
    }

    #endregion

    #region Status

    private void Electrify()
    {
        isElectrified = true;
        Debug.Log("Furbot eletrificado!");
    }

    private void Wet()
    {
        isWet = true;
        Debug.Log("Furbot molhado!");
    }

    private void ResetStatus()
    {
        isElectrified = false;
        isWet = false;
        Debug.Log("Status resetado.");
    }

    private bool IsDead() => isElectrified && isWet;

    #endregion

    #region Colisões

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Raio":
                Electrify();
                break;

            case "Agua":
                Wet();
                break;

            case "Conversao":
                ResetStatus();
                break;

            case "Moeda":
                Debug.Log("Fim de jogo!");
                break;

            case "Muro":
                Debug.Log("Colidiu com o muro!");
                CancelMoveAndRevert();
                break;
        }

        if (IsDead())
        {
            Debug.Log("GAME OVER: Molhado + Eletrificado!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void CancelMoveAndRevert()
    {
        // Volta para a última posição válida
        targetLocalPosition = lastLocalPosition;
        isMoving = true;
        reverting = true;
    }

    #endregion
}
