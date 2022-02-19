#include <iostream>
#include <stdio.h>
#include <stdlib.h>

typedef struct No{ //criei o meu tipo de dado com o struct 
    int valor;
    No* proximo; //ponteiro para um elemento do tipo No, ou seja, o próximo da lista
}No;//nome da minha struct devido a nomeação aqui em conjunto com o typedef
//não é necessário colocar struct toda vez que chama-lo

typedef struct {
    No* inicio;
    int tam;
}Lista;

//Inserir no inicio da lista
//Basicamente, ele insere um novo nó no inicio da lista
//lembrando que as variaveis PRECISAM ser do tipo ponteiro (*)
//pois os valores inseridos aqui, serão perdidos devido a ser uma variavel local
void inserirInicio(Lista *lista, int valor) { 
    //Malloc vai alocar espaço de memória para um tipo nó e vai nos retornar o endereço
    //dessa informação na memória
    No* novo = (No*)malloc(sizeof(No)); //Crio dinamicamente um novo nó
    //A região de memória apontada por novo da região apontada, vai receber valor
    novo->valor = valor; //Salvo o valor do nó
    novo->proximo = lista->inicio; //Digo que o próximo aponta pro inicio 
    lista->inicio = novo; //Lista inicio vai ser o novo nó
    lista->tam++; 
}

//Inserir no final da lista
void inserirFim(Lista* lista, int valor) {
    No *no, *novo = (No*)malloc(sizeof(No));
    novo->valor = valor;
    novo->proximo = NULL;

    //Aqui ele verifica se a lista está vazia, pois se estiver, ele segue uma lógica
    //parecia com a de inserir um valor no inicio de uma lista
    if (lista->inicio == NULL) {
        lista->inicio = novo;
    }
    //Caso não seja o caso de uma lista vazia, ele irá procurar qual o ultimo
    //no que a lista tem seguindo de nó em nó até encontrar o NULL
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
    while (inicio != NULL) { //Enquanto inicio é diferente de NULL
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
