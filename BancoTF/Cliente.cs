using System;
using System.Collections.Generic;
using System.Text;

namespace BancoSMEM {

	public abstract class Cliente {
		private int numero;

		protected Cliente(string nome, string cpf, int numero) {
			contas = new List<Conta>();
			this.nome = nome;
			this.cpf = cpf;
			this.numero = numero;
		}

		protected Cliente(string nome, string cpf) {
			contas = new List<Conta>();
			this.nome = nome;
			this.cpf = cpf;
		}

		public string nome { get; set; }
		public string cpf { get; }
		public List<Conta> contas { get; set; }
		public List<Conta> contasOrdenadas { get; set; }
		public Cliente direita;   
		public Cliente esquerda;

		public void addConta(Conta conta) {
			contas.Add(conta);
		}

		public string extrato() {
			var aux = new StringBuilder();
			foreach (var conta in contas) aux.Append(conta.extrato());

			return aux.ToString();
		}

		public void ordenaContas(){
			List<Conta> contasOrdenadas = contas;
			int length = contasOrdenadas.Count;

			for(int i = length - 1 ; i >= 1 ; i--){
				for(int j = 0 ; j < i ; j++){
					if (contasOrdenadas[j].saldoFinal > contasOrdenadas[j + 1].saldoFinal) {
						Conta aux = contasOrdenadas[j];
						contasOrdenadas[j] = contasOrdenadas[j + 1];
						contasOrdenadas[j + 1] = aux;
					}
				}	
			}
			this.contasOrdenadas = contasOrdenadas;
		}

		public virtual double tarifa(int numConta) {
			Conta minhaConta = contas.Find(x => x.numero == numConta);
			foreach (var conta in contas)
				if (conta.numero == numConta)
					return conta.tarifa();

			throw new NullReferenceException("Não foi possível encontrar uma conta de número " +
			                                 numConta.ToString("0000"));
		}

		public virtual double rendimento(int numConta) {
			foreach (var conta in contas)
				if (conta.numero == numConta)
					return conta.rendimento();

			throw new NullReferenceException("Não foi possível encontrar uma conta de número " +
			                                 numConta.ToString("0000"));
		}

		public override string ToString() {
			return "Nome: " + nome + "\nCPF: " + cpf;
		}
	}

}