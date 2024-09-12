using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrutanPemain : MonoBehaviour
{
    [SerializeField]
    private List<Pemain> ListPemain = new List<Pemain>();

    [SerializeField]
    private List<string> ListUrutanPemain = new List<string>();

    private int PemainSelanjutnya = 0;

    // Method untuk mendapatkan ID pemain yang saat ini aktif
    public string DapatkanPemainAktif()
    {
        return ListUrutanPemain[PemainSelanjutnya];
    }

    public void NextPemain()
    {
        // Index giliran pemain harus ditambah
        if (PemainSelanjutnya >= ListUrutanPemain.Count - 1)
        {
            PemainSelanjutnya = 0;
        }
        else
        {
            PemainSelanjutnya++;
        }

        // Mengaktifkan pemain yang sesuai dan mematikan yang lainnya
        for (int i = 0; i < ListPemain.Count; i++)
        {
            Pemain pemain = ListPemain[i];
            if (pemain.ID_Pemain == ListUrutanPemain[PemainSelanjutnya])
            {
                pemain.gameObject.SetActive(true);
                pemain.MainkanGiliran(); // Memanggil fungsi MainkanGiliran
            }
            else
            {
                pemain.gameObject.SetActive(false);
            }
        }
    }
}
