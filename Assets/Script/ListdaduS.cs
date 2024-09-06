using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListDaduS : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> dadu = new List<Sprite>(); // List untuk menyimpan gambar-gambar dadu

    [SerializeField]
    private Image gambarDadu; // Referensi ke UI Image yang akan menampilkan sprite dadu

    [SerializeField]
    private AturanPemain aturanPemain; // Referensi ke class AturanPemain

    private int hasilDadu; // Variabel untuk menyimpan hasil acak dadu

    // Fungsi untuk mengacak dadu
    public void AcakDadu()
    {
        hasilDadu = Random.Range(0, dadu.Count); // Menghasilkan nilai acak sesuai jumlah sprite di list
        gambarDadu.sprite = dadu[hasilDadu]; // Mengganti sprite gambar dadu sesuai dengan hasil acak

        // Setelah dadu diacak, panggil fungsi PeriksaHasilDadu di AturanPemain
        //aturanPemain.PeriksaHasilDadu(hasilDadu); // Kirimkan hasil dadu untuk diperiksa
    }

    // Fungsi untuk memulai pengacakan dadu saat tombol ditekan
    public void OnRollButtonClick()
    {
        AcakDadu();
    }
}
