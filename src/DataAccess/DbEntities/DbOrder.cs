using System;

namespace DataAccess;

public sealed class DbOrder : DbEntity
{
    private readonly DateTime? _dateOrder;
    private readonly string? _comment;
    private readonly decimal? _price;

    public long Id { get; set; }
    public DateTime DateOrder 
    { 
        get => _dateOrder ?? new DateTime(); 
        init => _dateOrder = value;
    }

    public string Comment 
    {
        get => _comment ?? string.Empty;
        init => _comment = value is {Length : > 0 } ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public decimal Price
    {
        get => _price ?? 0;
        init => _price = value;
    }
    public long ClientId { get; set; }
    
}