namespace BancoSMEM {

	public class ContaInvestimento : ITarifavel, ISacavel, IRentavel {
		public int numero { get; protected set; }
		public double saldo { get; protected set; }
		public Cliente Titular { get; set; }

		public double calcRendimento(double valor) {
			return valor * 0.08;
		}

		public bool sacar(double valor) {
			if (saldo > valor) {
				saldo -= valor;
				return true;
			}

			return false;
		}

		public bool depositar(double valor) {
			if (valor > 0) {
				saldo += valor;
				return true;
			}

			return false;
		}


		public double calcTarifa(double valor) {
			return valor * 0.05;
		}
	}

}