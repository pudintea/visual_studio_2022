#include <iostream>
#include <string>
#include <iomanip>
#include <algorithm>
#include <cctype>

// Pudin Saepudin Ilham
// 250401010001
// PJJ INFORMATIKA
// IF107
// 07 Februari 2026

using namespace std;

// Struct untuk merepresentasikan data produk
struct Produk {
    string kodeProduk;
    string nama;
    string kategori;
    double harga;
    int stok;
};

// Class Inventaris untuk mengelola data produk
class Inventaris {
private:
    Produk produkList[50];  // Array untuk menyimpan produk (max 50)
    int jumlahProduk;       // Jumlah produk yang tersimpan
    Produk* ptrProduk;      // Pointer untuk mengakses data produk

    // Fungsi rekursif untuk menghitung total nilai inventaris
    double hitungTotalNilaiRekursif(int index) {
        // Base case: jika index negatif, return 0
        if (index < 0) {
            return 0;
        }
        // Recursive case: hitung nilai produk saat ini + total nilai sebelumnya
        return (produkList[index].harga * produkList[index].stok) + 
               hitungTotalNilaiRekursif(index - 1);
    }

    // Fungsi helper untuk validasi string kosong
    bool stringKosong(const string& str) {
        // FIX: Kembalikan true jika string kosong atau hanya whitespace
        if (str.empty()) return true;
        
        for (size_t i = 0; i < str.length(); i++) {
            if (!isspace(str[i])) return false;
        }
        return true;
    }

    // Fungsi untuk konversi string ke uppercase
    string toUpperCase(string str) {
        transform(str.begin(), str.end(), str.begin(), ::toupper);
        return str;
    }

    // Fungsi untuk konversi string ke lowercase
    string toLowerCase(string str) {
        transform(str.begin(), str.end(), str.begin(), ::tolower);
        return str;
    }

public:
    // Constructor
    Inventaris() {
        jumlahProduk = 0;
        ptrProduk = produkList;  // Inisialisasi pointer ke array
    }

    // Method untuk menambah produk baru
    void tambahProduk() {
        if (jumlahProduk >= 50) {
            cout << "\n[ERROR] Inventaris penuh! Maksimal 50 produk.\n";
            return;
        }

        Produk produkBaru;
        cin.ignore();

        // Input dan validasi kode produk
        do {
            cout << "\nMasukkan Kode Produk: ";
            getline(cin, produkBaru.kodeProduk);
        
            if (stringKosong(produkBaru.kodeProduk)) {
                cout << "[ERROR] Kode produk tidak boleh kosong!\n";
            } 
            else if (cariProdukByKode(produkBaru.kodeProduk) != NULL) {
                cout << "[ERROR] Kode produk sudah ada!\n";
                produkBaru.kodeProduk = "";
            }
        
        } while (stringKosong(produkBaru.kodeProduk) || 
                 cariProdukByKode(produkBaru.kodeProduk) != NULL);
        
        // FIX: Ubah ke huruf besar menggunakan nama fungsi yang benar
        produkBaru.kodeProduk = toUpperCase(produkBaru.kodeProduk);

        // Input dan validasi nama produk
        do {
            cout << "Masukkan Nama Produk: ";
            getline(cin, produkBaru.nama);
            if (stringKosong(produkBaru.nama)) {
                cout << "[ERROR] Nama produk tidak boleh kosong!\n";
            }
        } while (stringKosong(produkBaru.nama));

        // Input dan validasi kategori
        do {
            cout << "Masukkan Kategori (Smartphone/Laptop/Aksesoris): ";
            getline(cin, produkBaru.kategori);
            if (stringKosong(produkBaru.kategori)) {
                cout << "[ERROR] Kategori tidak boleh kosong!\n";
            }
        } while (stringKosong(produkBaru.kategori));

        // Input harga
        do {
            cout << "Masukkan Harga (Rp): ";
            cin >> produkBaru.harga;
            if (produkBaru.harga <= 0) {
                cout << "[ERROR] Harga harus lebih dari 0!\n";
            }
        } while (produkBaru.harga <= 0);

        // Input stok
        do {
            cout << "Masukkan Stok (unit): ";
            cin >> produkBaru.stok;
            if (produkBaru.stok < 0) {
                cout << "[ERROR] Stok tidak boleh negatif!\n";
            }
        } while (produkBaru.stok < 0);

        // Simpan produk ke array menggunakan pointer
        *(ptrProduk + jumlahProduk) = produkBaru;
        jumlahProduk++;

        cout << "\n[SUCCESS] Produk berhasil ditambahkan!\n";
    }

    // Method untuk menampilkan semua produk
    void tampilkanSemuaProduk() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        cout << "\n" << string(105, '=') << endl;
        cout << "                       DAFTAR SEMUA PRODUK TECHMART" << endl;
        cout << string(105, '=') << endl;
        cout << left << setw(12) << "KODE" 
             << setw(25) << "NAMA PRODUK" 
             << setw(15) << "KATEGORI"
             << right << setw(18) << "HARGA (Rp)" 
             << setw(12) << "STOK"
             << setw(20) << "TOTAL NILAI (Rp)" << endl;
        cout << string(105, '-') << endl;

        for (int i = 0; i < jumlahProduk; i++) {
            Produk* p = ptrProduk + i;  // Menggunakan pointer
            cout << left << setw(12) << p->kodeProduk
                 << setw(25) << p->nama
                 << setw(15) << p->kategori
                 << right << setw(18) << fixed << setprecision(0) << p->harga
                 << setw(12) << p->stok
                 << setw(20) << (p->harga * p->stok) << endl;
        }
        cout << string(105, '=') << endl;
    }

    // Method untuk mencari produk berdasarkan kode (menggunakan pointer)
    Produk* cariProdukByKode(string kode) {
        kode = toUpperCase(kode);
        for (int i = 0; i < jumlahProduk; i++) {
            Produk* p = ptrProduk + i;
            if (p->kodeProduk == kode) {
                return p;  // Return pointer ke produk yang ditemukan
            }
        }
        return NULL;  // Produk tidak ditemukan
    }

    // Method untuk mencari dan menampilkan produk
    void cariProduk() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        string kode;
        cin.ignore();
        cout << "\nMasukkan Kode Produk yang dicari: ";
        getline(cin, kode);

        Produk* p = cariProdukByKode(kode);
        
        if (p != NULL) {
            cout << "\n" << string(80, '=') << endl;
            cout << "                    PRODUK DITEMUKAN" << endl;
            cout << string(80, '=') << endl;
            cout << "Kode Produk    : " << p->kodeProduk << endl;
            cout << "Nama Produk    : " << p->nama << endl;
            cout << "Kategori       : " << p->kategori << endl;
            cout << "Harga          : Rp " << fixed << setprecision(0) << p->harga << endl;
            cout << "Stok           : " << p->stok << " unit" << endl;
            cout << "Total Nilai    : Rp " << fixed << setprecision(0) << (p->harga * p->stok) << endl;
            cout << string(80, '=') << endl;
        } else {
            cout << "\n[ERROR] Produk dengan kode '" << kode << "' tidak ditemukan!\n";
        }
    }

    // Method untuk menghitung total nilai inventaris (wrapper untuk fungsi rekursif)
    void hitungTotalNilai() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        double total = hitungTotalNilaiRekursif(jumlahProduk - 1);
        
        cout << "\n" << string(65, '=') << endl;
        cout << "       TOTAL NILAI INVENTARIS (Rekursif)" << endl;
        cout << string(65, '=') << endl;
        cout << "Jumlah Produk     : " << jumlahProduk << " item" << endl;
        cout << "Total Nilai       : Rp " << fixed << setprecision(0) << total << endl;
        cout << string(65, '=') << endl;
        cout << "\nRincian Perhitungan Rekursif:" << endl;
        cout << string(65, '-') << endl;
        
        for (int i = 0; i < jumlahProduk; i++) {
            Produk* p = ptrProduk + i;
            cout << left << setw(20) << p->nama 
                 << ": Rp " << right << setw(12) << fixed << setprecision(0) << p->harga 
                 << " x " << setw(3) << p->stok 
                 << " unit = Rp " << setw(12) << (p->harga * p->stok) << endl;
        }
        cout << string(65, '=') << endl;
    }

    // Method untuk menampilkan produk dengan stok menipis
    void tampilkanStokMenipis() {
        bool adaStokMenipis = false;
        
        cout << "\n" << string(100, '=') << endl;
        cout << "                 PRODUK DENGAN STOK MENIPIS (< 5)" << endl;
        cout << string(100, '=') << endl;
        cout << left << setw(12) << "KODE" 
             << setw(25) << "NAMA PRODUK" 
             << setw(15) << "KATEGORI"
             << right << setw(18) << "HARGA (Rp)" 
             << setw(12) << "STOK" << endl;
        cout << string(100, '-') << endl;

        for (int i = 0; i < jumlahProduk; i++) {
            Produk* p = ptrProduk + i;
            if (p->stok < 5) {
                cout << left << setw(12) << p->kodeProduk
                     << setw(25) << p->nama
                     << setw(15) << p->kategori
                     << right << setw(18) << fixed << setprecision(0) << p->harga
                     << setw(9) << p->stok << " unit [LOW]" << endl;
                adaStokMenipis = true;
            }
        }

        if (!adaStokMenipis) {
            cout << "                      Tidak ada produk dengan stok menipis" << endl;
        }
        cout << string(100, '=') << endl;
    }

    // Method untuk mengurutkan produk berdasarkan stok
    void urutkanProduk() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        int pilihan;
        cout << "\nPilih metode pengurutan:\n";
        cout << "1. Ascending (Stok terendah ke tertinggi)\n";
        cout << "2. Descending (Stok tertinggi ke terendah)\n";
        cout << "Pilihan: ";
        cin >> pilihan;

        // Bubble sort
        for (int i = 0; i < jumlahProduk - 1; i++) {
            for (int j = 0; j < jumlahProduk - i - 1; j++) {
                bool tukar = false;
                if (pilihan == 1) {
                    // Ascending
                    if ((ptrProduk + j)->stok > (ptrProduk + j + 1)->stok) {
                        tukar = true;
                    }
                } else {
                    // Descending
                    if ((ptrProduk + j)->stok < (ptrProduk + j + 1)->stok) {
                        tukar = true;
                    }
                }

                if (tukar) {
                    Produk temp = *(ptrProduk + j);
                    *(ptrProduk + j) = *(ptrProduk + j + 1);
                    *(ptrProduk + j + 1) = temp;
                }
            }
        }

        cout << "\n[SUCCESS] Produk berhasil diurutkan!\n";
        tampilkanSemuaProduk();
    }

    // Method untuk edit stok produk
    void editStok() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        string kode;
        cin.ignore();
        cout << "\nMasukkan Kode Produk yang akan diedit: ";
        getline(cin, kode);

        Produk* p = cariProdukByKode(kode);
        
        if (p != NULL) {
            cout << "\nProduk ditemukan: " << p->nama << endl;
            cout << "Stok saat ini: " << p->stok << " unit" << endl;
            
            int stokBaru;
            do {
                cout << "Masukkan stok baru (unit): ";
                cin >> stokBaru;
                if (stokBaru < 0) {
                    cout << "[ERROR] Stok tidak boleh negatif!\n";
                }
            } while (stokBaru < 0);

            p->stok = stokBaru;
            cout << "\n[SUCCESS] Stok berhasil diupdate!\n";
        } else {
            cout << "\n[ERROR] Produk dengan kode '" << kode << "' tidak ditemukan!\n";
        }
    }

    // Method untuk menghapus produk
    void hapusProduk() {
        if (jumlahProduk == 0) {
            cout << "\n[INFO] Belum ada produk dalam inventaris.\n";
            return;
        }

        string kode;
        cin.ignore();
        cout << "\nMasukkan Kode Produk yang akan dihapus: ";
        getline(cin, kode);

        kode = toUpperCase(kode);
        int indexHapus = -1;

        // Cari index produk yang akan dihapus
        for (int i = 0; i < jumlahProduk; i++) {
            if ((ptrProduk + i)->kodeProduk == kode) {
                indexHapus = i;
                break;
            }
        }

        if (indexHapus != -1) {
            string namaProduk = (ptrProduk + indexHapus)->nama;
            
            // Geser produk setelah index yang dihapus
            for (int i = indexHapus; i < jumlahProduk - 1; i++) {
                *(ptrProduk + i) = *(ptrProduk + i + 1);
            }
            jumlahProduk--;
            
            cout << "\n[SUCCESS] Produk '" << namaProduk << "' berhasil dihapus!\n";
        } else {
            cout << "\n[ERROR] Produk dengan kode '" << kode << "' tidak ditemukan!\n";
        }
    }
};

// Fungsi untuk menampilkan menu
void tampilkanMenu() {
    cout << "\n" << string(55, '=') << endl;
    cout << "      SISTEM MANAJEMEN INVENTARIS TECHMART" << endl;
    cout << string(55, '=') << endl;
    cout << "1. Tambah Produk Baru" << endl;
    cout << "2. Tampilkan Semua Produk" << endl;
    cout << "3. Cari Produk (berdasarkan kode)" << endl;
    cout << "4. Hitung Total Nilai Inventaris (Rekursif)" << endl;
    cout << "5. Tampilkan Produk dengan Stok Menipis (< 5)" << endl;
    cout << "6. Urutkan Produk Berdasarkan Stok" << endl;
    cout << "7. Edit Stok Produk" << endl;
    cout << "8. Hapus Produk" << endl;
    cout << "9. Keluar" << endl;
    cout << string(55, '=') << endl;
    cout << "Pilihan Anda: ";
}

int main() {
    Inventaris inventaris;
    int pilihan;

    do {
        tampilkanMenu();
        cin >> pilihan;

        switch (pilihan) {
            case 1:
                inventaris.tambahProduk();
                break;
            case 2:
                inventaris.tampilkanSemuaProduk();
                break;
            case 3:
                inventaris.cariProduk();
                break;
            case 4:
                inventaris.hitungTotalNilai();
                break;
            case 5:
                inventaris.tampilkanStokMenipis();
                break;
            case 6:
                inventaris.urutkanProduk();
                break;
            case 7:
                inventaris.editStok();
                break;
            case 8:
                inventaris.hapusProduk();
                break;
            case 9:
                cout << "\n" << string(55, '=') << endl;
                cout << "   Terima kasih telah menggunakan TechMart!" << endl;
                cout << string(55, '=') << endl;
                break;
            default:
                cout << "\n[ERROR] Pilihan tidak valid!\n";
        }

        if (pilihan != 9) {
            cout << "\nTekan Enter untuk melanjutkan...";
            cin.ignore();
            cin.get();
        }

    } while (pilihan != 9);

    return 0;
}
