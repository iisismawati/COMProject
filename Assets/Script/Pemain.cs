using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pemain : MonoBehaviour
{
    [SerializeField]
    public string ID_Pemain;

    public void MainkanGiliran()
    {
        Debug.Log("Pemain " + ID_Pemain + " Mulai Bermain");
    }
}
