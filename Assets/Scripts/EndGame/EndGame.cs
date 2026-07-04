using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;
    private bool _endGame = false;
    public int currentLevel = 1;

    void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }
    void OnTriggerEnter(Collider other)
    {
        var player = other.transform.GetComponent<Player>();
        if (!_endGame && player != null)
        {
            ShowEndGame();
        }
    }

    void ShowEndGame()
    {
        //endGameObjects.ForEach(i => i.SetActive(true));
        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }
        SaveManager.Instance.SaveLastLevel(currentLevel);
        SaveManager.Instance.SaveItems();
    }
}
