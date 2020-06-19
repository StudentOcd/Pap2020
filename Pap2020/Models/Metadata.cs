﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pap2020.Models
{
    public class UtilizadorMetadata
    {


        [Required]
        [Display(Name = "Utilizador")]
        public string nome_utilizador { get; set; }

        [Display(Name = "Email")]
        public string email_utilizador { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string senha_utilizador { get; set; }
        [Display(Name = "Telefone")]
        public string telefone_utilizador { get; set; }
        [Display(Name = "Processo")]
        public string nr_processo { get; set; }
        [Display(Name = "Cargo")]
        public int id_tipo { get; set; }

       
    }
   
    public class RelatoriosMetadata
    {
        [Required]
        [Display(Name="Empresa/Entidade")]
        public string nome_empresa { get; set; }

        [Display(Name = "N. Identificação Fiscal")]
        [RegularExpression(@"\d{9}", ErrorMessage = "O formato do NIF deverá ser composto por 9 dígitos: xxxxxxxxx")]
        public string NIF { get; set; }

        [Required]
        [Display(Name="Email de Empresa")]
        public string email_empresa { get; set; }

        [Required]
        [Display(Name="Aluno")]
        public int id_aluno { get; set; }

        [Required]
        [Display(Name="Professor")]
        public int id_professor { get; set; }

        [Required]
        [Display(Name="Monitor")]
        public int id_monitor { get; set; }

        [Display(Name="Avaliação Final")]
        public double avaliacao { get; set; }
        public int is_locked { get; set; }

        [Display(Name="Aluno")]
        public virtual Utilizador Utilizador { get; set; }

        [Display(Name="Professor")]
        public virtual Utilizador Utilizador1 { get; set; }

        [Display(Name="Monitor")]
        public virtual Utilizador Utilizador2 { get; set; }
    }
    public class FaltaMetadata
    {
        [Display(Name="Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Relatório")]
        public int id_relatorio { get; set; }
    }
    public class DiaMetadata
    {
        [Display(Name = " Data")]
        public System.DateTime data_hora { get; set; }

        [Display(Name =" Atividade")]
        public string conteudo { get; set; }
        public int id_relatorio { get; set; }
    }
    public class BloqueioMetaData
    {
        [Display(Name = " Data")]
        public DateTime dia { get; set; }
        [Display(Name="Pode Editar?")]
        public int is_locked { get; set; }
        public int id_relatorio { get; set; }
    }
}
