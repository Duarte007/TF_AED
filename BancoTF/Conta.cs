using System;
using System.Collections.Generic;
using System.Text;

namespace BancoSMEM {

	public class Conta : IDados{
		public List<Operacao> operacoes;

		public Conta(Cliente titular, int numero, double saldoInicial, ISacavel categoria) {
			operacoes = new List<Operacao>();
			this.numero = numero;
			this.saldoInicial = saldoInicial;
			Titular = titular;
			this.categoria = categoria;
		}

		public int numero { get; set; }
		public double saldoInicial { get; set; }
		public double saldoFinal { get; set; }
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
                .Append("Saldo atual: ").Append(saldoInicial.ToString("c"))
                .Append(Environment.NewLine);    
            if(this.operacoes.Count > 0) aux.Append("===========Operações===========\n\n");
			foreach (var operacao in operacoes)
				aux.Append(operacao);

			return aux.ToString();
		}

		public string extratoByDateInterval(string dataIni, string dataFin) {
            String[] splitDtIni = dataIni.Split('/');
            String[] splitDtFin = dataFin.Split('/');
            DateTime dataInicial = new DateTime (
				int.Parse(splitDtIni[2]), 
				int.Parse(splitDtIni[1]), 
				int.Parse(splitDtIni[0])
			);
            DateTime dataFinal = new DateTime (
				int.Parse(splitDtFin[2]), 
				int.Parse(splitDtFin[1]), 
				int.Parse(splitDtFin[0])
			);
			var aux = new StringBuilder();
            aux.Append("Número: ").Append(numero.ToString("000"))
                .Append(Environment.NewLine)
                .Append("Saldo atual: ").Append(saldoInicial.ToString("c"))
                .Append(Environment.NewLine);    
            if(this.operacoes.Count > 0) aux.Append("===========Operações===========\n\n");
			foreach (var operacao in operacoes){
				if(operacao.data <= dataFinal && operacao.data >= dataInicial)
					aux.Append(operacao);
			}

			return aux.ToString();
		}

		public double tarifa() {
			return 50;
		}

		public double rendimento() {
			var saida = 0D;
			if (categoria.GetType() == typeof(IRentavel)) {
				var r = categoria as IRentavel;
				saida = r.calcRendimento(saldoInicial);
			}

			return saida;
		}

		public long getID(){
			return this.numero;
		}

		// 0 - Deposito
		// 1 - Saque 
		// 2 - Rendimento
		public void execOperacoes(){
			this.saldoFinal = this.saldoInicial;
			foreach (var operacao in operacoes) {
				switch (operacao.codOperacao) {
					case 0:
						this.saldoFinal+=operacao.valor;
					break;
					case 1:
						this.saldoFinal-=operacao.valor;
					break;
					case 2:
						this.saldoFinal+=operacao.valor;
					break;
				}
			}
		}

	}

}