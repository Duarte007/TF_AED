using System;

namespace BancoSMEM {

	public class Rendimento : Operacao {
		public Rendimento(double valor, DateTime data, int numConta, int codOperacao) 
		: base(valor, data, numConta, codOperacao) {
		}

		public override bool atualizar(Conta conta) {
			if (conta.rendimento() > 0) return true;

			return false;
		}
	}

}