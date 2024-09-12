using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPemain1 : MonoBehaviour
{
    [SerializeField]
    public Transform UbahGambarDaduPemain;

    [SerializeField]
    private List<Vector3> TempatDaduPemain = new List<Vector3>();

    [SerializeField]
    private AturanPemain aturanPemain;  // Referensi ke class AturanPemain


    private int nilaiDadu;  // Variabel untuk menyimpan nilai dadu

    public void AcakDaduPemain()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPemain.rotation = Quaternion.Euler(TempatDaduPemain[randomNumber - 1]);
        nilaiDadu = randomNumber - 1; // Simpan nilai dadu (0 hingga 5)

        // Setelah acak dadu, periksa hasilnya dengan aturan pemain
        aturanPemain.PeriksaHasilDadu();
    }

    private void OnMouseDown()
    {
        AcakDaduPemain();
    }

    // Fungsi untuk mendapatkan nilai dadu terakhir
    public int DapatkanNilaiDadu()
    {
        return nilaiDadu;
    }
}
