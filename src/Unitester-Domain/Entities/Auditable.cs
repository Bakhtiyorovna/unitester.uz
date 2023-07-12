﻿namespace Unitester_Domain.Entities;

public class Auditable : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
