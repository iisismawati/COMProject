using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.Reflection;
using UnityEngine.SocialPlatforms.Impl;

public class Pemainnya : MonoBehaviour
{
    public DataPemain playerData;  // Menyimpan data pemain yang diassign
    public int lastDiceResult;     // Menyimpan hasil dadu terakhir

    // UI untuk menampilkan jurusan pemain
    public TMP_Text jurusanText;

    public GameObject PionPemain;  // Referensi ke objek pion pemain

    // UI untuk menampilkan kartu di meja utama
    public Button[] tableCardButtons = new Button[3];  // Slot kartu di meja utama

    // UI untuk menampilkan kartu di meja tambahan
    public Button[] additionalTableCardButtons = new Button[3];  // Slot kartu di meja tambahan

    public List<Kartunya> tableCards = new List<Kartunya>();  // Kartu di meja utama
    public List<Kartunya> additionalTableCards = new List<Kartunya>();  // Kartu di meja tambahan

    [SerializeField]
    private List<Pemainnya> PemainLawan = new List<Pemainnya>();

    private GameManager gameManager;

    [SerializeField]
    private Image DekKartuBuang;  // Untuk Menampilkan Kartu yang Dibuang

    private bool isChoosingToDiscard = false;  // Variabel untuk melacak apakah pemain sedang memilih kartu untuk dibuang
    //private bool isChoosingToDiscardLawan = false;
    private Pemainnya selectedOpponent;  // Menyimpan pemain lawan yang dipilih
    public int Score;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Dapatkan referensi ke GameManager
        AddCardButtonListeners();  // Pasang listener pada kartu
    }

    // Tambahkan listener untuk klik pada setiap kartu
    void AddCardButtonListeners()
    {
        for (int i = 0; i < tableCardButtons.Length; i++)
        {
            int index = i;  // Variabel lokal untuk digunakan dalam lambda 
            tableCardButtons[i].onClick.AddListener(() => OnCardClicked(index, true));
        }

        for (int i = 0; i < additionalTableCardButtons.Length; i++)
        {
            int index = i;  // Variabel lokal untuk digunakan dalam lambda
            additionalTableCardButtons[i].onClick.AddListener(() => OnCardClicked(index, false));
        }
    }

    // Fungsi ini dipanggil ketika kartu di meja diklik
    void OnCardClicked(int index, bool isMainTable)
    {
        if (isChoosingToDiscard)
        {
            // Logika untuk membuang kartu sendiri
            if (isMainTable && index < tableCards.Count)
            {
                Kartunya card = tableCards[index];
                Debug.Log("Kartu di meja utama yang dibuang: " + card.IDKartu);
                DiscardCardFromTable(index, true);  // Buang kartu dari meja utama
            }
            else if (index < additionalTableCards.Count)
            {
                Kartunya card = additionalTableCards[index];
                Debug.Log("Kartu di meja tambahan yang dibuang: " + card.IDKartu);
                DiscardCardFromTable(index, false);  // Buang kartu dari meja tambahan
            }

            // Pindahkan kartu dari meja tambahan ke meja utama jika diperlukan
            if (additionalTableCards.Count > 0 && tableCards.Count < 3)
            {
                MoveCardFromAdditionalToMainTable();
            }
        }
        else
        {
            Debug.Log("Tidak ada tindakan yang diizinkan saat ini.");
        }
    }
    
    public void SetPlayerData(DataPemain dataPemain)
    {
        playerData = dataPemain;
        UpdatePlayerUI();
    }

    public void UpdatePlayerUI()
    {
        if (jurusanText != null && playerData != null)
        {
            jurusanText.text = "Jurusan: " + playerData.Jurusan;
        }
    }

    public void ReceiveStartingCardToTable(Kartunya card)
    {
        if (tableCards.Count < 3)
        {
            // Jika meja utama belum penuh, tambahkan kartu ke meja utama
            tableCards.Add(card);
            Debug.Log(playerData.Jurusan + " menerima kartu di meja utama: " + card.JurKartu + card.NilaiKartu);
        }
        else if (additionalTableCards.Count < 3)
        {
            // Jika meja utama penuh, tambahkan kartu ke meja tambahan
            additionalTableCards.Add(card);
            Debug.Log(playerData.Jurusan + " menerima kartu di meja tambahan: " + card.JurKartu + card.NilaiKartu);

            // Aktifkan mode pembuangan kartu karena meja tambahan terisi
            isChoosingToDiscard = true;
        }
        else
        {
            // Jika meja utama dan tambahan penuh, pemain harus memilih kartu untuk dibuang
            Debug.Log("Pemain harus memilih kartu untuk dibuang.");
            isChoosingToDiscard = true;  // Aktifkan mode memilih kartu untuk dibuang
        }

        UpdateTableUI();
    }

    // Fungsi untuk membuang kartu
    public void DiscardCardFromTable(int index, bool isMainTable)
    {
        if (isMainTable)
        {
            if (index < tableCards.Count)
            {
                Sprite SimpanKartuBuang = tableCards.ElementAt(index).GbrKartu;
                DekKartuBuang.sprite = SimpanKartuBuang;
                DekKartuBuang.gameObject.SetActive(true);
                tableCards.RemoveAt(index);  // Menghapus kartu dari list meja utama
                Debug.Log("Kartu di meja utama dihapus.");
            }
        }
        else
        {
            if (index < additionalTableCards.Count)
            {
                Sprite SimpanKartuBuang2 = additionalTableCards.ElementAt(index).GbrKartu;
                DekKartuBuang.sprite = SimpanKartuBuang2;
                DekKartuBuang.gameObject.SetActive(true);
                additionalTableCards.RemoveAt(index);  // Menghapus kartu dari list meja tambahan
                Debug.Log("Kartu di meja tambahan dihapus.");
            }
        }

        // Hitung jumlah total kartu yang harus dibuang
        int totalCards = tableCards.Count + additionalTableCards.Count;
        int cardsToDiscard = totalCards - 3;

        // Setelah kartu dibuang, cek apakah pemain masih harus membuang kartu
        if (cardsToDiscard > 0)
        {
            isChoosingToDiscard = true;  // Pemain masih harus membuang kartu
            Debug.Log("Pemain masih harus membuang " + cardsToDiscard + " kartu.");
        }
        else
        {
            isChoosingToDiscard = false;  // Pemain tidak perlu membuang kartu lagi
            gameManager.EndTurn();  // Panggil EndTurn untuk berpindah ke pemain berikutnya
            gameManager.EnableRollDice();  // Aktifkan fungsi roll dice di GameManager
            Debug.Log("Pemain selesai membuang kartu. Giliran berpindah.");
        }

        // Perbarui UI setelah kartu dibuang
        UpdateTableUI();
    }

    // Pindahkan kartu dari meja tambahan ke meja utama
    void MoveCardFromAdditionalToMainTable()
    {
        if (additionalTableCards.Count > 0 && tableCards.Count < 3)
        {
            Kartunya cardToMove = additionalTableCards[0];  // Ambil kartu pertama dari meja tambahan
            additionalTableCards.RemoveAt(0);  // Hapus dari meja tambahan
            tableCards.Add(cardToMove);  // Tambahkan ke meja utama

            Debug.Log("Kartu dari meja tambahan dipindahkan ke meja utama: " + cardToMove.IDKartu);
        }

        // Setelah kartu dipindahkan, perbarui UI
        UpdateTableUI();
    }

    public void UpdateTableUI()
    {
        // Update meja utama
        for (int i = 0; i < tableCardButtons.Length; i++)
        {
            if (i < tableCards.Count)
            {
                tableCardButtons[i].image.sprite = tableCards[i].GbrKartu;  // Set sprite kartu di meja
                tableCardButtons[i].gameObject.SetActive(true);  // Tampilkan slot jika ada kartu
            }
            else
            {
                tableCardButtons[i].gameObject.SetActive(false);  // Sembunyikan slot kartu jika kosong
            }
        }

        // Update meja tambahan
        for (int i = 0; i < additionalTableCardButtons.Length; i++)
        {
            if (i < additionalTableCards.Count)
            {
                additionalTableCardButtons[i].image.sprite = additionalTableCards[i].GbrKartu;  // Set sprite kartu tambahan
                additionalTableCardButtons[i].gameObject.SetActive(true);  // Tampilkan slot jika ada kartu
            }
            else
            {
                additionalTableCardButtons[i].gameObject.SetActive(false);  // Sembunyikan slot kartu jika kosong
            }
        }
    }

    // Fungsi untuk mengocok dadu
    public void RollDice()
    {
        lastDiceResult = UnityEngine.Random.Range(1, 7);  // Simpan hasil dadu
        //lastDiceResult = 5;
        Debug.Log("Hasil dadu pemain " + playerData.Jurusan + ": " + lastDiceResult);

        // Update UI dadu di GameManager
        gameManager.UpdateDiceImage(lastDiceResult);

        // Proses hasil dadu
        HandleDiceResult(lastDiceResult);
    }

    // Fungsi untuk menangani hasil dadu
    void HandleDiceResult(int result)
    {
        switch (result)
        {
            case 1:
                Debug.Log("Pemain tidak mendapat kartu/Skip giliran.");
                break;
            case 2:
                Debug.Log("Pemain mendapat 1 kartu.");
                TakeCardFromDeck(1);
                break;
            case 3:
                Debug.Log("Pemain mendapat 2 kartu.");
                TakeCardFromDeck(2);
                break;
            case 4:
                Debug.Log("Pemain tukar kartu.");
                break;
            case 5:
                Debug.Log("Pemain membuang kartu lawan.");
                BuangKartuLawan();  // Ubah listener kartu ke kartu lawan
                //NonAktifKkanBuangKartuLawan();
                break;
            case 6:
                Debug.Log("Pemain membuang kartu lawan & mendapat 1 kartu.");
                TakeCardFromDeck(1);
                break;
        }

        // Cek apakah meja tambahan sudah kosong
        if (additionalTableCards.Count == 0)
        {
            Debug.Log("Meja tambahan kosong, giliran berpindah.");
            gameManager.EndTurn();
        }
        else
        {
            gameManager.DisableRollDice();  // Nonaktifkan fungsi roll dice di GameManager
            Debug.Log("Masih ada kartu di meja tambahan, pemain harus membuang kartu sebelum giliran berpindah.");
        }
    }
    public void BuangKartuLawan()
    {
        // Mengubah listener tombol kartu lawan untuk membuang kartu
        foreach (var lawan in PemainLawan)
        {
            for (int i = 0; i < lawan.tableCardButtons.Length; i++)
            {
                int index = i;
                lawan.tableCardButtons[i].onClick.RemoveAllListeners();
                lawan.tableCardButtons[i].onClick.AddListener(() => lawan.DiscardCard(index));
            }
        }
        Debug.Log("Listener kartu lawan diubah untuk membuang kartu.");
    }

    public void NonAktifKkanBuangKartuLawan()
    {
        // Menonaktifkan listener dari semua kartu lawan setelah buang kartu selesai
        foreach (var lawan in PemainLawan)
        {
            foreach (var button in lawan.tableCardButtons)
            {
                button.onClick.RemoveAllListeners();
           }
            
        }
        foreach (var button in tableCardButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        // Mengembalikan listener asli ke tombol kartu pemain sendiri
        foreach (var lawan in PemainLawan)
        {
            lawan.AktifKanFungsiLain();
        }
        AktifKanFungsiLain();

    }

    public void AktifKanFungsiLain()
    {
        // Mengembalikan listener asli untuk kartu pemain
        for (int i = 0; i < tableCardButtons.Length; i++)
        {
            int index = i;
            tableCardButtons[i].onClick.RemoveAllListeners(); // Bersihkan listener sebelumnya
            tableCardButtons[i].onClick.AddListener(() => OnCardClicked(index, true));
        }

        // Jika ada kartu tambahan, tambahkan listener juga
        for (int i = 0; i < additionalTableCardButtons.Length; i++)
        {
            int index = i;
            additionalTableCardButtons[i].onClick.RemoveAllListeners(); // Bersihkan listener sebelumnya
            additionalTableCardButtons[i].onClick.AddListener(() => OnCardClicked(index, false));
        }
        Debug.Log("Listener kartu pemain dikembalikan ke fungsi asli.");
    }

    public void DiscardCard(int index)
    {
        // Fungsi ini dipanggil saat pemain membuang kartu lawan
        if (index < tableCards.Count)
        {
            DekKartuBuang.sprite = tableCards[index].GbrKartu;
            DekKartuBuang.gameObject.SetActive(true);
            tableCards.RemoveAt(index);
        }

        UpdateTableUI();
        NonAktifKkanBuangKartuLawan(); // Memanggil fungsi untuk mengembalikan listener asli setelah kartu lawan dibuang
    }


    // Fungsi untuk mengambil kartu dari deck
    public void TakeCardFromDeck(int jumlahKartu)
    {
        for (int i = 0; i < jumlahKartu; i++)
        {
            if (gameManager.deckStack.Count > 0)
            {
                Kartunya kartu = gameManager.deckStack.Pop();
                ReceiveStartingCardToTable(kartu);  // Tambahkan kartu ke meja utama atau tambahan
            }
            else
            {
                Debug.Log("Deck habis, tidak ada kartu yang bisa diambil.");
                gameManager.DisableRollDice();  // Nonaktifkan fungsi roll dice di GameManager
                return;  // Keluar dari loop
            }
        }
    }

    // Fungsi untuk memeriksa apakah pemain sedang memilih kartu untuk dibuang
    public bool IsChoosingToDiscard()
    {
        return isChoosingToDiscard;
    }

    public void HitungScore()
    {
        Score = 0;
        for (int i = 0; i < tableCards.Count; i++)
        {
            if (playerData.Jurusan == tableCards[i].JurKartu)
            {
                Score = Score + tableCards[i].NilaiKartu;
            }
            else
            {
                Score = Score - tableCards[i].NilaiKartu;
            }
        }

    }

}