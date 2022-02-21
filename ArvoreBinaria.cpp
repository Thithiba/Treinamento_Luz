#include <stdio.h>
#include <stdlib.h>
#include <iostream>

typedef struct no {
    int valor;
    struct no* esq, * dir; //ponteiro para outro nó, um pra esq e outro dir
}NoArv;

NoArv *inserir_versao_1(NoArv* raiz, int num) { //função que retorna um tipo nó
    if (raiz == NULL) {
        NoArv* aux = new NoArv;
        aux->valor = num;
        aux->esq = NULL;
        aux->dir = NULL;
        return aux;
    }
    else{
        if (num < raiz->valor) {
            raiz->esq = inserir_versao_1(raiz->esq, num);
        }
        else {
            raiz->dir = inserir_versao_1(raiz->dir, num);
        }
        return raiz;
    }
}

int altura(NoArv* raiz) {
    if (raiz == NULL) {
        //É colocado como -1 pois caso ela tenha apenas o nó raiz, o calculo
        //apresentaria o valor 1 apenas com a raiz, sendo que isso ta errado,
        //pois a distancia da raiz pra raiz é 0
        return -1;
    }
    else {
        //Chamadas recursivas somando o valor até a folha mais longe da raiz
        int esquerda = altura(raiz->esq);
        int direita = altura(raiz->dir);
        if (esquerda > direita) {
            return esquerda + 1;
        }
        else {
            return direita + 1;
        }
    }
}

//Imprime os nós da arvore de forma que raiz, no esq, no dir
void imprimir_versao_1(NoArv* raiz) { 
    if (raiz) {
        printf("\t%d\t", raiz->valor);
        imprimir_versao_1(raiz->esq);
        imprimir_versao_1(raiz->dir);
    }
}

//Imprime os nós de forma ordenada
void imprimir_versao_2(NoArv* raiz) { 
    if (raiz) {
        imprimir_versao_2(raiz->esq); //Imprime primeiro os valores a esq
        printf("\t%d\t", raiz->valor); //Imprime raiz
        imprimir_versao_2(raiz->dir);//Imprime valores a dir
    }
}

//Remover nós da árvore 
NoArv* remover(NoArv* raiz, int chave) {
    if (raiz == NULL) {
        printf("Valor não encontrado!\n");
        return NULL;
    }
    //O else procura o nó a remover e também remove o nó
    else {
        if (raiz->valor == chave) {
            //Remove folhas, ou seja, nós sem filhos
            if (raiz->esq == NULL && raiz->dir == NULL) {
                free(raiz);
                printf("Elemento folha removido: %d !\n", chave);
                return NULL;
            }
            //Remove nós que possuam 1 ou 2 filhos
            else{
                //Remove nós que possuam 2 filhos
                //Esse é muito esperto, assim que ele achar o valor que precisa
                //remover, ele irá percorrer os valores a direita do valor a esq
                //dele, até achar um que seja NULL, isso quer dizer que essa folha
                //tem um valor maior que seu nó esq e menor que seu direito, portant
                //eles vão trocar de lugar o valor a deletar e o valor mais a dir
                //do lado esquerdo, pra que fique fácil a remoção desse valor sem
                //interferir na arvore inteira (também é possivel fazer o contrário)
                if (raiz->esq != NULL && raiz->dir != NULL) {
                    NoArv* aux = raiz->esq;
                    while (aux->dir != NULL) {
                        aux = aux->dir;
                    }
                    raiz->valor = aux->valor;
                    aux->valor = chave;
                    raiz->esq = remover(raiz->esq, chave);
                    return raiz;
                }
                //Remove nós que possuam apenas 1 filho
                else {
                    NoArv* aux;
                    if (raiz->esq != NULL) {
                        aux = raiz->esq;
                    }
                    else {
                        aux = raiz->dir;
                    }
                    free(raiz);
                    printf("Elemento com 1 filho removido: %d !\n", chave);
                    return aux;
                }

            }
        }
        //Pesquisa o nó a remover (esse realmente faz isso)
        else {
            //Valor é menor que a raiz, ele procura a esquerda
            if (chave < raiz->valor) {
                raiz->esq = remover(raiz->esq, chave);
            }
            //Valor é maior que a raiz, ele procura a direita
            else {
                raiz->dir = remover(raiz->dir, chave);
            }
            return raiz;
        }
    }
}

int main()
{
    NoArv* raiz = NULL;
    int opcao, valor;

    do {
        printf("\n0 - Sair\n1 - Inserir\n2 - Imprimir\n3 - Remover no\n4 - Altura da arvore\n");
        scanf_s("%d", &opcao);

        switch (opcao) {
            case 1:
                system("cls");
                printf("\nDigite um valor: ");
                scanf_s("%d", &valor);
                raiz = inserir_versao_1(raiz, valor);
                break;
            case 2:
                system("cls");
                printf("\nPrimeira impressao:\n\n");
                imprimir_versao_1(raiz);
                printf("\n");
                printf("\nSegunda impressao:\n\n");
                imprimir_versao_2(raiz);
                printf("\n");
                break;
            case 3:
                system("cls");
                imprimir_versao_2(raiz);
                printf("\nDigite um valor a ser removido: ");
                scanf_s("%d ", &valor);
                raiz = remover(raiz, valor);
                break;
            case 4:
                system("cls");
                printf("\nAltura da arvore: %d", altura(raiz));
                break;
        default:
            if (opcao != 0) {
                printf("\nOpcao invalida\n");
            }
        }
    } while (opcao != 0);   
}
