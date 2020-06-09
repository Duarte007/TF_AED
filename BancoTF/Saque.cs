using System;

namespace BancoSMEM {

	public class Saque : Operacao {
		public Saque(double valor, DateTime data) : base(valor, data) {
		}

		public override bool atualizar(Conta conta) {
			return conta.saque(valor);
		}
	}

}