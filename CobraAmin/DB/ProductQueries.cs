using CobraAmin.Models;
using CobraEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobraAmin.DB
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

        public static async Task<List<ProductMetaData>> ListProducts(int pageNumber, int PageSize, int MainCategoryId, int CategoryType)
        {
            var SqlQuery = @"select p.Id , p.Namear , p.Nameen , pi.name as mainImageName , o.Nameen as CountryOfOrigin , (pv.`MaterialAvilabilityen`) MaterialAvilability,(psfv.`SurfaceFinishesen`) SurfaceFinishes
                            from product p 
                            join origin o on p.OriginId = o.Id
                            join productimages pi on p.Id = pi.productId
                            join `productmaterialavailability_view` pv on p.Id = pv.Id
                            join `productsurfacefinishes_view` psfv on p.Id = psfv.Id
                            where p.MainCategoryID = @catId and p.MainCategoryType = @catType and pi.DisplayOrder = 0
                            limit " + (pageNumber - 1) * PageSize + "," + PageSize;
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();
                    var result = (await conn.Connection.QueryAsync<ProductMetaData>(SqlQuery, new { catId = MainCategoryId, catType = CategoryType })).AsList();
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
