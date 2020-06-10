using System;
using System.Collections.Generic;
using System.Text;

namespace BancoSMEM {

	public class Conta : IDados{
		public List<Operacao> operacoes;

		public Conta(Cliente titular, int numero, double saldo, ISacavel categoria) {
			operacoes = new List<Operacao>();
			this.numero = numero;
			this.saldo = saldo;
			Titular = titular;
			this.categoria = categoria;
		}

		public int numero { get; set; }
		public double saldo { get; set; }
		public Cliente Titular { get; set; }
		public ISacavel categoria { get; }
		public Conta direita;
		public Conta esquerda;

		internal bool saque(double valor) {
			return categoria.sacar(valor);
		}

		public bool deposito(double valor) {
			return categoria.depositar(valor);
		}

		public void addOperacao(Operacao operacao) {
			operacoes.Add(operacao);
		}

		public string extrato() {
			var aux = new StringBuilder();
            aux.Append("Número: ").Append(numero.ToString("000"))
                .Append(Environment.NewLine)
                .Append("Saldo atual: ").Append(saldo.ToString("c"))
                .Append(Environment.NewLine);    
            if(this.operacoes.Count > 0) aux.Append("===========Operações===========");
			foreach (var operacao in operacoes) aux.Append(operacao);

			return aux.ToString();
		}

		public double tarifa() {
			return 50;
		}

		public double rendimento() {
			var saida = 0D;
			if (categoria.GetType() == typeof(IRentavel)) {
				var r = categoria as IRentavel;
				saida = r.calcRendimento(saldo);
			}

			return saida;
		}

		public long getID(){
			return this.numero;
		}
	}

}