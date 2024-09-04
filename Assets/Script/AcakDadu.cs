using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcakDadu : MonoBehaviour
{
    [SerializeField]
    //public Image UbahGambarDaduPemain;
    public Transform UbahGambarDaduPemain;

    [SerializeField]
    //public Image UbahGambarDaduPodium;
    public Transform UbahGambarDaduPodium;

    [SerializeField]
    //private List<Sprite> TempatDaduPemain = new List<Sprite>();
    private List<Vector3> TempatDaduPemain = new List<Vector3>();

    [SerializeField]
    //private List<Sprite> TempatDaduPodium = new List<Sprite>();
    private List<Vector3> TempatDaduPodium = new List<Vector3>();

    public void AcakDaduPemain()
    {
        int randomNumber = Random.Range(1, 7);
        //--UbahGambarDaduPemain.sprite = TempatDaduPemain[randomNumber - 1];
        UbahGambarDaduPemain.rotation = Quaternion.Euler(TempatDaduPemain[randomNumber - 1]);
    }

    public void AcakDaduPodium()
    {
        int randomNumber = Random.Range(1, 7);
        // UbahGambarDaduPodium.sprite = TempatDaduPodium[randomNumber - 1];
        UbahGambarDaduPodium.rotation = Quaternion.Euler(TempatDaduPodium[randomNumber - 1]);
    }
    private void OnMouseDown()
    {
        Debug.Log("Object Clicked");
        AcakDaduPemain();
        AcakDaduPodium();
    }
}
