using System;
using System.Collections.Generic;
using System.IO;

namespace BancoSMEM {

	public class LeitorArquivo {
		ArvoreCliente arvoreCliente = new ArvoreCliente();
		public List<Conta> contas = new List<Conta>();
		public List<Operacao> operacoes = new List<Operacao>();
        public string patch1, patch2, patch3;
	

        public void SetArquivos (string patch01, string patch02, string patch03)
        {
            patch1 = patch01;
            patch2 = patch02;
            patch3 = patch03;
        }

       
		public void leClientes(string path1) {
			try {
				string cpf, nome;
				var linhas = File.ReadAllLines(path1);
				foreach (var line in linhas) {
					var dados = line.Split(';');
					cpf = dados[0].Replace("-","");
					nome = dados[1];
					var cliente = new Premium(nome, cpf);
					arvoreCliente.inserir(cliente);
				}
			}
			catch (IOException) 
            {
				Console.WriteLine("Não foi possível ler os Clientes:");
			}
		}

		public void leOperacoes(string path2) {
			try {
				var linhas = File.ReadAllLines(path2);
				string conta;
				DateTime data;
				double valor;
				int tipo, dia, mes, ano;
				string[] datas;
				string[] dados;
				foreach (var line in linhas) {
					dados = line.Split(';');
					conta = dados[0];
					tipo = int.Parse(dados[1]);
					valor = double.Parse(dados[2]);
					datas = dados[3].Split('/');
					dia = int.Parse(datas[0]);
					mes = int.Parse(datas[1]);
					ano = int.Parse(datas[2]);
					data = new DateTime(ano, mes, dia);
					var operacao = decodificaOp(tipo, valor, data);
					var conta1 = encontraConta(int.Parse(conta));
					operacoes.Add(operacao);
					if (conta1 != null && operacao != null) conta1.addOperacao(operacao);
				}
			}
			catch (IOException) {
				Console.WriteLine("Não foi possível ler as operações:");
			}
		}


		public void leContas(string path3) {
			try {
				var linhas = File.ReadAllLines(path3);
				string cpfCliente;
				int tipo, numero;
				double saldoInicial;
				Cliente cliente = null;
				Conta conta = null;
				string[] dados;
				foreach (var line in linhas) {
					dados = line.Split(';');
					numero = int.Parse(dados[0]);
					tipo = 2; // int.Parse(dados[1]);
					cpfCliente = dados[1].Replace("-", "");
					cliente = encontraCliente(cpfCliente);

					saldoInicial = double.Parse(dados[2]);
					switch (tipo) {
						case 1:
							conta = new Conta(cliente, numero, saldoInicial, new ContaPoupanca());
							break;
						case 2:
							conta = new Conta(cliente, numero, saldoInicial, new ContaInvestimento());
							break;
						case 3:
							conta = new Conta(cliente, numero, saldoInicial, new ContaCorrente());
							break;
					}

					contas.Add(conta);

					if (conta != null && cliente != null) cliente.addConta(conta);
				}
			}
			catch (IOException) {
				Console.WriteLine("Não foi possível ler os Clientes:");
			}
		}

		public Cliente encontraCliente(string cpfCliente) {
			//return clientes.Find(cliente => cliente.cpf == cpfCliente);
			return arvoreCliente.encontrar(Convert.ToInt64(cpfCliente));
		}

		public Conta encontraConta(int numeroConta) {
			return contas.Find(conta => conta.numero == numeroConta);
		}

		public Cliente decodificaCliente(int num, string nome, string cpf) {
			switch (num) {
				case 2:
					return new Premium(nome, cpf);
			}

			throw new ArgumentException();
		}

		public Operacao decodificaOp(int num, double valor, DateTime data) {
			switch (num) {
				case 1:
					return new Saque(valor, data);
				case 0:
					return new Deposito(valor, data);
				case 2:
					return new Rendimento(valor, data);
			}

			throw new ArgumentException();
		}
	}

}