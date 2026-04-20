# Bengkel Motor API

## Deskripsi

Bengkel Motor API merupakan RESTful API yang dirancang untuk mengelola sistem layanan pada bengkel motor.
API ini mencakup pengelolaan data transaksi servis, mekanik, dan jenis layanan dengan struktur relasi yang terorganisir.

Project ini juga mengimplementasikan konsep **relational database**, **soft delete**, serta **validasi data** untuk menjaga konsistensi dan integritas data.

---

## Domain Sistem

Sistem ini digunakan untuk:

* Mencatat transaksi servis kendaraan
* Mengelola data mekanik
* Mengelola jenis layanan servis
* Menjaga histori data menggunakan fitur soft delete

---

## Teknologi yang Digunakan

| Komponen  | Teknologi            |
| --------- | -------------------- |
| Bahasa    | C#                   |
| Framework | ASP.NET Core Web API |
| Database  | PostgreSQL           |
| Library   | Npgsql               |

---

## Struktur Project

```
BengkelAPI/
│
├── Controllers/
│   └── TransactionController.cs
│
├── Models/
│   └── Transaction.cs
│
├── appsettings.json
├── Program.cs
├── bengkel.sql
└── README.md
```

---

## Instalasi & Cara Menjalankan

### 1. Clone Repository

```
git clone https://github.com/username/bengkel-api.git
```

### 2. Buka Project

Buka project menggunakan **Visual Studio**

### 3. Install Dependency

Install package berikut:

```
Npgsql
```

### 4. Konfigurasi Database

Edit file `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=bengkel;Username=postgres;Password=YOUR_PASSWORD"
}
```

### 5. Jalankan Project

Tekan:

```
F5 / Run
```

---

## Cara Import Database

1. Buka PostgreSQL / pgAdmin
2. Buat database baru:

```
bengkel
```

3. Jalankan file:

```
bengkel.sql
```

---

## 🔗 Endpoint API

| Method | Endpoint                      | Deskripsi                                 |
| ------ | ----------------------------- | ----------------------------------------- |
| GET    | /api/transaction              | Menampilkan semua data transaksi          |
| GET    | /api/transaction/{id}         | Menampilkan detail transaksi              |
| POST   | /api/transaction              | Menambahkan data transaksi                |
| PUT    | /api/transaction/{id}         | Mengupdate data transaksi                 |
| DELETE | /api/transaction/{id}         | Soft delete (data tidak dihapus permanen) |

---

## Fitur Utama

* CRUD lengkap (Create, Read, Update, Delete)
* Relasi antar tabel menggunakan Foreign Key
* Soft delete menggunakan kolom `deleted_at`
* Validasi data untuk menjaga konsistensi database
* Response API dalam format JSON yang konsisten

---

## Video Presentasi

Link video:

```
(https://youtube.com/https://youtube.com/ISI_LINK_VIDEO)

---

## Catatan

* Pastikan PostgreSQL sudah berjalan sebelum menjalankan API
* Gunakan ID yang valid untuk menghindari error foreign key
* Endpoint DELETE default menggunakan **soft delete**

---
