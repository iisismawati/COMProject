using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AcakKartuTest : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> AmbilKartuTest = new List<Sprite>();

    [SerializeField]
    private List<Sprite> Uji = new List<Sprite>();

    private Stack<Sprite> TumpukKartuTest = new Stack<Sprite>();

    [SerializeField]
    private TMP_Text TextJumlahKartuTest;

    //Untuk Bagi Kartu Pemain Masing-masing 1
   
    [SerializeField]
    //private List<Sprite> GambarBagiKartu = new List<Sprite>();
    private Image GambarBagiKartu1;

    [SerializeField]
    private Image GambarBagiKartu2;

    [SerializeField]
    private Image GambarBagiKartu3;

    [SerializeField]
    private Image GambarBagiKartu4;

    [SerializeField]
    private Image KartuDek;

    //variabel void yang bisa digunakan class lain
    public int randomKTest;

    public void RandomKartuTest()
    {
        randomKTest = Random.Range(0, AmbilKartuTest.Count);
    }
    void TumpukKTest()
    {  
        while (TumpukKartuTest.Count<AmbilKartuTest.Count)
        {
            RandomKartuTest();
            //Cek Kartu Sudah Ada atau Belum 
            //K1.KodeKartu
            bool KartuSudahAda = TumpukKartuTest.Contains(AmbilKartuTest[randomKTest]);
            //Jalankan Jika Tidak Ada (Agar Tidak Duplikat)
            if (!KartuSudahAda)
            {
                TumpukKartuTest.Push(AmbilKartuTest[randomKTest]);
            }
            
        }

        while (Uji.Count < AmbilKartuTest.Count)
        {
            RandomKartuTest();
            //Cek Kartu Sudah Ada atau Belum 
            //K1.KodeKartu
            bool KartuSudahAda = Uji.Exists(K1 => K1 == AmbilKartuTest[randomKTest]);
            //Jalankan Jika Tidak Ada (Agar Tidak Duplikat)
            if (!KartuSudahAda)
            {
                Uji.Add(AmbilKartuTest[randomKTest]);
            }

        } 
    }
    
    //Merubah Text Pada Dek Menjadi Sesuai Nilai Pada Stack TumpukKartu
    public void GantiTextDek()
    {
        TextJumlahKartuTest.text = TumpukKartuTest.Count.ToString();
    }

   
    public void Start()
    {
        TumpukKTest();
        GantiTextDek();
    }
    public void BagiKartu()
    {
        
        Sprite SimpanKartu1 = TumpukKartuTest.Pop();
        GambarBagiKartu1.sprite = SimpanKartu1;
        GambarBagiKartu1.gameObject.SetActive(true);

        Sprite SimpanKartu2 = TumpukKartuTest.Pop();
        GambarBagiKartu2.sprite = SimpanKartu2;
        GambarBagiKartu2.gameObject.SetActive(true);

        Sprite SimpanKartu3 = TumpukKartuTest.Pop();
        GambarBagiKartu3.sprite = SimpanKartu3;
        GambarBagiKartu3.gameObject.SetActive(true);

        Sprite SimpanKartu4 = TumpukKartuTest.Pop();
        GambarBagiKartu4.sprite = SimpanKartu4;
        GambarBagiKartu4.gameObject.SetActive(true);
       
        // KartuDek.gameObject.SetActive(false);

        if (TumpukKartuTest.Count <= 0)
        {
            //TombolAmbilKartu.interactable = false;
            Debug.Log("Kartu Habis");
            
        }
        GantiTextDek();
    }

}
