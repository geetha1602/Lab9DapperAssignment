using Dapper;
using Lab9CrudUsingDapper.Models;
using System.Data;
using System.Data.SqlClient;

namespace Lab9CrudUsingDapper.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IConfiguration _configuration;

        public ProductServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DbConnection");
            providerName = "System.Data.SqlClient";
        }
        public string ConnectionString { get; }
        public string providerName { get; }

        public IDbConnection Connection 
        {
            get { return new SqlConnection(ConnectionString); }
        }    

        public string DeleteProduct(int ProductId)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var prd = dbConnection.Query<Product>("sp_DeleteProducts",
                        new
                        {
                            ProductId = ProductId
                        },
                        commandType: CommandType.StoredProcedure);

                    if (prd != null)
                    {
                        result = "Deleted Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using(IDbConnection dbConnection=Connection)
                {
                    dbConnection.Open();
                    products = dbConnection.Query<Product>("sp_GetAllProducts", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return products;
                }
            }
            catch(Exception ex)
            {
                string errorMsg = ex.Message;
                return products;
            }
        }

        public string InsertProduct(Product product)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var prd = dbConnection.Query<Product>("sp_InsertProducts",
                        new { 
                            Name=product.Name,
                            Category =product.Category,
                            Color = product.Color,
                            UnitPrice = product.UnitPrice,
                            AvailableQuantity = product.AvailableQuantity
                        },
                        commandType: CommandType.StoredProcedure);

                    if (prd != null)
                    {
                        result = "Saved Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public string UpdateProduct(Product product)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var prd = dbConnection.Query<Product>("sp_UpdatetProducts",
                        new
                        {
                            Name = product.Name,
                            Category = product.Category,
                            Color = product.Color,
                            UnitPrice = product.UnitPrice,
                            AvailableQuantity = product.AvailableQuantity,
                            ProductId=product.ProductId
                        },
                        commandType: CommandType.StoredProcedure);

                    if (prd != null)
                    {
                        result = "Updated Sucessfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
    }
}
