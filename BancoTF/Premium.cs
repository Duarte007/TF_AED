namespace BancoSMEM {

	public class Premium : Cliente {
		public Premium(string nome, string cpf) : base(nome, cpf) {
		}

		public override double rendimento(int numConta) {
			return base.rendimento(numConta) * 0.5;
		}

		public override double tarifa(int numConta) {
			return base.tarifa(numConta) / 0.3;
		}
	}

}