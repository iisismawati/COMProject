using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcakDadu : MonoBehaviour
{
    [SerializeField]
    public Image UbahGambarDaduPemain;

    [SerializeField]
    public Image UbahGambarDaduPodium;

    [SerializeField]
    private List<Sprite> TempatDaduPemain = new List<Sprite>();

    [SerializeField]
    private List<Sprite> TempatDaduPodium = new List<Sprite>();

    public void AcakDaduPemain()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPemain.sprite = TempatDaduPemain[randomNumber - 1];
    }

    public void AcakDaduPodium()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPodium.sprite = TempatDaduPodium[randomNumber - 1];
    }
}
