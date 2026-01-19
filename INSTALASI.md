Oke, kodenya **sudah lengkap dan BENAR** 👍
Ini adalah **Windows Forms App (C#)** yang **dibuat full lewat kode (tanpa Designer)**.
Sekarang saya jelaskan **cara menjalankannya di Visual Studio 2022 langkah demi langkah**, **PASTI BISA RUN**.

---

# CARA MENJALANKAN KODE `FormBMI.cs` DI VISUAL STUDIO 2022

## A. Buat Project Baru (WAJIB)

Karena kode ini **tidak berdiri sendiri**, dia harus ada di dalam **Windows Forms Project**.

### 1. Buka Visual Studio Community 2022

### 2. Klik **Create a new project**

### 3. Pilih:

👉 **Windows Forms App (.NET Framework)**
⚠️ **PENTING:** JANGAN pilih yang `.NET` (Core)

### 4. Klik **Next**

Isi:

* **Project name**: `KalkulatorBMI`
* **Location**: bebas
* **Solution name**: `KalkulatorBMI`

Klik **Create**

### 5. Pilih Framework

* Pilih **.NET Framework 4.8**
* Klik **Create**

---

## B. Ganti Form Default dengan Kode Anda

### 1. Hapus Form bawaan

Di **Solution Explorer**:

* Klik kanan `Form1.cs`
* Pilih **Delete**
* Klik **Yes**

---

### 2. Tambahkan File `FormBMI.cs`

1. Klik kanan **Project KalkulatorBMI**
2. Pilih **Add → Class**
3. Nama file: **FormBMI.cs**
4. Klik **Add**
5. **Hapus semua isi file**
6. **Paste seluruh kode yang Anda kirim**
7. **Save** (Ctrl + S)

⚠️ Pastikan:

```csharp
namespace KalkulatorBMI
```

SAMA dengan **nama project**

---

## C. Atur FormBMI sebagai Startup Form

### 1. Buka file **Program.cs**

Isi default biasanya seperti ini:

```csharp
Application.Run(new Form1());
```

### 2. Ganti menjadi:

```csharp
Application.Run(new FormBMI());
```

### 3. Pastikan `using` ada:

```csharp
using System;
using System.Windows.Forms;
```

### Contoh Program.cs yang BENAR:

```csharp
using System;
using System.Windows.Forms;

namespace KalkulatorBMI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormBMI());
        }
    }
}
```

---

## D. Build & Run Program

### 1. Build dulu

Menu atas → **Build → Build Solution**
Pastikan muncul:

```
Build succeeded
```

---

### 2. Jalankan Program

Tekan salah satu:

* **F5**
* **Ctrl + F5**
* Tombol **▶ Start**

🎉 **Aplikasi Kalkulator BMI akan muncul**

---

## E. Kalau Muncul Error (Yang Paling Sering)

### ❌ Error: `The name 'FormBMI' does not exist`

✔ Solusi:

* Pastikan **nama class = FormBMI**
* Pastikan **Program.cs dan FormBMI.cs satu namespace**

---

### ❌ Error saat pilih project

✔ Pastikan Anda memilih:

* **Windows Forms App (.NET Framework)**
* Bukan **Console App**
* Bukan **WPF**
* Bukan **Windows Forms App (.NET)**

---

## F. Kesimpulan Singkat (Untuk Dilaporkan ke Dosen 😄)

* Program dibuat menggunakan **Windows Forms C#**
* Seluruh UI dibuat secara **manual via kode**
* Menggunakan:

  * **Enum**
  * **Operator aritmatika**
  * **Event handling**
  * **Container & Common Controls**
* Dijalankan melalui **Visual Studio Community 2022**

---

Kalau mau, saya bisa:

* 🔹 Buatkan **laporan tugas** (latar belakang, tujuan, kesimpulan)
* 🔹 Buat **diagram alur program**
* 🔹 Jelaskan **fungsi tiap bagian kode** (buat presentasi)

Tinggal bilang 👍
