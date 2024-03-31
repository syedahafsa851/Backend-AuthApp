using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;
                    
   
    public UserRepository(IMongoDatabase database)

   
    {
        _users = database.GetCollection<User>("Users");
    }

    public async Task InsertUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<User> FindUserByEmailAsync(string email)
    {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> FindUserByIdAsync(ObjectId id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateUserAsync(ObjectId id, User user)
    {
        var result = await _users.ReplaceOneAsync(u => u.Id == id, user);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteUserAsync(ObjectId id)
    {
        var result = await _users.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
    }
}
