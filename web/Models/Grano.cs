﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Grano
    {
        public Grano()
        {
            Contrato = new HashSet<Contrato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoGranoId { get; set; }

        public virtual TipoGrano TipoGrano { get; set; }
        public virtual ICollection<Contrato> Contrato { get; set; }
    }
}