using CobraWebSite.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraWebSite.DB
{
    public class ProductQueries
    {
        public static async Task<List<MainCategory>> GetMainGategories()
        {
            var SqlQuery = @"SELECT * from maincategory";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = (await conn.Connection.QueryAsync<MainCategory>(SqlQuery)).AsList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<List<MainCategory>> ListMainCategory()
        {
            var SqlQuery = @"select * from maincategory";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = (await conn.Connection.QueryAsync<MainCategory>(SqlQuery)).AsList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<int> AddMainGategories(string namear, string nameen)
        {
            var SqlQuery = @"insert into maincategory (NameAR  , NameEN ) values (@NameAR,@NameEN);SELECT LAST_INSERT_ID();";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var newCatId = await conn.Connection.ExecuteScalarAsync<int>(SqlQuery, new { NameAR = namear, NameEN = nameen });
                    return newCatId;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static async Task<bool> UpdateMainGategories(int id, string namear, string nameen)
        {
            var SqlQuery = @"update maincategory set NameAR  = @NameAR , NameEN  = @NameEN where Id = @Id";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = await conn.Connection.ExecuteAsync(SqlQuery, new { NameAR = namear, NameEN = nameen, Id = id });
                    return result == 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static async Task<bool> DeleteMainGategories(int id)
        {
            var SqlQuery = @"delete from maincategory where Id = @Id";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = await conn.Connection.ExecuteAsync(SqlQuery, new { Id = id });
                    return result == 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static async Task<List<ProductMetaData>> ListProducts(int MainCategoryId, int CategoryType)
        {
            var SqlQuery = @"select p.Id , p.Namear , p.Nameen , i.name as mainImageName
                            from product p 
                            join productimages i on p.Id = i.productId
                            where p.MainCategoryID = @catId and p.MainCategoryType = @catType and i.DisplayOrder = 0";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = (await conn.Connection.QueryAsync<ProductMetaData>(SqlQuery , new { catId = MainCategoryId , catType = CategoryType })).AsList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
