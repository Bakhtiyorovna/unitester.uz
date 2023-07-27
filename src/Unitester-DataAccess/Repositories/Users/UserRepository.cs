using Dapper;
using Unitester_DataAccess.Interfaces.Users;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Users;

namespace Unitester_DataAccess.Repositories.Users;
public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from users;";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    public async Task<long> CountRoleAsync(string role)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from users WHERE rol='{role}';";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.users(first_name, last_name, user_name, email, phone_number, salt, password, rol , region, imagi_path, description, created_at, updated_at) " +
                $"VALUES(@FirstName, @LastName, @UserName, @Email, @PhoneNumber,@Salt , @Password, '{entity.Rol.ToString()}',  @Region, @ImagePath, @Description, @CreatedAt, @UpdatedAt); ";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM users WHERE id=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<User>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<User>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<User>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }


    public async Task<User?> GetByconstructionAsync(string phone)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select* from users where phone_number = '{phone}' order by id desc limit 1";
            var data = await _connection.QuerySingleAsync<User>(query);
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users where id=@Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> IStudentcount(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from contest_student id = {id}";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE users " +
                $"SET first_name=@FirstName, last_name=@LastName, user_name=@UserName, email=@Email, phone_number= @PhoneNumber, rol=@Rol, description= @Description, created_at=@CreatedAt, updated_at= @UpdatedAt " +
                $"WHERE id={id};";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
