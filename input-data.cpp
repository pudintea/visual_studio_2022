#include <iostream>
using namespace std;

int data[100]; // array global agar bisa diakses fungsi

// Fungsi untuk input data
void InputData(int N) {
    for(int i = 1; i <= N; i++) {
        cout << "Masukkan Nilai ke-" << i << ": ";
        cin >> data[i];
    }
}

// Fungsi untuk menampilkan data
void TampilData(int N) {
    for(int i = 1; i <= N; i++) {
        cout << "Data ke-" << i << " = " << data[i] << endl;
    }
}

int main() {
    int N;
    cout << "Banyak data: ";
    cin >> N;

    InputData(N);
    TampilData(N);

    return 0;
}

