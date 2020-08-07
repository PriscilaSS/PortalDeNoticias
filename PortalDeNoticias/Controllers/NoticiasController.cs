using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//para acessar a classe RepositorioNoticias
using PortalDeNoticias.Models;

//para usar o CultureInfo e CompareOptions e realizar a busca com acentos
using System.Globalization;

namespace PortalDeNoticias.Controllers
{
    /*
    Botão direito na pasta Controllers > add > controller. Escolher o template Empty MVC controller para criar vazio
    Mudar o nome do controller para NoticiasController. A palavra Controller no nome deve ser mantido fixo pois assim o ASP.NET MVC identifica que é um controller e vai usar apenas a primeira parte do nome no sistema de rotas
    */
    public class NoticiasController : Controller
    {
        //
        // GET: /Noticias/

        /*
        Já cria o controller com esse método Index, que é o executado por padrão quando acessa esse Controller.
        ActionResult = resultado da chamada de uma action, no caso o método Index. 
        A classe ActionResult encapsula as propriedades que irá retornar para o usuário, pode ser um html, xml, json etc.
         
        Na RouteConfig.cs a url é {controller}/{action}/{id} sendo id opcional, portanto ao exibir a url pode ser:
        Index: domínio/Noticias/Index, domínio/Noticias ou domínio
        Detalhes: domínio/Noticias/Detalhes/1 ou domínio/Noticias/Detalhes?id=1
        Buscar: domínio/Noticias/Buscar?texto=Lorem
        O nome do parâmetro na querystring é o nome do parâmetro na ActionResult, texto não está no RouteConfig.cs portanto deve ser passado como querystring
        */
        public ActionResult Index()
        {
            /*
            return View() vai buscar na pasta views, dentro da pasta com o nome do controller (Noticias), a página com o nome da action (Index)
            
            dica: selecionar o nome do action, botão direito do mouse > add view
            (precisa dar build no projeto primeiro) Marcar "Create a strongly-typed view"
            Model classe, classe que representará os dados que serão listados nessa view (classe Noticia)
            Scaffold template, mudar para list pois serão listadas as notícias nessa view
            
            deve passar a coleção de itens que quer exibir, aqui acessa a propriedade public Noticias da classe RepositorioNoticias
            essa propriedade é uma List de Noticias, a classe List implementa a interface IEnumerable que a view Index.cshtml recebe no início chamado "model"
            */
            return View(RepositorioNoticias.Noticias);
        }

        /*
        Caso o parâmetro id não seja passado na url dá erro pois int não aceita null
        O tipo NullableValueTypes permite receber um null no ValueType int: Nullable<int> id ou o atalho int? id
        public ActionResult Detalhes(int id)
        */
        public ActionResult Detalhes(Nullable<int> id)
        {
            /*
            Também pode ser tratado dessa forma:
            No protocolo HTTP temos o conceito de códigos de status, que indicam o resultado de uma requisição web. 
            Quando acessamos uma página em busca de um recurso que não foi encontrado geralmente a aplicação retorna um erro do tipo 404 (Not Found) 
            */
            /*
            if (id == null)
                return HttpNotFound(); // Id nulo: não existe uma notícia sem id
            */

            /*
            pega o primeiro objeto que corresponde ao filtro recebido, usando a função FirstOrDefault da classe List
            usa expressão lambda: n representa o objeto noticia dentro da List, => é o operador lambda, e o filtro desejado
            a view é criada da mesma forma que a Index, mas o template é details, para exibir os detalhes da notícia
            */
            var noticia = RepositorioNoticias.Noticias.FirstOrDefault(n => n.Id == id);

            /*
            if (noticia == null)
                return HttpNotFound(); // Id inexistente: não existe uma notícia com esse id
            */

            return View(noticia);
        }

        public ActionResult Buscar(string texto = "")
        {
            /*
            Colocando um valor no recebimento do parâmetro (ao declarar o método) faz com que o argumento texto receba por padrão o valor "" (string vazia). 
            Não precisa aqui atribuir o valor padrão caso ele seja nulo.
            */
            //texto = (texto == null) ? "" : texto;


            /*
            retorna uma lista contendo os objetos que atendem ao filtro, usando a função Where da classe List
            usa expressão lambda: n representa o objeto noticia dentro da List, => é o operador lambda, e o filtro desejado
            Contains método da classe string que busca o texto especificado na string, precisa do ToUpper() pois é case sensitive
            a view é criada da mesma forma que a Index
            */
            //var noticias = RepositorioNoticias.Noticias.Where(n => n.Titulo.ToUpper().Contains(texto.ToUpper()) || n.Conteudo.ToUpper().Contains(texto.ToUpper()));

            /*
            O método Contains não oferece opções para ignorar os acentos. 
            O método compareInfo.IndexOf compara se uma string está contida em outra desconsiderando maiúsculas e minúsculas e acentos (definimos isso na variável opcoes). 
            Se a posição for maior que zero, isso indica que o texto está contido no título ou na descrição da notícia. 
            */
            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var opcoes = CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase;
            
            var noticias = RepositorioNoticias.Noticias.Where(n => compareInfo.IndexOf(n.Titulo, texto, opcoes) >= 0 || compareInfo.IndexOf(n.Conteudo, texto, opcoes) >= 0); 


            return View(noticias);
        }
    }
}
