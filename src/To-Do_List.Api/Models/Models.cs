using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.DTOs;

namespace To_Do_List.Api.Models;

public class Response
{
    public string Message { get; set; }
}
public class RequestUserLogin
{
    public string email { get; set; }
    public string password { get; set; }
}

public class RequestUserRegister
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}

public class ResponseLogin
{
    public string token { get; set; }
}


/// <summary>
/// Representa la respuesta paginada de tareas.
/// </summary>
public class PagedResponse<T>
{
    /// <summary>
    /// Número total de elementos.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Número total de páginas.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Página actual.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Tamaño de la página.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Lista de elementos en la página actual.
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}

