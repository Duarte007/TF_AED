namespace BancoSMEM {

	public class ContaCorrente : ISacavel, ITarifavel {
		public double limiteSaque;
		public int numero { get; protected set; }
		public double saldo { get; protected set; }

		public bool sacar(double valor) {
			if (saldo > valor) {
				saldo -= valor;
				return true;
			}

			saldo = saldo - valor - calcTarifa(valor);
			return true;
		}

		public bool depositar(double valor) {
			if (valor > 0) {
				saldo += valor;
				return true;
			}

			return false;
		}

		public double calcTarifa(double valor) {
			var taxa = valor * 0.05;
			return taxa;
		}
	}

}