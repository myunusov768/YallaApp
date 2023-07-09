namespace DataAccess;
/// <summary>
/// Класс описание клиентов
/// <summary>
public sealed class DbClient : DbEntity
{
    private readonly string? _name;
    private readonly string? _lastName;
    private readonly string? _numberPhone;
    private readonly string? _addrecc;
    private readonly string? _email;

    public long Id { get; set; }
    public string Name 
    { 
        get => _name ?? string.Empty; 
        init => _name = value is {Length: > 0} ? 
        value: throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string LastName 
    {
        get => _lastName ?? string.Empty;
        init => _lastName = value is {Length : > 0 } ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string NumberPhone
    {
        get => _numberPhone ?? string.Empty;
        init => _numberPhone = value is{Length : >0}? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public string Address
    {
        get => _addrecc ?? string.Empty;
        init => _addrecc = value is{Length : >0}? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    public string Email
    {
        get => _email ?? string.Empty;
        init => _email = value is{Length : >0}? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    
}