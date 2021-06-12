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

        public static async Task<List<ProductMetaData>> ListProducts(int pageNumber  , int PageSize , int MainCategoryId, int CategoryType)
        {
            var SqlQuery = @"select p.Id , p.Namear , p.Nameen , i.name as mainImageName , p.keyname
                            from product p 
                            join productimages i on p.Id = i.productId
                            where p.MainCategoryID = @catId and p.MainCategoryType = @catType and i.DisplayOrder = 0
                            limit " + (pageNumber - 1) * PageSize + "," + PageSize;
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

        public static ProductDetails GetProductDetails(string productId, string lowerlang = "en")
        {
            ProductDetails details = null;
            var SqlQuery = @"select p.Id, p.Name" + lowerlang + @" Name,MainCategoryID , MainCategoryType , p.Color" + lowerlang + @" Color  , o.Name"+ lowerlang + @" CountryOfOrigin   , (pv.`MaterialAvilability" + lowerlang + @"`) MaterialAvilability
                            ,(psfv.`SurfaceFinishes" + lowerlang + @"`) SurfaceFinishes
                            from product p 
                            join origin o on p.OriginId = o.Id
                            join `productmaterialavailability_view` pv on p.Id = pv.Id
                            join `productsurfacefinishes_view` psfv on p.Id = psfv.Id
                            where p.keyname = @Id;

                            -- product price
                            select p.Id , price.Price  , plk.UnitName" + lowerlang + @" PriceUnit ,Thickness, description" + lowerlang + @" MaterialDescription
                            from product p 
                            join productprice price on p.Id = price.FkproductId
                            join priceunitlk plk on price.PriceUnit = plk.Id
                            join materialdescriptionlk d on price.FKMaterialDescriptionId = d.Id
                            where p.keyname = @Id;

                            -- Product Images
                            select p.Id , pi.name , pi.DisplayOrder
                            from product p 
                            join productimages pi on p.Id = pi.productId
                            where p.keyname = @Id;

                            -- product videos
                            select p.Id , v.URL 
                            from product p 
                            join productvideos v on p.Id = v.productId
                            where p.keyname = @Id;

                            -- related products
                            select distinct p.Nameen, v.Nameen Name , v.keyname , v.name Imagename
                            from product p
                            join relatedproduct r on p.Id = r.productid
                            join relatedproductdetails_view v on  r.relatedproductid = v.id 
                            where p.keyname like @Id; 
                            ";
            try
            {
                using (var conn = new AppDB())
                {
                    conn.Connection.Open();

                    using (var multi = conn.Connection.QueryMultiple(SqlQuery, new { Id = productId }))
                    {
                        details = multi.Read<ProductDetails>().First();
                        details.PriceDetails = multi.Read<ProductPrice>().ToList();
                        details.Images = multi.Read<ProductImage>().ToList();
                        details.Videos = multi.Read<ProductVideo>().ToList();
                        details.RelatedProducts = multi.Read<RelatedProduct>().ToList();
                    }
                    return details;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
