using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vendas.DAO;
using vendas.Models;

namespace vendas
{
    public partial class Form1 : Form
    {
        ProdutoDAO produtoDAO;
        public Form1()
        {
            InitializeComponent();
            produtoDAO = new ProdutoDAO();
        }

        private void btnCadProduto_Click(object sender, EventArgs e)
        {
            InserirAlterarProdutosBD();
            Form1_Load(null, null);
        }
        private void VisualizarProdutosSelecionados(int id)
        {
            var produto = new ProdutoModel();
            produto = produtoDAO.Buscar(id);

            txtNome.Text = produto.Nome;
            txtMarca.Text = produto.Marca;
            txtValor.Text = produto.Valor.ToString();
            txtIdProduto.Text = produto.Id.ToString();
            cbCategoria.SelectedItem = produto.Categoria;
        }

        private void InserirAlterarProdutosBD()
        {
            var produto = new ProdutoModel();
            produto.Nome = txtNome.Text;
            produto.Marca = txtMarca.Text;
            produto.Valor = Convert.ToDecimal(txtValor.Text);
            produto.Categoria = cbCategoria.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(txtIdProduto.Text) && Convert.ToInt32(txtIdProduto.Text) > 0)
                produtoDAO.Atualizar(produto);
            else
            {
                produtoDAO.Criar(produto);

                txtIdProduto.Text = produto.Id.ToString();
            }
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtMarca.Text = "";
            txtValor.Text = "";
            txtIdProduto.Text = "";
            cbCategoria.SelectedItem = "";
        }


        private void btnCarregarProduto_Click(object sender, EventArgs e)
        {

            var produto = produtoDAO.Buscar(Convert.ToInt32(txtIdProduto.Text));

            txtNome.Text = produto.Nome;
            txtMarca.Text = produto.Marca;
            txtValor.Text = produto.Valor.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<ProdutoModel> produtos = produtoDAO.Listar();

            gridProdutos.Rows.Clear();
            foreach (var item in produtos)
            {
                gridProdutos.Rows.Add(item.Id, item.Nome, item.Marca, item.Valor, item.Categoria);
            }
        }

        private void gridProdutos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            int id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            VisualizarProdutosSelecionados(id);

        }

    }
}

