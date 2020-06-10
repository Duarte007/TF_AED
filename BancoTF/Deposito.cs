using System;

namespace BancoSMEM {

	public class Deposito : Operacao {
		public Deposito(double valor, DateTime data, int numConta, int codOperacao) 
		: base(valor, data, numConta, codOperacao) {
		}

		public override bool atualizar(Conta conta) {
			return conta.deposito(valor);
		}
	}

}