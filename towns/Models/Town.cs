using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace towns.Models;

[Index(nameof(Name), IsUnique = true)]
public class Town
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
