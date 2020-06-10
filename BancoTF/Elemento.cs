namespace BancoSMEM {
	public class Elemento{
		public IDados dado;
		public Elemento esquerda;
		public Elemento direita;

		public Elemento(IDados dado){
			this.dado = dado;
			this.esquerda=null;
			this.direita=null;
		}
	}
}