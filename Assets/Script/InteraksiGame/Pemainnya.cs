using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pemainnya : MonoBehaviour
{
    public DataPemain playerData;  // Menyimpan data pemain yang diassign
    public int lastDiceResult;     // Menyimpan hasil dadu terakhir

    // UI untuk menampilkan jurusan pemain
    public TMP_Text jurusanText;

    // UI untuk menampilkan kartu di meja utama
    public Button[] tableCardButtons = new Button[3];  // Slot kartu di meja utama

    // UI untuk menampilkan kartu di meja tambahan
    public Button[] additionalTableCardButtons = new Button[3];  // Slot kartu di meja tambahan

    public List<Kartunya> tableCards = new List<Kartunya>();  // Kartu di meja utama
    public List<Kartunya> additionalTableCards = new List<Kartunya>();  // Kartu di meja tambahan

    private GameManager gameManager;
   
    [SerializeField]
    private Image DekKartuBuang;  // Untuk Menampilkan Kartu yang Dibuang

    private bool isChoosingToDiscard = false; // Variabel untuk melacak apakah pemain sedang memilih kartu untuk dibuang

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  // Dapatkan referensi ke GameManager
        AddCardButtonListeners();  // Tambahkan listener ke kartu
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
        Debug.Log("Kartu diklik di index: " + index + " dari " + (isMainTable ? "Meja Utama" : "Meja Tambahan"));

        if (isChoosingToDiscard)
        {
            if (isMainTable)
            {
                if (index < tableCards.Count)
                {
                    Kartunya card = tableCards[index];
                    Debug.Log("Kartu di meja utama yang dibuang: " + card.IDKartu);
                    DiscardCardFromTable(index, true);
                }
            }
            else
            {
                if (index < additionalTableCards.Count)
                {
                    Kartunya card = additionalTableCards[index];
                    Debug.Log("Kartu di meja tambahan yang dibuang: " + card.IDKartu);
                    DiscardCardFromTable(index, false);
                }
            }

            // Periksa jika meja utama kosong setelah kartu dibuang dan isi dari meja tambahan
            if (additionalTableCards.Count > 0 && tableCards.Count < 3)
            {
                MoveCardFromAdditionalToMainTable();
            }
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
                //Sprite SimpanKartuBuang = tableCards.ElementAt(index);
                //DekKartuBuang.sprite= SimpanKartuBuang;
                //DekKartuBuang.gameObject.SetActive(true);
                tableCards.RemoveAt(index);  // Menghapus kartu dari list meja utama
                Debug.Log("Kartu di meja utama dihapus.");
            }
        }
        else
        {
            if (index < additionalTableCards.Count)
            {
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
        lastDiceResult = Random.Range(1, 7);  // Simpan hasil dadu
        Debug.Log("Hasil dadu pemain " + playerData.Jurusan + ": " + lastDiceResult);

        // Update UI dadu di GameManager
        gameManager.UpdateDiceImage(lastDiceResult);

        // Proses hasil dadu
        HandleDiceResult(lastDiceResult);
    }

    // Fungsi untuk memproses hasil dadu
    void HandleDiceResult(int result)
    {
        switch (result)
        {
            case 1:
                TakeCardFromDeck(1);
                break;
            case 2:
                TakeCardFromDeck(1);
                break;
            case 3:
                TakeCardFromDeck(1);
                break;
            case 4:
                TakeCardFromDeck(1);
                break;
            case 5:
                TakeCardFromDeck(1);
                break;
            case 6:
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
}
