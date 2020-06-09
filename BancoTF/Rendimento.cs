using System;

namespace BancoSMEM {

	public class Rendimento : Operacao {
		public Rendimento(double valor, DateTime data) : base(valor, data) {
		}

		public override bool atualizar(Conta conta) {
			if (conta.rendimento() > 0) return true;

			return false;
		}
	}

}