using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPodium1 : MonoBehaviour
{
    [SerializeField]
    public Transform UbahGambarDaduPemain;

    [SerializeField]
    private List<Vector3> TempatDaduPemain = new List<Vector3>();

    [SerializeField]
    private AturanPemain aturanPemain;  // Referensi ke class AturanPemain

    private int nilaiDadu;

    // Fungsi untuk mengacak dadu gold
    public void AcakDaduPemain()
    {
        int randomNumber = Random.Range(1, 7);
        UbahGambarDaduPemain.rotation = Quaternion.Euler(TempatDaduPemain[randomNumber - 1]);
        nilaiDadu = randomNumber - 1; // Simpan nilai dadu (0 hingga 5)
        //Debug.Log("Nilai dadu gold diacak: " + nilaiDadu);
    }

    // Fungsi untuk mendeteksi klik pada dadu dan mengacaknya
    private void OnMouseDown()
    {
        AcakDaduPemain();  // Acak dadu gold
        aturanPemain.PeriksaHasilDadu();  // Periksa hasil setelah dadu diacak
    }

    // Mengambil nilai dadu gold yang telah diacak
    public int DapatkanNilaiDadu()
    {
        //Debug.Log("Nilai dadu gold diambil: " + nilaiDadu);
        return nilaiDadu;
    }
}
