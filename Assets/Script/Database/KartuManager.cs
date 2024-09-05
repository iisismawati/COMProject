using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KartuManager : MonoBehaviour
{
    [SerializeField]
    private KartuSO kartuDatabase;

    private List<DBKartu> kartu = new List<DBKartu> ();
    //public List<DBKartu> KartuTampil;

    private Stack<DBKartu> TumpukKartuTest = new Stack<DBKartu>();

    [SerializeField]
    private TMP_Text TextJumlahKartuTest;

    //variabel void yang bisa digunakan class lain
    public int randomKTest;
    //public int numberOfPlayers;
    //public int cardsPerPlayer;

    public void RandomKartuTest()
    {
        randomKTest = Random.Range(0, kartu.Count);
    }
    void TumpukKTest()
    {

        while (TumpukKartuTest.Count < kartu.Count)
        {
            RandomKartuTest();
            //Cek Kartu Sudah Ada atau Belum 
            //K1.KodeKartu
            bool KartuSudahAda = TumpukKartuTest.Contains(kartu[randomKTest]);
            //Jalankan Jika Tidak Ada (Agar Tidak Duplikat)
            if (!KartuSudahAda)
            {
                TumpukKartuTest.Push(kartu[randomKTest]);
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

    private void Start()
    {
        for (int i = 0; i < kartuDatabase.kartu.Count; i++)
        {
            kartu.Add(kartuDatabase.kartu[i]);
        }
        TumpukKTest();
        GantiTextDek();
    }
    void DisplayCardImage(Sprite sprite)
    {
        // Misalnya, menampilkan sprite pada sebuah UI Image
        GameObject cardImageObject = GameObject.Find("CardImageUI");
        Image cardImage = cardImageObject.GetComponent<Image>();
        cardImage.sprite = sprite;
    }

    public void BagiKartu()
    {
        DisplayCardImage(kartu[0].GambarKartu);

    }
}
