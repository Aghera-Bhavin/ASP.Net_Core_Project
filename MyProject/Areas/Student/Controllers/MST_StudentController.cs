using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using static MyProject.Areas.Branch.Models.LOC_BranchModel;
using static MyProject.Areas.City.Models.LOC_CityModel;
using MyProject.Areas.Student.Models;
using MyProject.DAL;

namespace StoreProcedureDemoProject.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/{controller}")]
    public class MST_StudentController : Controller
    {
        public IConfiguration Configuration;
        public MST_StudentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region SelectAllStudent
        [Route("Index")]
        public IActionResult Index(MST_StudentModel studentModel)
        {
            FillCityDDL();
            FillBranchDDL();
            TempData["Message"] = "";
            DataTable dt = new DataTable();
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (studentModel.CityID == 0 && studentModel.BranchID == 0 && studentModel.StudentName == null)
            {
                dt = locDal.GetAllData(conString, "PR_Student_SelectAll");
            }
            else
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                data.Add(nameof(studentModel.CityID), Convert.ToInt32(studentModel.CityID));
                data.Add(nameof(studentModel.BranchID), studentModel.BranchID);
                data.Add(nameof(studentModel.StudentName), studentModel.StudentName);
                dt = locDal.GetSelectedData(conString, "PR_Student_Search", data);
            }
            ViewData["Table"] = dt;
            return View("Index");
        }
        #endregion

        #region AddEditStudent
        [Route("add")]
        public IActionResult Add(int? StudentID)
        {
            FillCityDDL();
            FillBranchDDL();
            if (StudentID != null)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Student_SelectByPK";
                command.Parameters.AddWithValue("@StudentID", StudentID);
                SqlDataReader dataReader = command.ExecuteReader();
                MST_StudentModel studentModel = new MST_StudentModel();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        studentModel.StudentName = (string)dataReader["StudentName"];
                        studentModel.BranchID = Convert.ToInt32(dataReader["BranchID"]);
                        studentModel.CityID = Convert.ToInt32(dataReader["CityID"]);
                        studentModel.MobileNoStudent = (string)dataReader["MobileNoStudent"];
                        studentModel.Email = (string)dataReader["Email"];
                        studentModel.MobileNoFather = dataReader["MobileNoFather"].ToString();
                        studentModel.Address = dataReader["Address"].ToString();
                        studentModel.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                        studentModel.Age = Convert.ToInt32(dataReader["Age"]);
                        studentModel.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                        studentModel.Gender = dataReader["Gender"].ToString();
                        studentModel.Password = dataReader["Password"].ToString();
                    }
                }
                conn.Close();
                return View("StudentAddEdit", studentModel);
            }
            return View("StudentAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new Dictionary<string, object?>();
                var properties = studentModel.GetType().GetProperties();
                foreach (var item in properties)
                {
                    dictionary.Add(item.Name, item.GetValue(studentModel));
                }
                string conString = this.Configuration.GetConnectionString("myConnectionString");
                string procedureName = "";
                LOC_DAL locDal = new LOC_DAL();
                if (studentModel.StudentID == null)
                {
                    procedureName = "PR_Student_Insert";
                }
                else
                {
                    procedureName = "PR_Student_UpdateByPk";
                }
                if (locDal.InsertOrUpdateData(conString, procedureName, dictionary))
                {
                    if (studentModel.StudentID == null)
                    {
                        TempData["Message"] = "Record Inserted Successfully.";
                    }
                    else
                    {
                        TempData["Message"] = "Record Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region DeleteCountry
        [Route("delete")]
        public IActionResult Delete(int StudentID)
        {
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (locDal.DeleteData(conString, "PR_Student_DeleteByPK", "StudentID", StudentID))
            {
                TempData["Message"] = "";
            }
            return RedirectToAction("Index");
        }
        #endregion
        public void FillCityDDL()
        {
            List<CityDropDownModel> list = new List<CityDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectAll";
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    CityDropDownModel cityDropDown = new CityDropDownModel()
                    {
                        CityID = Convert.ToInt32(dataReader["CityID"]),
                        CityName = dataReader["CityName"].ToString()
                    };
                    list.Add(cityDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.CityList = list;
        }
        public void FillBranchDDL()
        {
            List<BranchDropDownModel> list = new List<BranchDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Branch_SelectAll";
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    BranchDropDownModel branchDropDown = new BranchDropDownModel()
                    {
                        BranchID = Convert.ToInt32(dataReader["BranchID"]),
                        BranchName = dataReader["BranchName"].ToString()
                    };
                    list.Add(branchDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.BranchList = list;
        }
    }
}
