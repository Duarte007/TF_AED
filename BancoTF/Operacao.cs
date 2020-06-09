using System;
using System.Text;

namespace BancoSMEM {

	public abstract class Operacao {
		protected Operacao(double valor, DateTime data, int numConta, int codOperacao) {
			this.valor = valor;
			this.data = data;
			this.numConta = numConta;
			this.codOperacao = codOperacao;
		}

		public double valor { get; }
		public DateTime data { get; }
		public int numConta { get; }
		public int codOperacao { get; }
		public Operacao esquerda;
		public Operacao direita;

		public abstract bool atualizar(Conta conta);

		public override string ToString() {
			var aux = new StringBuilder();
            aux.Append("Nome da operação: ")
                .Append(this.GetType().Name)
                .Append(Environment.NewLine)
                .Append("Valor: ").Append(valor.ToString("c"))
                .Append(Environment.NewLine)
                .Append("Data: ").Append(data.ToString("f"))
                .Append(Environment.NewLine);
			return aux.ToString();
		}
	}

}