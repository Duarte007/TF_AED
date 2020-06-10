using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoSMEM {
	class Arvore {
		private Elemento raiz; // referência à raiz da árvore.


		public Arvore() {
			raiz = new Elemento(null);
		}

		public Boolean arvoreVazia() {
			if (this.raiz == null)
				return true;
			else return true;
		}

		private Elemento adicionar(Elemento raizArvore, Elemento novoElemento) {
			if (raizArvore == null || raizArvore.dado == null) raizArvore = novoElemento;
			else {
				if (raizArvore.dado.getID() > novoElemento.dado.getID())
                    raizArvore.esquerda = adicionar(raizArvore.esquerda, novoElemento);
				else {
					if (raizArvore.dado.getID() < raizArvore.dado.getID())
                        raizArvore.direita = adicionar(raizArvore.direita, novoElemento);
					else
					    return raizArvore;
				}
			}
			return raizArvore;
		}

		public void inserir(Elemento novoElemento) {
			this.raiz = adicionar(this.raiz, novoElemento);
		}

		private Elemento antecessor(Elemento elementoRetirar, Elemento raizArvore) {
			if (raizArvore.direita != null) {
				raizArvore.direita = antecessor(elementoRetirar, raizArvore.direita);
				return raizArvore;
			}
			else {
				elementoRetirar.dado = raizArvore.dado;
				return raizArvore.esquerda;
			}
		}

		private Elemento retirar(Elemento raizArvore, int id) {
			if (raizArvore == null) {
				MessageBox.Show("O elemento, cujo id é " + id + ", não foi encontrado.");
				return raizArvore;
			}
			else {
				if (raizArvore.dado.getID() == id) {
					if (raizArvore.direita == null)
						return (raizArvore.esquerda);
					else
					if (raizArvore.esquerda == null)
						return (raizArvore.direita);
					else {
						raizArvore.esquerda = antecessor(raizArvore, raizArvore.esquerda);
						return (raizArvore);
					}
				}
				else {
					if (raizArvore.dado.getID() > id)
						raizArvore.esquerda = retirar(raizArvore.esquerda, id);
					else
						raizArvore.direita = retirar(raizArvore.direita, id);
					return raizArvore;
				}
			}
		}

		public void remover(int idRemover) {
			this.raiz = retirar(this.raiz, idRemover);
		}

		private Elemento pesquisar(Elemento raizArvore, long id) {
			if (raizArvore == null) 
			{
				return raizArvore;
			}
			else {
				if (raizArvore.dado.getID() == id) 
				{
					return raizArvore;
				}
				else {
					if (raizArvore.dado.getID() > id)
					{
						raizArvore.esquerda = pesquisar(raizArvore.esquerda, id);
					}
					else
					{
						raizArvore.direita = pesquisar(raizArvore.direita, id);
					}
					return raizArvore;
				}
			}
		}

		public Elemento encontrar(long id) {
			this.raiz = pesquisar(this.raiz, id);
			return this.raiz;
		}
	}
}
