using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrutanPemain : MonoBehaviour
{
    //Menyimpan List Player yang ada dalam Game
    [SerializeField]
    private List <Pemain> ListPemain = new List<Pemain>();

    //List Urutan Dari Player
    [SerializeField]
    private List<string> ListUrutanPemain = new List<string>();

    //index Giliran Pemain
    private int PemainSelanjutnya;


    // Function Giliran Selanjutnya
    
    public void NextPemain()
    {
        //Index Giliran Pemain Harus Ditambah
        if (PemainSelanjutnya >= ListUrutanPemain.Count - 1)
        {
            PemainSelanjutnya = 0;
        }
        else
        {
            PemainSelanjutnya = PemainSelanjutnya + 1;
        }


        //Memanggil Function Mainkan Giliran dari player dengan index pemain yang sedang bemain
        Pemain player = ListPemain.Find(player => player.ID_Pemain == ListUrutanPemain[PemainSelanjutnya]);
        player.MainkanGiliran();

    }
}
