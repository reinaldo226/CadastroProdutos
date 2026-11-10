using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace CadastroProdutos
{
    public partial class Form1 : Form
    {
        private int idSelecionado;

        public Form1()
        {
            InitializeComponent();
            Banco.CriarBanco();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = Banco.Consultar(
                "SELECT Id, Descricao, Preco, Estoque, Categoria FROM Produtos ORDER BY Descricao");
            ExibirProdutos(produtos);
        }

        private void ExibirProdutos(DataTable produtos)
        {
            dgvProdutos.DataSource = produtos;
            dgvProdutos.Columns["Id"].Visible = false;
            dgvProdutos.Columns["Descricao"].HeaderText = "Descrição";
            dgvProdutos.Columns["Preco"].HeaderText = "Preço";
            dgvProdutos.Columns["Estoque"].HeaderText = "Estoque";
            dgvProdutos.Columns["Categoria"].HeaderText = "Categoria";
            dgvProdutos.Columns["Preco"].DefaultCellStyle.Format = "C2";
            dgvProdutos.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            AtualizarTotal(produtos);
        }

        private void AtualizarTotal(DataTable produtos)
        {
            double total = 0;
            foreach (DataRow produto in produtos.Rows)
                total += Convert.ToDouble(produto["Preco"]) * Convert.ToInt32(produto["Estoque"]);

            lblTotalEstoque.Text = "Valor total em estoque: " + total.ToString("C2");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string descricao = txtDescricao.Text.Trim();
            if (descricao.Length == 0)
            {
                MessageBox.Show("Informe a descrição do produto.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Focus();
                return;
            }

            double preco;
            if (!double.TryParse(txtPreco.Text.Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out preco) || preco < 0)
            {
                MessageBox.Show("Informe um preço válido e não negativo.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return;
            }

            int estoque;
            if (!int.TryParse(txtEstoque.Text.Trim(), out estoque) || estoque < 0)
            {
                MessageBox.Show("Informe uma quantidade inteira e não negativa para o estoque.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstoque.Focus();
                return;
            }

            var parametros = new Dictionary<string, object>
            {
                { "@Descricao", descricao },
                { "@Preco", preco },
                { "@Estoque", estoque },
                { "@Categoria", string.IsNullOrWhiteSpace(txtCategoria.Text) ? (object)DBNull.Value : txtCategoria.Text.Trim() }
            };

            if (idSelecionado == 0)
            {
                Banco.ExecutarComando(
                    "INSERT INTO Produtos (Descricao, Preco, Estoque, Categoria) " +
                    "VALUES (@Descricao, @Preco, @Estoque, @Categoria)", parametros);
                MessageBox.Show("Produto incluído com sucesso.", "Cadastro",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                parametros.Add("@Id", idSelecionado);
                Banco.ExecutarComando(
                    "UPDATE Produtos SET Descricao = @Descricao, Preco = @Preco, " +
                    "Estoque = @Estoque, Categoria = @Categoria WHERE Id = @Id", parametros);
                MessageBox.Show("Produto atualizado com sucesso.", "Cadastro",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LimparCampos();
            CarregarProdutos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (idSelecionado == 0)
            {
                MessageBox.Show("Selecione um produto na lista para excluir.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Confirma a exclusão do produto \"" + txtDescricao.Text + "\"?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta != DialogResult.Yes)
                return;

            Banco.ExecutarComando("DELETE FROM Produtos WHERE Id = @Id",
                new Dictionary<string, object> { { "@Id", idSelecionado } });
            LimparCampos();
            CarregarProdutos();
            MessageBox.Show("Produto excluído.", "Cadastro",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busca = txtBuscaCategoria.Text.Trim();
            if (busca.Length == 0)
            {
                CarregarProdutos();
                return;
            }

            var produtos = Banco.Consultar(
                "SELECT Id, Descricao, Preco, Estoque, Categoria FROM Produtos " +
                "WHERE Categoria LIKE @Busca ORDER BY Descricao",
                new Dictionary<string, object> { { "@Busca", "%" + busca + "%" } });
            ExibirProdutos(produtos);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];
            idSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
            txtDescricao.Text = Convert.ToString(linha.Cells["Descricao"].Value);
            txtPreco.Text = Convert.ToDouble(linha.Cells["Preco"].Value).ToString(CultureInfo.CurrentCulture);
            txtEstoque.Text = Convert.ToString(linha.Cells["Estoque"].Value);
            txtCategoria.Text = linha.Cells["Categoria"].Value == DBNull.Value
                ? string.Empty
                : Convert.ToString(linha.Cells["Categoria"].Value);
        }

        private void dgvProdutos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];
            if (linha.Cells["Estoque"].Value != null &&
                Convert.ToInt32(linha.Cells["Estoque"].Value) == 0)
            {
                linha.DefaultCellStyle.BackColor = Color.MistyRose;
                linha.DefaultCellStyle.ForeColor = Color.DarkRed;
            }
            else
            {
                linha.DefaultCellStyle.BackColor = dgvProdutos.DefaultCellStyle.BackColor;
                linha.DefaultCellStyle.ForeColor = dgvProdutos.DefaultCellStyle.ForeColor;
            }
        }

        private void LimparCampos()
        {
            idSelecionado = 0;
            txtDescricao.Clear();
            txtPreco.Clear();
            txtEstoque.Text = "0";
            txtCategoria.Clear();
            txtDescricao.Focus();
        }
    }

    internal static class Banco
    {
        private static readonly string ConnectionString =
            "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "produtos.db") + ";Version=3;";

        public static void CriarBanco()
        {
            using (var conexao = new SQLiteConnection(ConnectionString))
            {
                conexao.Open();
                const string sql = @"
                    CREATE TABLE IF NOT EXISTS Produtos (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Descricao TEXT NOT NULL,
                        Preco REAL NOT NULL,
                        Estoque INTEGER NOT NULL DEFAULT 0,
                        Categoria TEXT
                    );";
                using (var comando = new SQLiteCommand(sql, conexao))
                    comando.ExecuteNonQuery();
            }
        }

        public static void ExecutarComando(string sql, IDictionary<string, object> parametros)
        {
            using (var conexao = new SQLiteConnection(ConnectionString))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    AdicionarParametros(comando, parametros);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static DataTable Consultar(string sql, IDictionary<string, object> parametros = null)
        {
            var tabela = new DataTable();
            using (var conexao = new SQLiteConnection(ConnectionString))
            {
                conexao.Open();
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    AdicionarParametros(comando, parametros);
                    using (var adaptador = new SQLiteDataAdapter(comando))
                        adaptador.Fill(tabela);
                }
            }
            return tabela;
        }

        private static void AdicionarParametros(SQLiteCommand comando, IDictionary<string, object> parametros)
        {
            if (parametros == null)
                return;

            foreach (var parametro in parametros)
                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
        }
    }
}
