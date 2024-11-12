using EmployeePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeePortal.Controllers
{
    public class EmployeeController : Controller
    {
        string Connection = "Data Source=LAPTOP-CS5V8EVD\\SQLEXPRESS;Initial Catalog=EmployeeDataBase;Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True;";
        // GET: EmployeeController
        public ActionResult Index()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();

            EmployeeModel employeeModel = null;

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = Connection;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "ListEmployeeDetails";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlCon;

            sqlCon.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    employeeModel = new EmployeeModel();

                    employeeModel.EmpId = int.Parse(sqlDataReader["EmpId"].ToString());
                    employeeModel.Name = sqlDataReader["Name"].ToString();
                    employeeModel.Team = sqlDataReader["Team"].ToString();
                    employeeModel.Project = sqlDataReader["Project"].ToString();
                    employeeModel.City = sqlDataReader["City"].ToString();
                    employeeModel.Phone = sqlDataReader["Phone"].ToString();
                    employeeModel.JoiningDate = DateTime.Parse(sqlDataReader["JoiningDate"].ToString());

                    //Adding model details to List
                    employeeList.Add(employeeModel);
                }

            }
            sqlCon.Close();


            return View(employeeList);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmpInsert";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;

                sqlCommand.Parameters.AddWithValue("@Name",employeeModel.Name);
                sqlCommand.Parameters.AddWithValue("@Team", employeeModel.Team);
                sqlCommand.Parameters.AddWithValue("@Project", employeeModel.Project);
                sqlCommand.Parameters.AddWithValue("@City", employeeModel.City);
                sqlCommand.Parameters.AddWithValue("@Phone", employeeModel.Phone);
                sqlCommand.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);

                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
       
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmpUpdate";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;

                sqlCommand.Parameters.AddWithValue("@EmpId",id);
                sqlCommand.Parameters.AddWithValue("@Name", employeeModel.Name);
                sqlCommand.Parameters.AddWithValue("@Team", employeeModel.Team);
                sqlCommand.Parameters.AddWithValue("@Project", employeeModel.Project);
                sqlCommand.Parameters.AddWithValue("@City", employeeModel.City);
                sqlCommand.Parameters.AddWithValue("@Phone", employeeModel.Phone);
                sqlCommand.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);

                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            EmployeeModel employeeModel = GetEmployeeDetails(id);
            return View(employeeModel);
      
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeModel employeeModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = Connection;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "EmpDelete";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Connection = sqlCon;

                sqlCommand.Parameters.AddWithValue("@EmpId", id);
       
                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private EmployeeModel GetEmployeeDetails(int id) { 
        
            EmployeeModel employeeModel = new EmployeeModel();

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = Connection;

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "EmployeeDetails";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlCon;

            sqlCommand.Parameters.AddWithValue("@EmpId", id);

            sqlCon.Open();

            SqlDataReader sqlDataReader=sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows) {
                while (sqlDataReader.Read())
                {
                    employeeModel.EmpId = int.Parse(sqlDataReader["EmpId"].ToString()) ;
                    employeeModel.Name = sqlDataReader["Name"].ToString() ;
                    employeeModel.Team = sqlDataReader["Team"].ToString() ;
                    employeeModel.Project = sqlDataReader["Project"].ToString() ;
                    employeeModel.City = sqlDataReader["City"].ToString() ;
                    employeeModel.Phone = sqlDataReader["Phone"].ToString() ;
                    employeeModel.JoiningDate = DateTime.Parse(sqlDataReader["JoiningDate"].ToString()) ;

                }

            }
            sqlCon.Close();
            return employeeModel;


        }
    }
}
