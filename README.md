# Bengkel Motor API

## Deskripsi
Project ini merupakan REST API sederhana untuk sistem manajemen bengkel motor. API ini digunakan untuk mengelola data transaksi servis kendaraan, mekanik, dan layanan servis.

## Teknologi
- Bahasa: C#
- Framework: ASP.NET Core Web API
- Database: PostgreSQL
- Library: Npgsql

## Instalasi & Menjalankan
1. Clone repository
2. Buka di Visual Studio
3. Install package: Npgsql
4. Jalankan project (F5)

## Import Database
1. Buka PostgreSQL / pgAdmin
2. Buat database: bengkel
3. Jalankan file: bengkel.sql

## Endpoint API

| Method | URL | Keterangan |
|------|-----|-----------|
| GET | /api/transaction | Ambil semua data |
| GET | /api/transaction/{id} | Ambil detail |
| POST | /api/transaction | Tambah data |
| PUT | /api/transaction/{id} | Update data |
| DELETE | /api/transaction/{id} | Hapus data |

## Video Presentasi
Link: https://youtube.com/...

