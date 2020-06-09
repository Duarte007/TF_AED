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