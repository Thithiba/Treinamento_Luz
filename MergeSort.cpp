#include <stdio.h>

void merge_sort(int a[], int length);
void merge_sort_recursion(int a[], int l, int r);
void merge_sorted_arrays(int a[], int l, int m, int r);

int main()
{
    int array[] = { 9, 4, 8, 1, 7, 0, 3, 2, 5, 6 };
    int length = 10;

    merge_sort(array, length);

    for (int i = 0; i < length; i++)
        printf("%d ", array[i]);
    printf("\n");

    return 0;
}

void merge_sort(int a[], int length)
{
    merge_sort_recursion(a, 0, length - 1);
}

void merge_sort_recursion(int a[], int l, int r)
{
    // Como diz abaixo, a partir do momento em que o vetor estiver com o máximo de
    // divisões possíveis, ou seja, l, m e r iguais a 0, ele vai parar de fazer
    // o processo de dividir o vetor
    if (l < r)
    {
        // Aqui encontramos o meio do nosso vetor
        int m = l + (r - l) / 2;

        // Nesse momento que temos os valores, vamos chamar novamente o merge_sort 
        // com o valor de m para que possamos continuar as divisões do vetor até
        // termos seus valores separadinhos, ou seja o momento em que l, m e r
        // serão iguais a 0
        merge_sort_recursion(a, l, m);
        merge_sort_recursion(a, m + 1, r);

        // A partir daqui, o vetor já está com seus valores divididos, então
        // essa é a parte que vamos ordenando os valores divididos do vetor
        merge_sorted_arrays(a, l, m, r);
    }
}

void merge_sorted_arrays(int a[], int l, int m, int r)
{
    // Calcula o tamanho esquerdo e direito do vetor
    int left_length = m - l + 1;
    int right_length = r - m;

    // Ele cria dois vetores temporários para armazenar os valores dos vetores
    // esquerdo e direito, porém não deu certo aqui como ele utilizou, dei
    // valores para os vetores, para que o erro não continuasse
    int temp_left[12];
    int temp_right[12];

    int i, j, k;

    // copia a porcao esquerda pro vetor temporario esq
    for (int i = 0; i < left_length; i++)
        temp_left[i] = a[l + i];

    // copia a porcao direita pro vetor temporario dir
    for (int i = 0; i < right_length; i++)
        temp_right[i] = a[m + 1 + i];

    for (i = 0, j = 0, k = l; k <= r; k++)
    {

        if ((i < left_length) &&
            (j >= right_length || temp_left[i] <= temp_right[j]))
        {
            a[k] = temp_left[i];
            i++;
        }

        else
        {
            a[k] = temp_right[j];
            j++;
        }
    }
}