using System;
using System.Collections.Generic;
using System.IO;

namespace BancoSMEM {

	public class LeitorArquivo {
		Arvore arvoreCliente = new Arvore();
		Arvore arvoreConta = new Arvore();
		Arvore arvoreOp = new Arvore();
        public string patch1, patch2, patch3;
	

        public void SetArquivos (string patch01, string patch02, string patch03) {
            patch1 = patch01;
            patch2 = patch02;
            patch3 = patch03;
        }

       
		public void createCustomer(string path1) {
			try {
				string cpf, nome;
				var linhas = File.ReadAllLines(path1);
				foreach (var line in linhas) {
					var dados = line.Split(';');
					cpf = dados[0].Replace("-","");
					nome = dados[1];
					var cliente = new Premium(nome, cpf);
					var el = new Elemento(cliente);
					arvoreCliente.inserir(el);
				}
			}
			catch (IOException err) 
            {
				Console.WriteLine("Não foi possível ler os Clientes:");
			}
		}

		public void createOperation(string path2) {
			try {
				var linhas = File.ReadAllLines(path2);
				DateTime data;
				double valor;
				Conta conta = null;
				int tipo, dia, mes, ano, codOp, numConta;
				string[] datas;
				string[] dados;
				foreach (var line in linhas) {
					dados = line.Split(';');
					numConta = int.Parse(dados[0]);
					codOp = int.Parse(dados[1]);
					valor = double.Parse(dados[2]);
					datas = dados[3].Split('/');
					dia = int.Parse(datas[0]);
					mes = int.Parse(datas[1]);
					ano = int.Parse(datas[2]);
					data = new DateTime(ano, mes, dia);
					var operacao = decodificaOp(valor, data, numConta, codOp);
					conta = encontraConta(numConta);
					if (conta != null && operacao != null){
						conta.addOperacao(operacao);
						var el = new Elemento(conta);
						// arvoreOp.inserir(el);
					} 
				}
			}
			catch (IOException) {
				Console.WriteLine("Não foi possível ler as operações:");
			}
		}


		public void createAccount(string path3) {
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
					cliente = encontraCliente(Convert.ToInt64(cpfCliente));

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

					//contas.Add(conta);

					if (conta != null && cliente != null){
						cliente.addConta(conta);
						var el = new Elemento(conta);
						arvoreConta.inserir(el);
					}
				}
			}
			catch (IOException) {
				Console.WriteLine("Não foi possível ler os Clientes:");
			}
		}

		public Cliente encontraCliente(long cpfCliente) {
			//return clientes.Find(cliente => cliente.cpf == cpfCliente);
			return (Cliente) arvoreCliente.encontrar(cpfCliente).dado;
		}

		public Conta encontraConta(int numeroConta) {
			//return contas.Find(conta => conta.numero == numeroConta);
			return (Conta) arvoreConta.encontrar(numeroConta).dado;
		}

		public Cliente decodificaCliente(int num, string nome, string cpf) {
			switch (num) {
				case 2:
					return new Premium(nome, cpf);
			}

			throw new ArgumentException();
		}

		public Operacao decodificaOp(double valor, DateTime data, int numConta, int codOperacao) {
			switch (codOperacao) {
				case 1:
					return new Saque(valor, data, numConta, codOperacao);
				case 0:
					return new Deposito(valor, data, numConta, codOperacao);
				case 2:
					return new Rendimento(valor, data, numConta, codOperacao);
			}

			throw new ArgumentException();
		}
	}

}