using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pap2020.Models
{
    [MetadataType(typeof(UtilizadorMetadata))]
    public partial class Utilizador
    {

    }
    [MetadataType(typeof(DiaMetadata))]
    public partial class Dia
    {

    }
    [MetadataType(typeof(RelatoriosMetadata))]
    public partial class Relatorio
    {

    }
    [MetadataType(typeof(FaltaMetadata))]
    public partial class Falta
    {
       
    }
    [MetadataType(typeof(BloqueioMetaData))]
    public partial class Bloqueio
    {

    }
    [MetadataType(typeof(AvaliaçãoMetaData))]
    public partial class Avaliação
    {

    }
   
}