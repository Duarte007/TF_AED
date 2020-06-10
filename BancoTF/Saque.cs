using System;

namespace BancoSMEM {

	public class Saque : Operacao {
		public Saque(double valor, DateTime data, int numConta, int codOperacao) 
		: base(valor, data, numConta, codOperacao) {
		}

		public override bool atualizar(Conta conta) {
			return conta.saque(valor);
		}
	}

}