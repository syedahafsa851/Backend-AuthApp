using MongoDB.Bson;

public class User
{
    public ObjectId Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public User()
    {
        Email = ""; 
        Password = ""; 
    }
}
