using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoSMEM
{

    public partial class Menu : Form
    {
		LeitorArquivo leitorArquivo = new LeitorArquivo();
        public bool teste1 = false, teste2 = false, teste3 = false;
        public string dadosCliente, dadosConta, dadosOp;
        public Menu()
        {
            InitializeComponent();
        }

        //Codigo para iniciar o Tread e previnir o bug System.Threading.ThreadStateException
        public void ThreadStateExceptionSolution()
        {
            var runapp = new System.Threading.ThreadStart(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Menu());
            });

            var thread = new System.Threading.Thread(runapp);
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Logo feita pelo proprio grupo
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            gbxArquivos.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e) {
            if (teste1 && teste2 && teste3) {
                btnBuscar.Visible = true;
				//Cliente
                leitorArquivo.createCustomer(dadosCliente);
				//Conta                
                leitorArquivo.createAccount(dadosConta);
			    //Operacoes
			    leitorArquivo.createOperation(dadosOp);

            }
            else
                MessageBox.Show("Favor inserir todos os arquivos");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            gbxArquivos.Visible = false;
            gbxBuscar.Visible = true;
            gbxCliente.Visible = true;
            gbxContas.Visible = true;
            groupExtrato.Visible = true;
			grpOperacao.Visible = true;

		}

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e) {
            try {
               
                string cpf = txtBuscarcpf.Text;

                Cliente cliente = leitorArquivo.encontraCliente(Convert.ToInt64(cpf));
                txtClientecpf.Text = cliente.cpf;
                txtClienteNome.Text = cliente.nome;
                txtClienteTipo.Text = cliente.GetType().Name;

                 int conta = int.Parse(txtConta.Text);

                 Conta contas = leitorArquivo.encontraConta(conta);
                 txtContaNumero.Text = contas.numero.ToString();
                 txtContaTipo.Text = contas.categoria.GetType().Name;
                 txtContaSaldoInicial.Text = contas.saldo.ToString("c");

            } catch (ArgumentNullException err) {
                MessageBox.Show("Digite o CPF para fazer a pesquisa");
            } catch (Exception err) {
                MessageBox.Show("Erro não definido.");
            }
        }

        private void button2_Click_1(object sender, EventArgs e) {
            try {
                LeitorArquivo leitorArquivo2 = new LeitorArquivo();
                int conta = int.Parse(txtConta.Text);

                Conta contas = leitorArquivo2.encontraConta(conta);
            } catch (Exception) {

            }
        }

        private void TxtExtratoExibir_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click_2(object sender, EventArgs e)
        {
            txtClientecpf.Text = txtClientecpf.Text = txtClienteNome.Text = txtClienteTipo.Text = txtConta.Text = txtContaNumero.Text = txtContaSaldoInicial.Text = txtExtratoExibir.Text = txtContaTipo.Text = "";  
        }

        private void GbxArquivos_Enter(object sender, EventArgs e)
        {

        }

		private void btnMostrarExtrato_Click(object sender, EventArgs e)
		{
			int conta = int.Parse(txtConta.Text);
			Conta contas = leitorArquivo.encontraConta(conta);
			txtExtratoExibir.Text = contas.extrato();
		}

		private void button4_Click(object sender, EventArgs e)
        {
            System.Threading.Thread threadFix = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                OpenFileDialog dialogo = new OpenFileDialog();
                dialogo.Title = "Procurar arquivos no computador";
                dialogo.InitialDirectory = @"C:\POO";
                dialogo.Filter = "Arquivos texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                DialogResult resposta = dialogo.ShowDialog();

                if (resposta == DialogResult.OK)
                {
                    this.dadosCliente = dialogo.FileName;
                    teste1 = true;
					btnBuscarCliente.BackColor = Color.LawnGreen; 

                }

                if (resposta == DialogResult.Cancel || resposta == DialogResult.Abort)
                {
                    MessageBox.Show("Insira um arquivo de texto para poder continuar");

                }
            }));

            threadFix.SetApartmentState(System.Threading.ApartmentState.STA);
            threadFix.IsBackground = false;
            threadFix.Start();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            System.Threading.Thread threadFix = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                OpenFileDialog dialogo = new OpenFileDialog();
                dialogo.Title = "Procurar arquivos para contas de clientes";
                dialogo.InitialDirectory = @"C:\POO";
                dialogo.Filter = "Arquivos texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                DialogResult resposta = dialogo.ShowDialog();

                if (resposta == DialogResult.OK)
                {
                    dadosConta = dialogo.FileName;
                    teste2 = true;
					btnBuscarConta.BackColor = Color.LawnGreen;
                }

                if (resposta == DialogResult.Cancel || resposta == DialogResult.Abort)
                {
                    MessageBox.Show("Insira um arquivo de texto para poder continuar");

                } 
            }));

            threadFix.SetApartmentState(System.Threading.ApartmentState.STA);
            threadFix.IsBackground = false;
            threadFix.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Threading.Thread threadFix = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                OpenFileDialog dialogo = new OpenFileDialog();
                dialogo.Title = "Procurar arquivos no computador";
                dialogo.InitialDirectory = @"C:\POO";
                dialogo.Filter = "Arquivos texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
                DialogResult resposta = dialogo.ShowDialog();

                if (resposta == DialogResult.OK)
                {
                    dadosOp = dialogo.FileName;
                    teste3 = true;
					btnBuscarOperacoes.BackColor = Color.LawnGreen; 

                }

                if (resposta == DialogResult.Cancel || resposta == DialogResult.Abort)
                {
                    MessageBox.Show("Insira um arquivo de texto para poder continuar");

                }
            }));

            threadFix.SetApartmentState(System.Threading.ApartmentState.STA);
            threadFix.IsBackground = false;
            threadFix.Start();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            /*if (teste1 == true)
            {
                pgbArquivos.Value = 33;
            }

            if (teste2 == true)
            {
                pgbArquivos.Value = 66;
            }

            if (teste3 == true)
            {
                pgbArquivos.Value = 100;
            }

            if (pgbArquivos.Value == 100)
            {
                btnBuscar.Visible = true;
                gbxArquivos.Visible = false;
            }

            if (teste3 == false)
            {
                MessageBox.Show("Insira primeiro um arquivo válido");
            }
            */
        }

    }
}
