using System;

namespace BancoSMEM {

	public class Deposito : Operacao {
		public Deposito(double valor, DateTime data) : base(valor, data) {
		}

		public override bool atualizar(Conta conta) {
			return conta.deposito(valor);
		}
	}

}