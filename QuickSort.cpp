#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void troca(int* x, int* y);
void quicksort(int vetor[], int tamanho);
void quicksort_recursion(int vetor[], int menor, int maior);
int partition(int vetor[], int menor, int maior);

int main(){
 
    int vetor[] = { 10,11,23,44,8,15,3,9,12,45,56,45,45 };
    int tamanho = 13;

    quicksort(vetor, tamanho);

    for (int i = 0; i < tamanho; i++) {
        printf("%d ", vetor[i]);
        printf("\n");
    }

    return 0;
}

void troca(int* x, int* y) {

    //Parte em que ocorre a troca dos valores em i e j onde x tem as coordenadas do valor
    //de i e y tem as coordenadas do valor de j, para a trocar dar certo temos uma variavel
    //temporária tmp para receber o valor de x para que ele possa receber seu proximo valor
    int temp = *x;
    *x = *y;
    *y = temp;

}

void quicksort(int vetor[], int tamanho) {

    //Essa parte vai ser utilizada para verificar os valores antes do nosso
    //pivô, como definimos ele como o ultimo valor do vetor, ele está pegando
    //a partir da posição 0 até a tamanho-1 ou seja o valor antes do ultimo do vetor
    quicksort_recursion(vetor, 0, tamanho - 1);

}

void quicksort_recursion(int vetor[], int menor, int maior) {

    if (menor < maior) {
        //Aqui é onde será aplicada a ideia de dividir para conquistar
        //Chamamos partition para dividir o vetor dando seus limites menor e maior
        //isso irá retornar a posição do nosso pivô para nós dividirmos a parte esquerda
        //e direita do nosso vetor para aplicarmos novamente a função QuickSort
        int pivot_index = partition(vetor, menor, maior);
        //Essa será a parte esquerda do nosso vetor, pegando os valores antes do pivô
        quicksort_recursion(vetor, menor, pivot_index - 1);
        //Essa será a parte direita do nosso vetor, pegando os valores depois do pivô
        quicksort_recursion(vetor, pivot_index + 1, maior);
    }
}

int partition(int vetor[], int menor, int maior) {

    //Esse é o momento em que dizemos que o nosso pivô está em algum lugar,
    //no caso, ele sempre será a maior posição do nosso vetor, então pivot_value
    //irá receber o valor da última posição do nosso vetor
    int pivot_value = vetor[maior];
    int i = menor;


    //Aqui é onde a magia da comparação acontece, toda vez que j for maior que o pivô
    //será feito j++ para verificar até o momento em que j seja menor que i, nesse 
    //momento i receberá o valor de j via void troca e j receberá o valor de i por
    //meio de nossa variavel temp. Após isso i receberá i++ e as verificações continuarão
    //até j terminar e prosseguirmos para os comentários abaixo
    for (int j = menor; j < maior; j++) {

        if (vetor[j] <= pivot_value) {
            troca(&vetor[i], &vetor[j]);
            i++;
        }
    }

    //Depois do For acima ser completo, essa parte irá declarar que o nosso maior valor
    //é aqule da posição i, pois terminamos de separar os valores que eram menores dos 
    //que eram maiores que o valor utilizado como pivô
    troca(&vetor[i], &vetor[maior]);

    //Vamos retornar o valor de i pois é onde o pivô está, como sabemos disso?
    //Nós sabemos pois foi o que declaramos na linha acima, dizendo que o maior valor
    //do vetor é a ultima posição do i 
    return i;

}