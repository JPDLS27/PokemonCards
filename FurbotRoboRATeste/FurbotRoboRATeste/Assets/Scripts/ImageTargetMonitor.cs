using System.Collections;
using UnityEngine;
using Vuforia;

public class ImageTargetMonitor : MonoBehaviour
{
    public GameObject tabuleiroVirtual;   // O plano ou container com os objetos do jogo
    public GameObject furbot;             // Referência ao Furbot
    public GameObject marcador;           // Objeto ImageTarget
    
    private bool isTracking = true;
    private Transform originalParent;
    private Vector3 ultimaPosicao;
    private Quaternion ultimaRotacao;

    void Start()
    {
        if (tabuleiroVirtual == null || furbot == null || marcador == null)
        {
            Debug.LogWarning("Faltando referências no ImageTargetMonitor!");
            return;
        }

        originalParent = tabuleiroVirtual.transform.parent;
        StartCoroutine(VerificarMarcador());
    }

    IEnumerator VerificarMarcador()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            var observerBehaviour = marcador.GetComponent<ObserverBehaviour>();
            if (observerBehaviour == null) continue;

            var status = observerBehaviour.TargetStatus.Status;

            // Caso o rastreamento seja perdido
            if ((status != Status.TRACKED && status != Status.EXTENDED_TRACKED) && isTracking)
            {
                isTracking = false;

                ultimaPosicao = tabuleiroVirtual.transform.position;
                ultimaRotacao = tabuleiroVirtual.transform.rotation;

                tabuleiroVirtual.transform.parent = null;

                Debug.Log("Marcador perdido. Tabuleiro mantido na última posição.");
            }
            else if ((status == Status.TRACKED || status == Status.EXTENDED_TRACKED) && !isTracking)
            {
                isTracking = true;

                tabuleiroVirtual.transform.parent = originalParent;
                tabuleiroVirtual.transform.position = ultimaPosicao;
                tabuleiroVirtual.transform.rotation = ultimaRotacao;

                Debug.Log("Marcador reencontrado. Tabuleiro reapontado.");
            }
        }
    }
}
