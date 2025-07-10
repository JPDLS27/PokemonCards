using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject setaSul;
    public GameObject setaOeste;
    public GameObject setaLeste;
    public GameObject setaNorte;
    private void EsconderTodasAsSetas()
    {
        setaNorte.SetActive(false);
        setaSul.SetActive(false);
        setaOeste.SetActive(false);
        setaLeste.SetActive(false);
    }

    public void OnClickUp()
    {
        player.MoveUp();
        EsconderTodasAsSetas();
        setaNorte.SetActive(true);
    }

    public void OnClickDown()
    {
        player.MoveDown();
        EsconderTodasAsSetas();
        setaSul.SetActive(true);
    }

    public void OnClickLeft()
    {
        player.MoveLeft();
        EsconderTodasAsSetas();
        setaOeste.SetActive(true);
    }

    public void OnClickRight()
    {
        player.MoveRight();
        EsconderTodasAsSetas();
        setaLeste.SetActive(true);
    }

}
