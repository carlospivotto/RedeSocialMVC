using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialMVC.Models
{
    public class Usuario
    {
        //Os campos do Usuário (Domínio) que queremos exibir na tela:
        //(Ainda usando FB como exemplo)
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Sobrenome { get; set; }
        [EmailAddress(ErrorMessage = "Não é um e-mail válido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Não é um telefone válido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataNascimento { get; set; }
    }
}
