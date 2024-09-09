using ProjetoEstagio.Models;

namespace ProjetoEstagio.Classes
{
    public class Operacoes
    {
        private readonly DatabaseContext _dbContext;

        public Operacoes(DatabaseContext dbContext)
        {
            _dbContext=dbContext;
        }


        public bool isValido(Fornecedor fornecedor)
        {
            if (_dbContext.Fornecedores.Any(c => c.cnpj == fornecedor.cnpj))
            {
                return false;
            }
            else
                return true;

            //foreach (var c in _dbContext.Fornecedores)
            //{
            //    if(c.cnpj == fornecedor.cnpj)
            //        return false;
            //}
            //return true;
            
        }

        

       
    }
}
