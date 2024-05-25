﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreekShoping.ProductAPI.Models.Base;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}
