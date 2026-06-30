namespace mvc_client.Models;

public class User
{
    public virtual int Id { get; set; }
    public virtual Client Client { get; set; } = null!;
    public virtual UserRole Role { get; set; }
    public virtual bool IsActive { get; set; }
}