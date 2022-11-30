using System;
using System.Collections.Generic;

namespace Ajmera.Assessment.DL.Models;

public partial class BookMaster
{
    public Guid BookMasterId { get; set; }

    public string Name { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
