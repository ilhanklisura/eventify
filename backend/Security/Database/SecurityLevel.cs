namespace Eventify.Backend.Security.Database;

/// <summary>Razina pristupa za security filter (čitaj, kreiraj, ažuriraj, briši).</summary>
public class SecurityLevel
{
    public bool Read { get; set; }
    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }

    public bool Readonly() => Read && !Create && !Update && !Delete;
}
