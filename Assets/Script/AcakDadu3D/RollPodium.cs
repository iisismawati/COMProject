using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPodium : MonoBehaviour
{
    [SerializeField]
    public Transform UbahGambarDaduPodium;

    [SerializeField]
    private List<Vector3> TempatDaduPodium = new List<Vector3>();
 
    public void AcakDaduPodium()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPodium.rotation = Quaternion.Euler(TempatDaduPodium[randomNumber - 1]);
    }
    private void OnMouseDown()
    {
        AcakDaduPodium();
    }
}
