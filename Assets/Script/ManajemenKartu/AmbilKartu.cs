using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmbilKartu : MonoBehaviour
{
    [SerializeField]
    private KartuSO kartuDatabase;  // Referensi ke ScriptableObject database kartu

    [SerializeField]
    public IniPemainnya activePlayer1;    // Referensi ke pemain yang aktif

    [SerializeField]
    public IniPemainnya activePlayer2;

    [SerializeField]
    public IniPemainnya activePlayer3;

    [SerializeField]
    public IniPemainnya activePlayer4;

    [SerializeField]
    private Image KartuDiTangan1;

    [SerializeField]
    private Image KartuDiTangan2;

    [SerializeField]
    private Image KartuDiTangan3;

    [SerializeField]
    private Image KartuDiTangan4;

    [SerializeField]
    private Image KartuDiDeck;

    [SerializeField]
    private TMP_Text SisaKartu;

    [SerializeField]
    private TMP_Text pesanAwalText;  // TMP Text untuk menampilkan pesan "Permainan Dimulai"

    private Stack<Sprite> StackCangkulan = new Stack<Sprite>();

    // Start is called before the first frame update

    void RandomKartuTest()
    {
        // Buat list lokal untuk menampung kartu dari database yang sesuai dengan jurusan pemain aktif
        List<Sprite> kartuList = new List<Sprite>();

        // Dapatkan jurusan pemain aktif dalam sebuah list
        List<string> jurusanAktifList = new List<string>();
        jurusanAktifList.Add(activePlayer1.Jurusan);
        jurusanAktifList.Add(activePlayer2.Jurusan);
        jurusanAktifList.Add(activePlayer3.Jurusan);
        jurusanAktifList.Add(activePlayer4.Jurusan);

        // Ambil kartu dari database yang memiliki JurKartu sesuai dengan Jurusan pemain aktif
        for (int i = 0; i < kartuDatabase.KartuList.Count; i++)
        {
            // Cek jika JurKartu cocok dengan salah satu jurusan pemain aktif
            if (jurusanAktifList.Contains(kartuDatabase.KartuList[i].JurKartu))
            {
                kartuList.Add(kartuDatabase.KartuList[i].GbrKartu);
            }
        }

        // Acak urutan kartu menggunakan Random.Range
        while (kartuList.Count > 0)
        {
            int randomIndex = Random.Range(0, kartuList.Count);
            Sprite kartuTerpilih = kartuList[randomIndex];
            StackCangkulan.Push(kartuTerpilih);
            kartuList.RemoveAt(randomIndex);
        }

    }

    // Function mengambil kartu langsung tanpa tombol
    private void BagiKartu()
    {
        if (StackCangkulan.Count >= 4)
        {
            Sprite simpanKartu1 = StackCangkulan.Pop();
            KartuDiTangan1.sprite = simpanKartu1;
            KartuDiTangan1.gameObject.SetActive(true);

            Sprite simpanKartu2 = StackCangkulan.Pop();
            KartuDiTangan2.sprite = simpanKartu2;
            KartuDiTangan2.gameObject.SetActive(true);

            Sprite simpanKartu3 = StackCangkulan.Pop();
            KartuDiTangan3.sprite = simpanKartu3;
            KartuDiTangan3.gameObject.SetActive(true);

            Sprite simpanKartu4 = StackCangkulan.Pop();
            KartuDiTangan4.sprite = simpanKartu4;
            KartuDiTangan4.gameObject.SetActive(true);

            SisaKartu.text = StackCangkulan.Count.ToString();  
        }
    }


    void Start()
    {
        

        RandomKartuTest();

        SisaKartu.text = StackCangkulan.Count.ToString();

        // Memulai coroutine untuk menampilkan pesan dan ambil kartu
        StartCoroutine(TampilkanPesanAwalDanAmbilKartu());

    }

    // Coroutine untuk menampilkan dua pesan dengan jeda waktu, kemudian ambil kartu
    private IEnumerator TampilkanPesanAwalDanAmbilKartu()
    {
        // Tampilkan pesan pertama
        pesanAwalText.text = "Permainan Dimulai\nSetiap Pemain Mendapatkan Modal Bermain 1 Kartu";
        pesanAwalText.gameObject.SetActive(true);  // Aktifkan teks agar terlihat

        // Tunggu 3 detik untuk pesan pertama
        yield return new WaitForSeconds(3f);

        // Sembunyikan pesan pertama
        pesanAwalText.gameObject.SetActive(false);

        // Tampilkan pesan kedua "CEKIDOT!!!"
        pesanAwalText.text = "PEMAIN 1 MULAI!!!";
        pesanAwalText.gameObject.SetActive(true);

        // Tunggu 2 detik untuk pesan kedua
        yield return new WaitForSeconds(2f);

        // Sembunyikan pesan kedua
        pesanAwalText.gameObject.SetActive(false);

        // Lanjutkan ke fungsi untuk mengambil kartu
        BagiKartu();
    }

   void AmbilKartuPermainan()
    {


    }

    void BuangKartu()
    {


    }

    void TukarKartu()
    {


    }
    
}
