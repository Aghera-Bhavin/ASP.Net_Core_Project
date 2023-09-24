using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MyProject.Areas.State.Models;
using static MyProject.Areas.Country.Models.LOC_CountryModel;
using MyProject.DAL;

namespace MyProject.Areas.State.Controllers
{
    [Area("State")]
    [Route("State/{controller}")]
    //[Route("State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        public IConfiguration Configuration;
        public LOC_StateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region SelectAllState
        [Route("Index")]
        public IActionResult Index(LOC_StateModel stateModel)
        {
            FillCountyDDL();
            TempData["Message"] = "";
            DataTable dt = new DataTable();
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (stateModel.CountryID == null && stateModel.StateName == null && stateModel.StateCode == null)
            {
                dt = locDal.GetAllData(conString, "PR_State_SelectAll");
            }
            else
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                data.Add(nameof(stateModel.CountryID), Convert.ToInt32(stateModel.CountryID));
                data.Add(nameof(stateModel.StateName), stateModel.StateName);
                data.Add(nameof(stateModel.StateCode), stateModel.StateCode);
                dt = locDal.GetSelectedData(conString, "PR_State_Search", data);
            }
            ViewData["Table"] = dt;
            return View("Index");
        }
        #endregion

        #region AddEditState
        [Route("add")]
        public IActionResult Add(int? StateID)
        {
            FillCountyDDL();
            if (StateID != null)
            {
                SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("myConnectionString"));
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_State_SelectByPK";
                command.Parameters.AddWithValue("@StateID", StateID);
                SqlDataReader dataReader = command.ExecuteReader();
                LOC_StateModel stateModel = new LOC_StateModel();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        stateModel.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                        stateModel.StateName = dataReader["StateName"].ToString();
                        stateModel.StateCode = dataReader["StateCode"].ToString();
                    }
                }
                conn.Close();
                return View("StateAddEdit", stateModel);
            }
            return View("StateAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_StateModel stateModel)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new Dictionary<string, object?>();
                var properties = stateModel.GetType().GetProperties();
                foreach (var item in properties)
                {
                    dictionary.Add(item.Name, item.GetValue(stateModel));
                }
                string conString = this.Configuration.GetConnectionString("myConnectionString");
                string procedureName = "";
                LOC_DAL locDal = new LOC_DAL();
                if (stateModel.StateID == null)
                {
                    procedureName = "PR_State_Insert";
                }
                else
                {
                    procedureName = "PR_State_UpdateByPk";
                }
                if (locDal.InsertOrUpdateData(conString, procedureName, dictionary))
                {
                    if (stateModel.StateID == null)
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
        public IActionResult Delete(int StateID)
        {
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (locDal.DeleteData(conString, "PR_State_DeleteByPK", "StateID", StateID))
            {
                TempData["Message"] = "";
            }
            return RedirectToAction("Index");
        }
        #endregion

        // Fill Country DropDown
        public void FillCountyDDL()
        {
            List<CountryDropDownModel> list = new List<CountryDropDownModel>();
            SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("myConnectionString"));
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectAll";
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    CountryDropDownModel countryDropDown = new CountryDropDownModel()
                    {
                        CountryID = Convert.ToInt32(dataReader["CountryID"]),
                        CountryName = dataReader["CountryName"].ToString()
                    };
                    list.Add(countryDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.CountryList = list;
        }
    }
}
