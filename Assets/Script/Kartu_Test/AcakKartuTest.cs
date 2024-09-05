using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //Merubah Text Pada Dek Menjadi Sesuai Nilai Pada Stack TumpukKartu
        TextJumlahKartuTest.text = TumpukKartuTest.Count.ToString();
        Debug.Log(TumpukKartuTest.Count);
    }
    public void GantiTextDek()
    {
        TextJumlahKartuTest.text = TumpukKartuTest.Count.ToString();
    }

    public void Start()
    {
        TumpukKTest();
        GantiTextDek();
    }

    
}
