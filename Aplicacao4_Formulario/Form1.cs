using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao4_Formulario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Carrega a lista ao iniciar
            btnListar.PerformClick();
        }

        // Botão Incluir
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            // Validação
            if (txtNome.Text.Equals(""))
            {
                MessageBox.Show("Informe o nome do aluno.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (txtMat.Text.Equals(""))
            {
                MessageBox.Show("Informe a matrícula.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMat.Focus();
                return;
            }

            if (txtN1.Text.Equals(""))
            {
                MessageBox.Show("Informe a nota N1.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtN1.Focus();
                return;
            }

            if (txtN2.Text.Equals(""))
            {
                MessageBox.Show("Informe a nota N2.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtN2.Focus();
                return;
            }

            try
            {
                // Calcula a média
                double n1 = double.Parse(txtN1.Text);
                double n2 = double.Parse(txtN2.Text);
                double media = (n1 + n2) / 2;
                txtMedia.Text = media.ToString("F2");

                // Define o status
                string status = media >= 6.0 ? "APROVADO" : "REPROVADO";
                txtStatus.Text = status;

                // Cria o objeto Aluno
                Alunos alunoReg = new Alunos(
                    int.Parse(txtMat.Text),
                    txtNome.Text,
                    n1,
                    n2,
                    media,
                    status
                );

                // Insere no banco
                AlunosDB alunoBD = new AlunosDB();
                alunoBD.IncluirAluno(alunoReg);

                MessageBox.Show("Registro salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa os campos e atualiza a lista
                btnListar.PerformClick();
            }
            catch (FormatException)
            {
                MessageBox.Show("Verifique os campos numéricos (N1 e N2). Use vírgula ou ponto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão Listar
        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                AlunosDB alunoBD = new AlunosDB();
                dataGridView1.DataSource = alunoBD.getAlunos();

                // Ajusta a largura das colunas
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Limpa os campos
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão Sair
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Método para limpar campos
        private void LimparCampos()
        {
            txtMat.Text = "";
            txtNome.Text = "";
            txtN1.Text = "";
            txtN2.Text = "";
            txtMedia.Text = "";
            txtStatus.Text = "";
            txtMat.Focus();
        }

        // Evento para calcular média automaticamente
        private void txtN2_TextChanged(object sender, EventArgs e)
        {
            CalcularMedia();
        }

        private void txtN1_TextChanged(object sender, EventArgs e)
        {
            CalcularMedia();
        }

        private void CalcularMedia()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtN1.Text) && !string.IsNullOrEmpty(txtN2.Text))
                {
                    double n1 = double.Parse(txtN1.Text);
                    double n2 = double.Parse(txtN2.Text);
                    double media = (n1 + n2) / 2;
                    txtMedia.Text = media.ToString("F2");
                    txtStatus.Text = media >= 6.0 ? "APROVADO" : "REPROVADO";
                }
            }
            catch (FormatException)
            {
                // Ignora erros de formatação durante a digitação
            }
        }
    }
}