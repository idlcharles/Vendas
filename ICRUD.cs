using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendas.Models;

namespace vendas
{
    interface ICRUD
    {
        ProdutoModel Criar(ProdutoModel model);
        void Atualizar(ProdutoModel model);
        void Excluir(int id);
        ProdutoModel Buscar(int id);
        List<ProdutoModel> Listar();
    }
}
