using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalDeNoticias.Models
{
    //botão direito na pasta Models > add > class
    public class Noticia
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public DateTime Data { get; set; }

        public string Conteudo { get; set; }
    }
}