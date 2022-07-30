using Dapper;
using StudentRecordManagementApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace StudentRecordManagementApp.Services
{
    public class StudentServices : IStudentService
    {
        private readonly IConfiguration _configuration;
        public StudentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Connetionstring = _configuration.GetConnectionString("DBConnection");
            providerName = "System.Data.SqlClient";
        }

        public string Connetionstring { get; }
        public string providerName { get; }

        public IDbConnection Connection
        {
            get
            {

                return new SqlConnection(Connetionstring);
            }
        }

        public string InsertStudent(Student model)
        {
            string result = " ";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var student = dbConnection.Query<Student>("InsertStudentRecord", new { FullName = model.FullName, EmailAddress = model.EmailAddress, City = model.City, CreatedBy = 1 }, commandType: CommandType.StoredProcedure);
                    // var topRow = student.FirstOrDefault();
                    //StudentID	FullName	EmailAddress	City	CreateOn	CreatedBy
                    if (student != null && student.FirstOrDefault().Response == "Saved Successfully")
                    {
                        result = "Saved Successfully";
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
        public List<Student> GetStudentList()
        {
            List<Student> result = new List<Student>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open(); // here
                    result = (List<Student>)dbConnection.Query<Student>("GetStudentsList", commandType: CommandType.StoredProcedure);
                    dbConnection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
                return result;
            }
        }

        public string DeleteStudentRecord(int StudentID)
        {
            string result = " ";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var student = dbConnection.Query<Student>("DeleteStudentRecord", new { StudentID = StudentID }, commandType: CommandType.StoredProcedure).ToList();
                   
                    if (student != null && student.FirstOrDefault().Response == "Deleted Successfully")
                    {
                        result = "Deleted Successfully";
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

        public string UpdateStudentRecord(Student model)
        {
            string result = " ";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var student = dbConnection.Query<Student>("[UpdateStudentRecord]", new { FullName = model.FullName, EmailAddress = model.EmailAddress, City = model.City, StudentID = model.StudentID }, commandType: CommandType.StoredProcedure);
                    // var topRow = student.FirstOrDefault();
                    //StudentID	FullName	EmailAddress	City	CreateOn	CreatedBy
                    if (student != null && student.FirstOrDefault().Response == "Saved Successfully")
                    {
                        result = "Saved Successfully";
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


    public interface IStudentService
    {
        public string InsertStudent(Student model);
        public List<Student> GetStudentList();

        public string DeleteStudentRecord(int StudentID);
        public string UpdateStudentRecord(Student model);

    }
}
