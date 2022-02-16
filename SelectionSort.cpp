#include <iostream>

int main()
{
    int vetor[] = { 5, 9, 7, 6, 4, 0, 2, 3, 8, 1 };
    int tamanho = 10;

    for (int i = 0; i < tamanho - 1; i++)
    {
        int min_pos = i;

        for (int j = i + 1; j < tamanho; j++) {

            if (vetor[j] < vetor[min_pos]) {
                min_pos = j;
            }

            if (min_pos != i) {
                int temp = vetor[i];
                vetor[i] = vetor[min_pos];
                vetor[min_pos] = temp;
            }
        }
    }

    for (int i = 0; i < tamanho; i++) {
        printf("vetor[%d] = %d\n", i, vetor[i]);
    }

    return 0;
}

// Melhor caso: O(N^2)
// Pior caso: O(N^2)
// Insuficiente para grandes conjuntos de dados