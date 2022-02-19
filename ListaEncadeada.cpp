#include <iostream>
#include <stdio.h>
#include <stdlib.h>

typedef struct No{ //criei o meu tipo de dado com o struct 
    int valor;
    No* proximo; //ponteiro para um elemento do tipo No, ou seja, o pr�ximo da lista
}No;//nome da minha struct devido a nomea��o aqui em conjunto com o typedef
//n�o � necess�rio colocar struct toda vez que chama-lo

typedef struct {
    No* inicio;
    int tam;
}Lista;

//Inserir no inicio da lista
//Basicamente, ele insere um novo n� no inicio da lista
//lembrando que as variaveis PRECISAM ser do tipo ponteiro (*)
//pois os valores inseridos aqui, ser�o perdidos devido a ser uma variavel local
void inserirInicio(Lista *lista, int valor) { 
    //Malloc vai alocar espa�o de mem�ria para um tipo n� e vai nos retornar o endere�o
    //dessa informa��o na mem�ria
    No* novo = (No*)malloc(sizeof(No)); //Crio dinamicamente um novo n�
    //A regi�o de mem�ria apontada por novo da regi�o apontada, vai receber valor
    novo->valor = valor; //Salvo o valor do n�
    novo->proximo = lista->inicio; //Digo que o pr�ximo aponta pro inicio 
    lista->inicio = novo; //Lista inicio vai ser o novo n�
    lista->tam++; 
}

//Inserir no final da lista
void inserirFim(Lista* lista, int valor) {
    No *no, *novo = (No*)malloc(sizeof(No));
    novo->valor = valor;
    novo->proximo = NULL;

    //Aqui ele verifica se a lista est� vazia, pois se estiver, ele segue uma l�gica
    //parecia com a de inserir um valor no inicio de uma lista
    if (lista->inicio == NULL) {
        lista->inicio = novo;
    }
    //Caso n�o seja o caso de uma lista vazia, ele ir� procurar qual o ultimo
    //no que a lista tem seguindo de n� em n� at� encontrar o NULL
    else {
        no = lista->inicio;
        while (no->proximo != NULL) {
            no = no->proximo;
            no->proximo = novo;
        }
    }
    lista->tam++;
}

//Remover um elemento da lista de acordo com seu valor
void removerValor(Lista* lista, int valor) {
    No* inicio = lista->inicio;
    No* noARemover = NULL;


    if (inicio != NULL && lista->inicio->valor == valor) {
        noARemover = lista->inicio;
        lista->inicio = noARemover->proximo;
    }
    else {
        while (inicio != NULL && inicio->proximo != NULL && inicio->proximo->valor != valor){
            inicio = inicio->proximo;
        }
        noARemover = inicio->proximo;
        inicio->proximo = noARemover->proximo;
    }
    if (noARemover){
        free(noARemover);
        lista->tam--;
    }
}

//imprimir a lista
void imprimir(Lista *lista) {
    No* inicio = lista->inicio;
    printf("Tamanho da lista: %d\n", lista->tam);
    while (inicio != NULL) { //Enquanto inicio � diferente de NULL
        printf("%d\t ", inicio->valor);
        inicio = inicio->proximo;
    }
    printf("\n\n");
}

int main()
{
    Lista lista; //tenho uma variavel lista do tipo Lista
    int opcao, valor;

    lista.inicio = NULL;
    lista.tam = 0;

    do {
        printf("1 - Inserir no inicio\n2 - Imprimir\n3 - Inserir no final\n4 - Remover valor\n5 - Sair\n");
        scanf_s("%d", &opcao);
        switch (opcao)
        {
            case 1:
                system("cls");
                printf("Digite um valor a ser inserido:");
                scanf_s("%d", &valor);
                inserirInicio(&lista, valor);
                system("cls");
                break;
            case 2:
                imprimir(&lista);
                break;
            case 3:
                system("cls");
                printf("Digite um valor a ser inserido:");
                scanf_s("%d", &valor);
                inserirFim(&lista, valor);
                system("cls");
                break;
            case 4:
                system("cls");
                printf("Digite um valor a ser removido:");
                scanf_s("%d", &valor);
                removerValor(&lista, valor);
                system("cls");
                break;
            case 5:
                system("cls");
                printf("Finalizando...\n");
                break;
            default:
                printf("Opcao invalida!\n");
            break;
        }
    } while (opcao != 5);

    return 0;
}
