using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AturanPemain : MonoBehaviour
{
    [SerializeField]
    private RollPemain1 rollPemain;  // Referensi ke class RollPemain1
    [SerializeField]
    private UrutanPemain urutanPemain;  // Referensi ke class UrutanPemain

    // Fungsi untuk memeriksa hasil dadu dan melakukan aksi
    public void PeriksaHasilDadu()
    {
        // Ambil nilai dadu dari RollPemain1
        int hasilDadu = rollPemain.DapatkanNilaiDadu();  // Memanggil fungsi untuk mendapatkan nilai dadu

        if (hasilDadu == 0)
        {
            Debug.Log("Nilai dadu 1, skip giliran pemain.");
            urutanPemain.NextPemain(); // Skip giliran pemain, langsung ke pemain berikutnya
        }
        else if (hasilDadu == 1)
        {
            Debug.Log("Nilai dadu 2, dapatkan 1 kartu.");
            DapatkanKartu(); // Berikan kartu ke pemain
            urutanPemain.NextPemain();
        }
        else if (hasilDadu == 2)
        {
            Debug.Log("Nilai dadu 3, dapatkan 2 kartu.");
            DapatkanKartu(); // Berikan kartu ke pemain
            urutanPemain.NextPemain();
        }
        else if (hasilDadu == 3)
        {
            Debug.Log("Nilai dadu 4, podium dan tukar/tambah kartu.");
            // Tambahkan logika untuk podium atau tukar/tambah kartu
            urutanPemain.NextPemain();
        }
        else if (hasilDadu == 4)
        {
            Debug.Log("Nilai dadu 5, tukar kartu.");
            // Tambahkan logika untuk tukar kartu
            urutanPemain.NextPemain();
        }
        else if (hasilDadu == 5)
        {
            Debug.Log("Nilai dadu 6, Buang Kartu Lawan.");
            DapatkanKartu(); // Berikan kartu ke pemain
            urutanPemain.NextPemain();
        }
        else
        {
            Debug.Log("Nilai dadu tidak valid.");
        }
    }

    private void DapatkanKartu()
    {
        // Logika untuk memberikan kartu ke pemain
        Debug.Log("Kamu dapat kartu!");
        // Tambahkan logika sesuai dengan sistem permainanmu
    }
}
