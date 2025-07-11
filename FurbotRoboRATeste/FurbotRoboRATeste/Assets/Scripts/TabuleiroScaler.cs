using UnityEngine;

public class TabuleiroScaler : MonoBehaviour
{
    [Header("Referência ao plano virtual do tabuleiro")]
    public GameObject planoTabuleiro; // Arraste o plano quadriculado aqui

    [Header("Tamanho do tabuleiro virtual (em metros)")]
    public float tamanhoX = 1.0f; // Largura (horizontal, eixo X)
    public float tamanhoZ = 1.0f; // Comprimento (profundidade, eixo Z)

    [Header("Altura do tabuleiro virtual (levemente acima do marcador)")]
    public float alturaY = 0.01f;

    void Start()
    {
        if (planoTabuleiro == null)
        {
            Debug.LogWarning("Plano do tabuleiro não atribuído no TabuleiroScaler!");
            return;
        }

        // Ajusta a escala do plano com base nos valores fornecidos
        planoTabuleiro.transform.localScale = new Vector3(tamanhoX, 1f, tamanhoZ);

        // Reposiciona o plano para crescer a partir do marcador no canto
        planoTabuleiro.transform.localPosition = new Vector3(tamanhoX / 2f, alturaY, tamanhoZ / 2f);
    }
}
