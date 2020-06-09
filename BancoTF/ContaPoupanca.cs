namespace BancoSMEM {

	public class ContaPoupanca : IRentavel, ISacavel {
		public int numero { get; protected set; }
		public double saldo { get; protected set; }

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
	}

}