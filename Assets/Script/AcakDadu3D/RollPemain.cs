using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPemain : MonoBehaviour
{
    [SerializeField]
    public Transform UbahGambarDaduPemain;

    [SerializeField]
    private List<Vector3> TempatDaduPemain = new List<Vector3>();

    public void AcakDaduPemain()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPemain.rotation = Quaternion.Euler(TempatDaduPemain[randomNumber - 1]);
    }
   
    private void OnMouseDown()
    {
        AcakDaduPemain();
    }
}
