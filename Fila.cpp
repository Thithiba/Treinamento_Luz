#include <iostream>
#include <stdio.h>
#include <stdlib.h>

typedef struct No { //criei o meu tipo de dado com o struct 
    int valor;
    No* proximo; //ponteiro para um elemento do tipo No, ou seja, o próximo da lista
}No;//nome da minha struct devido a nomeação aqui em conjunto com o typedef
//não é necessário colocar struct toda vez que chama-lo

typedef struct {
    No* inicio;
    int tam;
}Lista;

//Inserir no final da lista
void inserirFim(Lista* lista, int valor) {
    No* no, * novo = (No*)malloc(sizeof(No));
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
        }
        no->proximo = novo;
    }

    lista->tam++;
}

//Remove primeiro no da lista
void removeInicio(Lista* lista) {
    No* noARemover = NULL;

    noARemover = lista->inicio;
    lista->inicio = lista->inicio->proximo;
    free(noARemover);
    lista->tam--;
}

//imprimir a lista
void imprimir(Lista* lista) {
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
        printf("1 - Inserir valor\n2 - Remover valor\n3 - Imprimir valores\n4 - Sair\n");
        scanf_s("%d", &opcao);
        switch (opcao)
        {
        case 1:
            system("cls");
            printf("Digite um valor a ser inserido:");
            scanf_s("%d", &valor);
            inserirFim(&lista, valor);
            system("cls");
            break;
        case 3:
            imprimir(&lista);
            break;
        case 2:
            removeInicio(&lista);
            break;
        case 4:
            system("cls");
            printf("Finalizando...\n");
            break;
        default:
            printf("Opcao invalida!\n");
            break;
        }
    } while (opcao != 4);

    return 0;
}
