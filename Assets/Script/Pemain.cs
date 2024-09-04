using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pemain : MonoBehaviour
{
    [SerializeField]
    public string ID_Pemain;

    // Daftar objek pemain lainnya yang harus diaktifkan/nonaktifkan
    //[SerializeField]
    //private List<GameObject> pemainList;

    public void MainkanGiliran()
    {
        Debug.Log("Pemain " + ID_Pemain + " Mulai Bermain");
        // Aktifkan atau nonaktifkan objek berdasarkan giliran pemain
        //UpdatePemainList();
    }

    // Method untuk mengaktifkan game object berdasarkan giliran pemain
    //private void UpdatePemainList()
    //{
    //    foreach (GameObject pemain in pemainList)
    //    {
    //        if (pemain != null)
    //        {
    //            Pemain pemainComponent = pemain.GetComponent<Pemain>();
    //            if (pemainComponent != null)
    //            {
    //                // Nonaktifkan pemain lain
    //                pemain.SetActive(false);
    //            }
    //        }
    //    }
    //}
}
