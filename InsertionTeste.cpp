#include <stdlib.h>
#include <stdio.h>

void insertion_sort(int vetor[], int tamanho);

int main()
{
    int vetor[] = { 8,4,9,5,7,6,3,2 };

    insertion_sort(vetor, 8);
    for (int i = 0; i < 8; i++)
        printf("vetor [%d] = %d\n", i, vetor[i]);

    return 0;
}

void insertion_sort(int vetor[], int tamanho)
{
    
    for (int i = 1; i < tamanho; i++)
    {
        
        int atual = vetor[i];
        int j = i - 1;
        while (j >= 0 && vetor[j] > atual)
        {
            vetor[j + 1] = vetor[j];
            j = j - 1;
        }
        vetor[j + 1] = atual;
    }
}

//melhor caso: O(n)
//pior caso: o(n*2)

// Serão 2 loops, 1 olhando pra cada elemento do vetor e um interno que estará trocando o valor para sua posição
//correta a sua esquerda.
// Enquanto J não terminar de verificar o vetor e J for maior que o atual J+1 receberá o valor de J