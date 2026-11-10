using System.Drawing;
using System.Windows.Forms;

namespace CadastroProdutos
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDescricao;
        private Label lblPreco;
        private Label lblEstoque;
        private Label lblCategoria;
        private Label lblBuscaCategoria;
        private Label lblTotalEstoque;
        private TextBox txtDescricao;
        private TextBox txtPreco;
        private TextBox txtEstoque;
        private TextBox txtCategoria;
        private TextBox txtBuscaCategoria;
        private Button btnNovo;
        private Button btnSalvar;
        private Button btnExcluir;
        private Button btnBuscar;
        private DataGridView dgvProdutos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblDescricao = new Label();
            this.lblPreco = new Label();
            this.lblEstoque = new Label();
            this.lblCategoria = new Label();
            this.lblBuscaCategoria = new Label();
            this.lblTotalEstoque = new Label();
            this.txtDescricao = new TextBox();
            this.txtPreco = new TextBox();
            this.txtEstoque = new TextBox();
            this.txtCategoria = new TextBox();
            this.txtBuscaCategoria = new TextBox();
            this.btnNovo = new Button();
            this.btnSalvar = new Button();
            this.btnExcluir = new Button();
            this.btnBuscar = new Button();
            this.dgvProdutos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.SuspendLayout();

            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new Point(20, 23);
            this.lblDescricao.Text = "Descrição:";
            this.txtDescricao.Location = new Point(105, 19);
            this.txtDescricao.Size = new Size(350, 23);
            this.txtDescricao.Name = "txtDescricao";

            this.lblPreco.AutoSize = true;
            this.lblPreco.Location = new Point(475, 23);
            this.lblPreco.Text = "Preço:";
            this.txtPreco.Location = new Point(525, 19);
            this.txtPreco.Size = new Size(120, 23);
            this.txtPreco.Name = "txtPreco";

            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Location = new Point(665, 23);
            this.lblEstoque.Text = "Estoque:";
            this.txtEstoque.Location = new Point(725, 19);
            this.txtEstoque.Size = new Size(100, 23);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Text = "0";

            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new Point(20, 62);
            this.lblCategoria.Text = "Categoria:";
            this.txtCategoria.Location = new Point(105, 58);
            this.txtCategoria.Size = new Size(350, 23);
            this.txtCategoria.Name = "txtCategoria";

            this.lblBuscaCategoria.AutoSize = true;
            this.lblBuscaCategoria.Location = new Point(475, 62);
            this.lblBuscaCategoria.Text = "Buscar categoria:";
            this.txtBuscaCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtBuscaCategoria.Location = new Point(580, 58);
            this.txtBuscaCategoria.Size = new Size(245, 23);
            this.txtBuscaCategoria.Name = "txtBuscaCategoria";

            this.btnNovo.Location = new Point(20, 100);
            this.btnNovo.Size = new Size(95, 32);
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            this.btnSalvar.Location = new Point(125, 100);
            this.btnSalvar.Size = new Size(95, 32);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            this.btnExcluir.Location = new Point(230, 100);
            this.btnExcluir.Size = new Size(95, 32);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);

            this.btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnBuscar.Location = new Point(730, 100);
            this.btnBuscar.Size = new Size(95, 32);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.AllowUserToDeleteRows = false;
            this.dgvProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutos.BackgroundColor = SystemColors.Window;
            this.dgvProdutos.Location = new Point(20, 150);
            this.dgvProdutos.MultiSelect = false;
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdutos.Size = new Size(805, 405);
            this.dgvProdutos.CellClick += new DataGridViewCellEventHandler(this.dgvProdutos_CellClick);
            this.dgvProdutos.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.dgvProdutos_RowPrePaint);

            this.lblTotalEstoque.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lblTotalEstoque.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalEstoque.Location = new Point(20, 568);
            this.lblTotalEstoque.Size = new Size(805, 25);
            this.lblTotalEstoque.Text = "Valor total em estoque: R$ 0,00";
            this.lblTotalEstoque.TextAlign = ContentAlignment.MiddleRight;

            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(845, 610);
            this.MinimumSize = new Size(700, 500);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.txtCategoria);
            this.Controls.Add(this.lblBuscaCategoria);
            this.Controls.Add(this.txtBuscaCategoria);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvProdutos);
            this.Controls.Add(this.lblTotalEstoque);
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Produtos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

