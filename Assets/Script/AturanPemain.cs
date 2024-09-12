using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AturanPemain : MonoBehaviour
{
    // Sama seperti sebelumnya
    [SerializeField]
    private RollPemain1 rollPemain;  // Referensi ke class RollPemain1 (dadu silver)
    [SerializeField]
    private RollPodium1 rollGold;    // Referensi ke class RollPodium1 (dadu gold)
    [SerializeField]
    private UrutanPemain urutanPemain;  // Referensi ke class UrutanPemain

    // Dictionary untuk menyimpan status dadu gold per pemain
    private Dictionary<string, bool> statusGoldDicePerPemain = new Dictionary<string, bool>();

    public void PeriksaHasilDadu()
    {
        string pemainAktif = urutanPemain.DapatkanPemainAktif(); // Dapatkan ID pemain aktif

        // Periksa apakah pemain ini menggunakan dadu gold atau silver
        bool isGoldDiceActive = statusGoldDicePerPemain.ContainsKey(pemainAktif) && statusGoldDicePerPemain[pemainAktif];

        if (!isGoldDiceActive)
        {
            // Aturan untuk dadu silver
            int hasilDadu = rollPemain.DapatkanNilaiDadu();  // Ambil nilai dadu dari RollPemain1 (dadu silver)

            if (hasilDadu == 0)
            {
                Debug.Log("Nilai dadu silver 1, skip giliran pemain.");
                urutanPemain.NextPemain(); // Skip giliran pemain
            }
            else if (hasilDadu == 1)
            {
                Debug.Log("Nilai dadu silver 2, dapatkan 1 kartu.");
                DapatkanKartu();
                urutanPemain.NextPemain();
            }
            else if (hasilDadu == 2)
            {
                Debug.Log("Nilai dadu silver 3, dapatkan 2 kartu.");
                DapatkanKartu();
                DapatkanKartu();
                urutanPemain.NextPemain();
            }
            else if (hasilDadu == 3 || hasilDadu == 4 || hasilDadu == 5)
            {
                Debug.Log("Nilai dadu silver 4,5, atau 6, tukar dadu ke gold.");
                AktifkanDaduGold(pemainAktif);  // Aktifkan dadu gold
            }
        }
        else
        {
            // Aturan untuk dadu gold (RollPodium1)
            int hasilGold = rollGold.DapatkanNilaiDadu(); // Ambil nilai dadu gold

            if (hasilGold == 0)
            {
                Debug.Log("Nilai dadu gold 1, dapatkan 1 kartu.");
                DapatkanKartu();
            }
            else if (hasilGold == 1)
            {
                Debug.Log("Nilai dadu gold 2, dapatkan 2 kartu.");
                DapatkanKartu();
                DapatkanKartu();
            }
            else if (hasilGold == 2)
            {
                Debug.Log("Nilai dadu gold 3, dapatkan 3 kartu.");
                DapatkanKartu();
                DapatkanKartu();
                DapatkanKartu();
            }
            else if (hasilGold == 3)
            {
                Debug.Log("Nilai dadu gold 4, tukar 1 kartu.");
                TukarKartu();
            }
            else if (hasilGold == 4)
            {
                Debug.Log("Nilai dadu gold 5, buang 1 kartu lawan.");
                BuangKartuLawan();
            }
            else if (hasilGold == 5)
            {
                Debug.Log("Nilai dadu gold 6, buang 1 kartu lawan dan dapatkan 1 kartu.");
                BuangKartuLawan();
                DapatkanKartu();
            }

            // Setelah pemain gold selesai, ubah status goldnya ke false agar kembali ke dadu silver di giliran berikutnya
            statusGoldDicePerPemain[pemainAktif] = false;
            rollGold.gameObject.SetActive(false);
            rollPemain.gameObject.SetActive(true);

            urutanPemain.NextPemain();  // Setelah aksi selesai, giliran pemain berikutnya
        }
    }

    // Fungsi untuk mengaktifkan dadu gold dan menyimpan statusnya
    private void AktifkanDaduGold(string pemainAktif)
    {
        // Set pemain ini untuk menggunakan dadu gold
        if (!statusGoldDicePerPemain.ContainsKey(pemainAktif))
        {
            statusGoldDicePerPemain.Add(pemainAktif, true);
        }
        else
        {
            statusGoldDicePerPemain[pemainAktif] = true;
        }

        rollPemain.gameObject.SetActive(false);  // Nonaktifkan dadu silver
        rollGold.gameObject.SetActive(true);     // Aktifkan dadu gold
        Debug.Log("Dadu gold aktif untuk pemain: " + pemainAktif);
    }

    // Fungsi lain seperti DapatkanKartu, TukarKartu, dan BuangKartuLawan tetap sama



    private void DapatkanKartu()
    {
        Debug.Log("Kamu dapat kartu!");
        // Logika pemberian kartu ke pemain
    }

    private void TukarKartu()
    {
        Debug.Log("Tukar 1 kartu dengan pemain lain!");
        // Logika untuk menukar kartu dengan pemain lain
    }

    private void BuangKartuLawan()
    {
        Debug.Log("Buang 1 kartu lawan!");
        // Logika untuk membuang kartu lawan
    }
}
