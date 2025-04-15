using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Core.Domain.DTOs;

/// <summary>
/// Representa una tarea en el sistema.
/// </summary>
public class TodoItemResponseDto
{
    /// <summary>
    /// Identificador único de la tarea.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Título de la tarea.
    /// </summary>

    public string Title { get; init; }

    /// <summary>
    /// Descripcion detallada de la tarea.
    /// </summary>
   
    public string Description { get; init; }

    
    public int? Difficulty { get; init; }

    /// <summary>
    /// Estado actual de la tarea.
    /// </summary>
    /// <remarks>
    /// Los estados posibles son: Realizada, Pendiente, Cancelada
    /// </remarks>
  
    public Status Status { get; init; }
    
    /// <summary>
    /// Fecha de creación de la tarea.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Fecha de la última actualización de la tarea.
    /// </summary>
    public DateTime LastUpdatedDate { get; init; }
}

