using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Food_delivery_library.About_orders
{

    public class Completed_Cheсk
    {
        public int Id { get; set; }
        public int Check_Id { get; set; }
        public string Check_Admin { get; set; }
        public string Check_User_Phone { get; set; }
        public DateTime Check_Date { get; set; }
        public float Check_Final_Price { get; set; }
    }
  public  class Completed_Chek_Repository : IRepository<Completed_Cheсk>
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["ConSTR"].ConnectionString;
        string connectionString = "Data Source=SQL5104.site4now.net;Initial Catalog=db_a736b5_foodeliverydb123;User Id=db_a736b5_foodeliverydb123_admin;Password=QQddRRvv1";
        public void Create(Completed_Cheсk value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "INSERT INTO  Completed_Check (Check_Id, Check_Admin ,Check_User_Phone, Check_Date, Check_Final_Price)" +
                                 " VALUES(@Check_Id, @Check_Admin, @Check_User_Phone, @Check_Date, @Check_Final_Price)";
                        db.Execute(sqlQuery,
                           new
                           {
                               Check_Id = value.Check_Id,
                               Check_Admin = value.Check_Admin,
                               Check_User_Phone = value.Check_User_Phone,
                               Check_Date = value.Check_Date,
                               Check_Final_Price = value.Check_Final_Price

                           },
                           transaction);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Delete(Completed_Cheсk value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "DELETE FROM Completed_Check WHERE Id = @Id";
                        db.Execute(sqlQuery, new { Id= value.Id }, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public Completed_Cheсk Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Completed_Cheсk> GetColl()
        {
            List<Completed_Cheсk> coll = new List<Completed_Cheсk>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                coll = db.Query<Completed_Cheсk>("SELECT * FROM Completed_Check").ToList();
            }

            return coll;
        }

        public void Update(Completed_Cheсk value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "UPDATE  Completed_Check SET Check_Admin = @Check_Admin ,Check_User_Phone =  @Check_User_Phone," +
                            " Check_Date =  @Check_Date, Check_Final_Price = @Check_Final_Price, Check_Id = @Check_Id" +
                            " WHERE Id = @Id ";
                        db.Execute(sqlQuery,
                           new
                           {
                               Id  = value.Id,
                               Check_Id = value.Check_Id,
                               Check_Admin = value.Check_Admin,
                               Check_User_Phone = value.Check_User_Phone,
                               Check_Final_Price = value.Check_Final_Price,
                               Check_Date = value.Check_Date
                           },
                           transaction);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
