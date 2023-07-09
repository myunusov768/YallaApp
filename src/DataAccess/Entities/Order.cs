using System;

namespace DataAccess;

public sealed class Order: IBaseEntity
{
    private readonly DateTime? _dateOrder;
    private readonly string? _comment;
    private readonly decimal? _price;

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

    
}