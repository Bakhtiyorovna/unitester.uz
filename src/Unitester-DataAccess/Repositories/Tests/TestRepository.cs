using Dapper;
using Unitester_DataAccess.Interfaces.Tests;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Tests;
using Unitester_Domain.Enums;

namespace Unitester_DataAccess.Repositories.Tests;

public class TestRepository : BaseRepository, ITestRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from tests";
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

    //public async Task<long> CountAsync(long id)
    //{
    //    try
    //    {
    //        await _connection.OpenAsync();
    //        string query = $"select count(*) from tests WHERE ";
    //        var result = await _connection.QuerySingleAsync<long>(query);
    //        return result;
    //    }
    //    catch
    //    {
    //        return 0;
    //    }
    //    finally
    //    {
    //        await _connection.CloseAsync();
    //    }
    //}

    public async Task<int> CreateAsync(Test entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.tests( test, direction_id, type, variant_a, variant_b, variant_c, variant_d, right_variant, created_at, updated_at) " +
               $"VALUES( @test , {entity.DirectionId} , @Type, @VariantA, @VariantB, @VariantC, @VariantD, @RightVariant, @CreatedAt, @UpdatedAt); ";
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
            string query = $"DELETE FROM public.tests WHERE id={id};";
            var result = await _connection.ExecuteAsync(query);
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

    public async Task<List<Test>> GetAllDirectionAsync(PaginationParams @params, long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM tests WHERE direction_id= {id} order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Test>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Test>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Test>> GetAllTypeAsync(PaginationParams @params, TestType type)
    {
        try
        {
            await _connection.OpenAsync();
            string typ = "";
            if (Convert.ToString(type) == "Qiyin")
            {
                typ = "0";
            }
            else if (Convert.ToString(type) == "Ortacha")
            {
                typ = "1";
            }
            else if (Convert.ToString(type) == "Oson")
            {
                typ = "2";
            }
            string query = $"SELECT * FROM tests WHERE type = '{typ}' order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Test>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Test>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Test> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM tests where id=@Id";
            var result = await _connection.QuerySingleAsync<Test>(query, new { Id = id });
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

    public async Task<int> UpdateAsync(long id, Test entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE tests " +
                $"SET  test=@test, type=@Type, variant_a=@VariantA, variant_b=@VariantB, variant_c=@VariantC, variant_d=@VariantD, right_variant=@RightVariant, created_at=@CreatedAt, updated_at=@UpdatedAt" +
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
