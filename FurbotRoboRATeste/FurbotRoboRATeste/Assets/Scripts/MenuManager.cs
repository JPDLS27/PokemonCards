using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IrParaBatalhaCiencia()
    {
        SceneManager.LoadScene("BatalhaCiencia");
    }

    public void IrParaFurchoque()
    {
        SceneManager.LoadScene("Furchoque");
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
