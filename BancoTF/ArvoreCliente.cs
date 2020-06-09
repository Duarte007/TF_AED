using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BancoSMEM
{
	class ArvoreCliente
	{
		private Cliente raiz; // referência à raiz da árvore.


		/// Esse construtor cria uma nova árvore binária de clientes vazia. Para isso,  esse  método  atribui  null  à  raiz  da  árvore.
		public ArvoreCliente()
		{
			raiz = null;
		}

		/// Método booleano que indica se a árvore está vazia ou não.
		/// verdadeiro: se a raiz da árvore for null, o que significa que a árvore está vazia.
		/// falso: se a raiz da árvore não for null, o que significa que a árvore não está vazia.
		public Boolean arvoreVazia()
		{
			/// Se a raiz da árvore  for null, a  árvore  está vazia.  
			if (this.raiz == null)
				return true;
			/// Caso contrário, a árvore não está vazia.
			else return true;
		}

		private Cliente adicionar(Cliente raizArvore, Cliente clienteNovo)
		{
			///  Se  a  raiz  da  árvore  ou  sub-­‐árvore  for  null,  a  árvore  está  vazia e então um novo aluno é inserido.
			if (raizArvore == null) raizArvore = clienteNovo;
			else
			{
				/// Se o número de matrícula do aluno armazenado na raiz da árvore for maior do que o número de matrícula do aluno que deverá ser inserido na árvore:
				///  adicione  esse  aluno  à  sub-­‐árvore  esquerda;  e  atualize  a referência  para  a  sub-­‐árvore  esquerda  modificada.
				if (Convert.ToInt64(raizArvore.cpf) > Convert.ToInt64(clienteNovo.cpf)) raizArvore.esquerda = adicionar(raizArvore.esquerda, clienteNovo);
				else
				{
					/// Se o número de matrícula do aluno armazenado na raiz da árvore for menor do que o número de matrícula do aluno que deverá ser inserido  na árvore:
					///  adicione  esse  aluno  à  sub-­‐árvore  direita; e atualize a referência para a sub-árvore direita modificada.
					if (Convert.ToInt64(raizArvore.cpf) < Convert.ToInt64(clienteNovo.cpf)) raizArvore.direita = adicionar(raizArvore.direita, clienteNovo);
					else
						/// O número de matrícula do aluno armazenado na raiz da árvore é igual ao número de matrícula do aluno que deveria ser inserido na árvore.
						//System.out.println("O aluno " + alunoNovo.nome + ", cuja matrícula é " + alunoNovo.numMatricula + ", já foi inserido anteriormente.");
						//MessageBox.Show("O cliente " + clienteNovo.nome + ", cujo cpf é " + clienteNovo.cpf + ", já foi inserido anteriormente.");
					return raizArvore;
				}
			}
			///  Retorna a raiz atualizada da árvore ou sub-árvore em que o aluno foi adicionado.
			return raizArvore;
		}

		public void inserir(Cliente clienteNovo)
		{
			/// Chama o método recursivo "adicionar", que será responsável por adicionar, o aluno passado como parâmetro, à árvore.
			/// O método "adicionar" receberá,  como  primeiro  parâmetro,  a  raiz atual da árvore; e, como segundo parâmetro, o aluno que deverá ser adicionado à árvore.
			/// Por fim, a raiz atual da árvore é atualizada, com a raiz retornada pelo método "adicionar".
			this.raiz = adicionar(this.raiz, clienteNovo);
		}

		///  Método recursivo responsável por localizar na árvore ou sub-árvore o antecessor do nó que deverá ser retirado.
		/// O antecessor do nó que deverá ser retirado da árvore corresponde
		/// ao nó que armazena o aluno cujo número de matrícula é o maior,
		/// dentre os números de matrícula menores do que o número de matrícula do nó que deverá ser retirado.
		///  Depois  de  ser  localizado  na  árvore  ou  sub-­‐árvore,
		/// o antecessor do nó que deverá ser retirado da árvore o substitui.
		///  Adicionalmente, a árvore ou sub-árvore é atualizada com a remoção do antecessor.
		private Cliente antecessor(Cliente clienteRetirar, Cliente raizArvore)
		{
			/// Se o antecessor do nó que deverá ser retirado da árvore ainda não foi encontrado...
			if (raizArvore.direita != null)
			{
				///  Pesquise o antecessor na sub-árvore direita.
				raizArvore.direita = antecessor(clienteRetirar, raizArvore.direita);
				return raizArvore;
			}
			/// O antecessor do nó que deverá ser retirado da árvore foi encontrado.
			else
			{
				/// O antecessor do nó que deverá ser retirado da árvore o substitui.
				//clienteRetirar.cpf = raizArvore.cpf;
				clienteRetirar.nome = raizArvore.nome;
				/// A raiz da árvore é atualizada com os descendentes à esquerda do antecessor.
				///  Ou  seja,  retira-­‐se  o  antecessor  da  árvore. 
				return raizArvore.esquerda;
			}
		}

		private Cliente retirar(Cliente raizArvore, int cpf)
		{
			///  Se  a  raiz  da  árvore  ou  sub-­‐árvore  for  null,  a  árvore  está  vazia e o aluno, que deveria ser retirado dessa árvore, não foi encontrado.
			///  Nesse  caso,  deve-­‐se  retornar  null. 
			if (raizArvore == null)
			{
				MessageBox.Show("O clienet, cujo cpf é " + cpf + ", não foi encontrado.");
				return raizArvore;
			}
			else
			{
				/// O número de matrícula do aluno armazenado na raiz da árvore é igual ao número de matrícula do aluno que deve ser retirado dessa árvore.
				/// Ou seja, o aluno que deve ser retirado da árvore foi encontrado.
				if (raizArvore.cpf.Equals(cpf))
				{
					/// O nó da árvore que será retirado não possui descendentes à direita.
					/// Nesse caso, os descendentes à esquerda do nó que está sendo  retirado  da  árvore  passarão  a  ser  descendentes  do  nó-­‐pai  do  nó  que  está sendo retirado.
					if (raizArvore.direita == null)
						return (raizArvore.esquerda);
					else
					/// O nó da árvore que será retirado não possui descendentes à esquerda.
					/// Nesse caso, os descendentes à direita do nó que está  sendo  retirado  da  árvore  passarão  a  ser  descendentes  do  nó-­‐pai  do  nó  que está sendo retirado.
					if (raizArvore.esquerda == null)
						return (raizArvore.direita);
					else
					{
						/// O nó que está sendo retirado da árvore possui descendentes à esquerda e à direita.
						/// Nesse caso, o antecessor do nó que está sendo retirado  é  localizado  na  sub-­‐árvore  esquerda  desse  nó.
						/// O antecessor do nó que está sendo retirado da árvore corresponde matrícula é o maior,
						/// ao nó que armazena o aluno cujo número de
						/// dentre os números de matrícula menores do que o número de matrícula do nó que está sendo retirado.
						///  Depois  de  ser  localizado  na  sub-­‐árvore  esquerda do nó que está sendo retirado,
						/// o antecessor desse nó o substitui.
						///  A  sub-­‐árvore  esquerda  do  nó  que  foi  retirado  é atualizada com a remoção do antecessor.
						raizArvore.esquerda = antecessor(raizArvore, raizArvore.esquerda);
						///  Retorna  a  raiz  atualizada  da  árvore  ou  sub-árvore da qual o aluno foi retirado.
						return (raizArvore);
					}
				}
				else
				{
					/// Se o número de matrícula do aluno armazenado na raiz da árvore for maior  do  que  o  número  de  matrícula  do  aluno  que  deverá  ser localizado e retirado da árvore:
					///  pesquise  e  retire  esse  aluno  da  sub-­‐árvore  esquerda. 
					if (Convert.ToInt64(raizArvore.cpf) > cpf)
						raizArvore.esquerda = retirar(raizArvore.esquerda, cpf);
					else
						/// Se o número de matrícula do aluno armazenado na raiz da árvore for menor do que o número de matrícula do aluno que deverá ser localizado e retirado da árvore:
						///  pesquise  e  retire  esse  aluno  da  sub-­‐árvore  direita. 
						raizArvore.direita = retirar(raizArvore.direita, cpf);
					///  Retorna  a  raiz  atualizada  da  árvore  ou  sub-­‐árvore  da qual o aluno foi retirado arn raizArvore;
					return raizArvore;
				}
			}
		}

		public void remover(int matriculaRemover)
		{
			/// Chama o método recursivo "retirar", que será responsável por pesquisar o aluno, passado como parâmetro, na árvore e retirá-lo da árvore.
			/// O método "retirar" receberá, como primeiro parâmetro, a raiz atual da árvore; e, como segundo parâmetro, o aluno que deverá ser localizado e retirado dessa árvore.
			this.raiz = retirar(this.raiz, matriculaRemover);
		}

		private Cliente pesquisar(Cliente raizArvore, long cpf)
		{
			if (raizArvore == null)
			{
				// Se caiu aqui, é porque não encontrou o cliente
				return raizArvore;
			}
			else
			{
				if (raizArvore.cpf.Equals(cpf))
				{
					return raizArvore;
				}
				else
				{
					if (Convert.ToInt64(raizArvore.cpf) > cpf)
						raizArvore.esquerda = pesquisar(raizArvore.esquerda, cpf);
					else
						raizArvore.direita = pesquisar(raizArvore.direita, cpf);
					return raizArvore;
				}
			}
		}

		public Cliente encontrar(long cpf)
		{
			this.raiz = pesquisar(this.raiz, cpf);
			return this.raiz;
		}
	}
}
