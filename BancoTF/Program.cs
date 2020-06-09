using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BancoSMEM {

	internal class Program {
		private static void Main(string[] args) 
        {
			var leitorArquivo = new LeitorArquivo();
			var Contas = leitorArquivo.contas;
			var Clientes = leitorArquivo.clientes;
			var Operacaos = leitorArquivo.operacoes;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());


        }
	}

}