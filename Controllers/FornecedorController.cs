using Microsoft.AspNetCore.Mvc;
using ProjetoEstagio.Classes;
using ProjetoEstagio.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetoEstagio.Controllers
{
    public class FornecedorController : Controller
    {

        private readonly DatabaseContext _databaseContext;
        Operacoes _operacoes;

        public FornecedorController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _operacoes = new Operacoes(databaseContext);
        }

        public IActionResult Index()
        {
            var fornecedores = _databaseContext.Fornecedores.ToList();
            return View(fornecedores);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Fornecedor fornecedor)
        {
            try
            {
                if (_operacoes.isValido(fornecedor) == true)
                {
                    var tmpCnpj = fornecedor.cnpj.ToList();
                    var tmpCep = fornecedor.cep.ToList();
                    string[] enderecoArray = fornecedor.endereco.Split(',');

                    for (int i = 0; i < enderecoArray.Length - 2; i++)
                    {
                        if (enderecoArray[i] == "undefined")
                        {
                            ModelState.AddModelError("cep", "Coloque um CEP válido!");
                            return View(fornecedor);

                        }
                    }

                    if (string.IsNullOrWhiteSpace(enderecoArray[4]) || enderecoArray[4] == ",")
                    {
                        ModelState.AddModelError("cep", "O número residencial está vazio!");
                        return View(fornecedor);

                    }

                    else
                    {
                        if (tmpCnpj.Count < 14)
                        {
                            ModelState.AddModelError("cnpj", "");
                            return View(fornecedor);
                        }

                        else
                        {
                            if (tmpCep.Count < 8)
                            {
                                ModelState.AddModelError("cep", "");
                                return View(fornecedor);
                            }

                            else
                            {
                                if (fornecedor.fotoPerfil == null || fornecedor.fotoPerfil == "Escolha uma opção")
                                {
                                    Random random = new Random();
                                    fornecedor.fotoPerfil = random.Next(0, 4).ToString();
                                    fornecedor.fotoPerfil += ".jpg";
                                }
                                else
                                {
                                    fornecedor.fotoPerfil += ".jpg";
                                }
                                _databaseContext.Add(fornecedor);
                                _databaseContext.SaveChanges();

                                return RedirectToAction("Index");
                            }

                            
                        }
                        
                    }

                    
                }
                else
                {
                    ModelState.AddModelError("cnpj", "CNPJ Já cadastrado");
                    return View(fornecedor);
                }
            }
            catch(Exception ex) 
            {

                ModelState.AddModelError("", "Exceção lançada, por favor verifique as colunas!");
                return View(fornecedor);
                
            }
            
        }

        public IActionResult Edit(int id)
        {
            var fornecedor =_databaseContext.Fornecedores.Find(id);

            if(fornecedor == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Title"] = "Update";
                return View("Create", fornecedor);
            }
        }


        [HttpPost]
        public IActionResult Edit(Fornecedor fornecedor)
        {

            if(ModelState.IsValid)  
            {
                var tmpCnpj = fornecedor.cnpj.ToList();
                var tmpCep = fornecedor.cep.ToList();
                string[] enderecoArray = fornecedor.endereco.Split(',');

                for (int i = 0; i < enderecoArray.Length - 2; i++)
                {
                    if (enderecoArray[i] == "undefined")
                    {
                        ModelState.AddModelError("cep", "Coloque um CEP válido!");
                        return View("Create", fornecedor);

                    }
                }

                if (string.IsNullOrWhiteSpace(enderecoArray[4]) || enderecoArray[4] == ",")
                {
                    Trace.WriteLine(enderecoArray[4] + "  ");
                    ModelState.AddModelError("cep", "O número residencial está vazio!");
                    return View("Create", fornecedor);

                }

                else
                {
                    if (tmpCnpj.Count < 14)
                    {
                        ModelState.AddModelError("cnpj", "");
                        return View("Create", fornecedor);
                    }

                    else
                    {
                        if (tmpCep.Count < 8)
                        {
                            ModelState.AddModelError("cep", "");
                            return View("Create", fornecedor);
                        }

                        else
                        {
                            if (fornecedor.fotoPerfil == null || fornecedor.fotoPerfil == "Escolha uma opção")
                            {
                                Random random = new Random();
                                fornecedor.fotoPerfil = random.Next(0, 4).ToString();
                                fornecedor.fotoPerfil += ".jpg";
                            }
                            else
                            {
                                fornecedor.fotoPerfil += ".jpg";
                            }
                            _databaseContext.Fornecedores.Update(fornecedor);
                            _databaseContext.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }

                }
            }
            else
            {
                ModelState.AddModelError("nome", "ModelState inválida");
                return View("Create",fornecedor);
            }

        }

        public IActionResult Delete(int Id)
        {
            var fornecedor = _databaseContext.Fornecedores.Find(Id);

            if(fornecedor == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Title"] = "Delete";
                return View(fornecedor);
            }
            
        }

        [HttpPost]
        public IActionResult Delete(Fornecedor fornecedor)
        {
            _databaseContext.Remove(fornecedor);
            _databaseContext.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
