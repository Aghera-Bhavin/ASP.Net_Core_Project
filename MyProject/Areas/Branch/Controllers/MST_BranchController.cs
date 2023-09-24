using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MyProject.Areas.Branch.Models;
using MyProject.DAL;

namespace MyProject.Areas.Branch.Controllers
{
    [Area("Branch")]
    [Route("Branch/{controller}")]
    public class MST_BranchController : Controller
    {
        public IConfiguration Configuration;
        public MST_BranchController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region SelectAllBranch
        [Route("Index")]
        public IActionResult Index(LOC_BranchModel branchModel)
        {
            TempData["Message"] = "";
            DataTable dt = new DataTable();
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (branchModel.BranchName == null && branchModel.BranchCode == null)
            {
                dt = locDal.GetAllData(conString, "PR_Branch_SelectAll");
            }
            else
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                data.Add(nameof(branchModel.BranchName), branchModel.BranchName);
                data.Add(nameof(branchModel.BranchCode), branchModel.BranchCode);
                dt = locDal.GetSelectedData(conString, "PR_Branch_Search", data);
            }
            ViewData["Table"] = dt;
            return View("Index");
        }
        #endregion

        #region AddEditBranch
        [Route("add")]
        public IActionResult Add(int? BranchID)
        {
            if (BranchID != null)
            {
                SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("myConnectionString"));
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Branch_SelectByPK";
                command.Parameters.AddWithValue("@BranchID", BranchID);
                SqlDataReader dataReader = command.ExecuteReader();
                LOC_BranchModel BranchModel = new LOC_BranchModel();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        BranchModel.BranchName = dataReader["BranchName"].ToString();
                        BranchModel.BranchCode = dataReader["BranchCode"].ToString();
                    }
                }
                conn.Close();
                return View("BranchAddEdit", BranchModel);
            }
            return View("BranchAddEdit");
        }
        #endregion

        #region SaveBranch
        [HttpPost]
        public IActionResult Save(LOC_BranchModel branchModel)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new Dictionary<string, object?>();
                var properties = branchModel.GetType().GetProperties();
                foreach (var item in properties)
                {
                    dictionary.Add(item.Name, item.GetValue(branchModel));
                }
                string conString = this.Configuration.GetConnectionString("myConnectionString");
                string procedureName = "";
                LOC_DAL locDal = new LOC_DAL();
                if (branchModel.BranchID == null)
                {
                    procedureName = "PR_Branch_Insert";
                }
                else
                {
                    procedureName = "PR_Branch_UpdateByPk";
                }
                if (locDal.InsertOrUpdateData(conString, procedureName, dictionary))
                {
                    if (branchModel.BranchID == null)
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

        #region DeleteBranch
        [Route("delete")]
        public IActionResult Delete(int BranchID)
        {
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (locDal.DeleteData(conString, "PR_Branch_DeleteByPK", "BranchID", BranchID))
            {
                TempData["Message"] = "";
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}
