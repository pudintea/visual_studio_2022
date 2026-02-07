#include <iostream>
using namespace std;

// Fungsi faktorial menggunakan rekursi
long long Faktorial(int n) {
    if(n == 0 || n == 1)
        return 1;
    else
        return n * Faktorial(n - 1);
}

int main() {
    int N;
    cout << "Masukkan N: ";
    cin >> N;

    long long hasil = Faktorial(N);

    cout << "Faktorial dari " << N << " = " << hasil << endl;

    return 0;
}

