using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Pemainnya[] players;  // Referensi ke objek pemain

    [SerializeField]
    private PemainSO pemainDatabase;  // Referensi ke ScriptableObject PemainSO

    [SerializeField]
    private KartuSO kartuDatabase;  // Referensi ke ScriptableObject KartuSO

    [SerializeField]
    private TMP_Text sisaKartuText;  // TMP Text untuk menampilkan jumlah kartu yang tersisa di deck

    [SerializeField]
    private TMP_Text pesanAwalText;  // TMP Text untuk menampilkan jumlah kartu yang tersisa di deck

    [SerializeField]
    private Image diceImage;  // Objek UI untuk menampilkan dadu

    [SerializeField]
    private Sprite[] diceSprites;  // Array Sprite untuk dadu (1-6)

    [SerializeField]
    private Button rollDiceButton;  // Referensi ke tombol Roll Dice

    public Stack<Kartunya> deckStack = new Stack<Kartunya>();  // Stack untuk menyimpan kartu permainan
    private int currentPlayerIndex = 0;  // Indeks pemain yang saat ini mendapatkan giliran
    private bool isWaitingForDiscard = false;  // Menyimpan status apakah sedang menunggu pembuangan kartu
    private bool isDiceRollAllowed = true;  // Flag untuk mengontrol apakah dadu boleh diklik

    void Start()
    {
        AssignPlayerDataFromSO();   // Mengambil data dari PemainSO dan meng-assign ke pemain
        InitializeDeck();           // Inisialisasi deck kartu
        DistribusiKartuKeMeja();  // Distribusi kartu modal ke meja pemain
        UpdateSisaKartuText();             // Perbarui jumlah sisa kartu setelah inisialisasi

        // Memulai giliran pemain pertama tanpa langsung memutar dadu
        Debug.Log("Giliran pemain pertama: " + players[currentPlayerIndex].playerData.Jurusan);

    }

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
        pesanAwalText.text = "CEKIDOT!!!";
        pesanAwalText.gameObject.SetActive(true);

        // Tunggu 2 detik untuk pesan kedua
        yield return new WaitForSeconds(2f);

        // Sembunyikan pesan kedua
        pesanAwalText.gameObject.SetActive(false);

        // Lanjutkan ke fungsi untuk mengambil kartu
       // MengambilKartuLangsung();
    }


    // Fungsi yang dipanggil ketika dadu diklik
    public void OnDiceClick()
    {
        if (!isDiceRollAllowed)
        {
            Debug.Log("Pemain belum membuang kartu. Dadu tidak bisa diklik.");
            return;
        }

        Pemainnya currentPlayer = players[currentPlayerIndex];
        currentPlayer.RollDice();  // Pemain mengocok dadu
    }

    // Fungsi untuk menonaktifkan tombol roll dice
    public void DisableRollDice()
    {
        rollDiceButton.interactable = false;  // Nonaktifkan tombol roll dice
        Debug.Log("Tombol Roll Dice dinonaktifkan karena deck habis.");
    }

    // Fungsi untuk mengaktifkan kembali tombol roll dice (jika diperlukan)
    public void EnableRollDice()
    {
        rollDiceButton.interactable = true;  // Aktifkan kembali tombol roll dice
        Debug.Log("Tombol Roll Dice diaktifkan kembali.");
    }

    // Mengambil data pemain dari PemainSO dan meng-assignnya ke objek pemain
    void AssignPlayerDataFromSO()
    {
        for (int i = 0; i < players.Length && i < pemainDatabase.PlayerList.Count; i++)
        {
            players[i].SetPlayerData(pemainDatabase.PlayerList[i]);
        }
    }

    // Inisialisasi deck dengan kartu yang sesuai dengan jurusan pemain
    void InitializeDeck()
    {
        List<Kartunya> kartuList = new List<Kartunya>();

        // Ambil jurusan dari setiap pemain
        HashSet<string> jurusanPemain = new HashSet<string>();
        foreach (var player in players)
        {
            if (player.playerData != null)
            {
                jurusanPemain.Add(player.playerData.Jurusan);  // Ambil jurusan dari DataPemain
            }
        }

        // Filter kartu berdasarkan jurusan yang ada pemainnya
        foreach (var kartu in kartuDatabase.KartuList)
        {
            if (jurusanPemain.Contains(kartu.JurKartu))
            {
                kartuList.Add(kartu);  // Tambahkan kartu yang sesuai dengan jurusan
            }
        }

        // Acak kartu yang sesuai jurusan dan masukkan ke Stack
        while (kartuList.Count > 0)
        {
            int randomIndex = Random.Range(0, kartuList.Count);
            Kartunya kartuTerpilih = kartuList[randomIndex];
            deckStack.Push(kartuTerpilih);  // Masukkan kartu ke stack
            kartuList.RemoveAt(randomIndex);  // Hapus dari list
        }
    }

    // Fungsi untuk mendistribusikan kartu modal ke meja pemain
    void DistribusiKartuKeMeja()
    {
        foreach (Pemainnya player in players)
        {
            if (deckStack.Count > 0)
            {
                Kartunya kartuAwal = deckStack.Pop();  // Ambil kartu dari stack
                player.ReceiveStartingCardToTable(kartuAwal);  // Berikan kartu ke pemain langsung ke meja
                UpdateSisaKartuText();  // Perbarui jumlah sisa kartu setelah distribusi kartu
            }
        }
    }

    // Fungsi untuk memperbarui jumlah sisa kartu di deck
    void UpdateSisaKartuText()
    {
        sisaKartuText.text = deckStack.Count.ToString(); // Tampilkan jumlah sisa kartu
    }

    void Update()
    {
        UpdateSisaKartuText();
    }

    // Fungsi untuk memperbarui gambar dadu
    public void UpdateDiceImage(int diceValue)
    {
        if (diceValue >= 1 && diceValue <= 6)
        {
            diceImage.sprite = diceSprites[diceValue - 1];  // Set sprite sesuai hasil dadu
        }
    }

    // Fungsi untuk mengakhiri giliran dan berpindah ke pemain berikutnya
    public void EndTurn()
    {
        if (players[currentPlayerIndex].IsChoosingToDiscard())
        {
            Debug.Log("Menunggu pemain membuang kartu...");
            isWaitingForDiscard = true;
            //isDiceRollAllowed = false;
            return;
        }

        isWaitingForDiscard = false;
        //isDiceRollAllowed = true;

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Length)
        {
            currentPlayerIndex = 0;
        }

        Debug.Log("Sekarang giliran pemain: " + players[currentPlayerIndex].playerData.Jurusan);
    }
}
