using Microsoft.AspNetCore.Mvc;
using MyProject.Areas.City.Models;
using MyProject.DAL;
using System.Data;
using System.Data.SqlClient;
using static MyProject.Areas.Country.Models.LOC_CountryModel;
using static MyProject.Areas.State.Models.LOC_StateModel;

namespace StoreProcedureDemoProject.Areas.City.Controllers
{
    [Area("City")]
    [Route("City/{controller}")]
    public class LOC_CityController : Controller
    {
        public IConfiguration Configuration;
        public LOC_CityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region SelectAllCity
        [Route("Index")]
        public IActionResult Index(LOC_CityModel cityModel)
        {
            FillCountryDDL();
            ViewBag.StateList = "";
            TempData["Message"] = "";
            DataTable dt = new DataTable();
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (cityModel.CountryID == 0 && cityModel.StateID == 0 && cityModel.CityName == null && cityModel.CityCode == null)
            {
                dt = locDal.GetAllData(conString, "PR_City_SelectAll");
            }
            else
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                data.Add(nameof(cityModel.CountryID), Convert.ToInt32(cityModel.CountryID));
                data.Add(nameof(cityModel.StateID), Convert.ToInt32(cityModel.StateID));
                data.Add(nameof(cityModel.CityName), cityModel.CityName);
                data.Add(nameof(cityModel.CityCode), cityModel.CityCode);
                ViewBag.StateList = FillStateDropDownCountyWise(Convert.ToInt32(cityModel.CountryID) == 0 ? 0 : cityModel.CountryID);
                dt = locDal.GetSelectedData(conString, "PR_City_Search", data);
            }
            ViewData["Table"] = dt;
            return View("Index");
        }
        #endregion

        #region AddEditCity
        [Route("add")]
        public IActionResult Add(int? CityID)
        {
            FillCountryDDL();
            if (CityID != null)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_City_SelectByPK";
                command.Parameters.AddWithValue("@CityID", CityID);
                SqlDataReader dataReader = command.ExecuteReader();
                LOC_CityModel cityModel = new LOC_CityModel();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cityModel.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                        cityModel.StateID = Convert.ToInt32(dataReader["StateID"]);
                        cityModel.CityName = dataReader["CityName"].ToString();
                        cityModel.CityCode = dataReader["CityCode"].ToString();
                        ViewBag.StateList = FillStateDropDownCountyWise(cityModel.CountryID);
                    }
                }
                conn.Close();
                return View("CityAddEdit", cityModel);
            }
            ViewBag.StateList = "";
            return View("CityAddEdit");
        }
        #endregion

        #region SaveCity
        [HttpPost]
        public IActionResult Save(LOC_CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new Dictionary<string, object?>();
                var properties = cityModel.GetType().GetProperties();
                foreach (var item in properties)
                {
                    dictionary.Add(item.Name, item.GetValue(cityModel));
                }
                string conString = this.Configuration.GetConnectionString("myConnectionString");
                string procedureName = "";
                LOC_DAL locDal = new LOC_DAL();
                if (cityModel.CityID == null)
                {
                    procedureName = "PR_City_Insert";
                }
                else
                {
                    procedureName = "PR_City_UpdateByPk";
                }
                if (locDal.InsertOrUpdateData(conString, procedureName, dictionary))
                {
                    if (cityModel.CityID == null)
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

        #region DeleteCity
        [Route("delete")]
        public IActionResult Delete(int CityID)
        {
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (locDal.DeleteData(conString, "PR_City_DeleteByPK", "CityID", CityID))
            {
                TempData["Message"] = "";
            }
            return RedirectToAction("Index");
        }
        #endregion 
        // Fill Country DropDown
        public void FillCountryDDL()
        {
            List<CountryDropDownModel> list = new List<CountryDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
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
        [Route("CheckDropDown")]
        public IActionResult CheckDropDown(int CountryID)
        {
            var list = FillStateDropDownCountyWise(CountryID);
            return Json(list);
        }
        public List<StateDropDownModel> FillStateDropDownCountyWise(int CountryId)
        {
            List<StateDropDownModel> list = new List<StateDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            if (CountryId != -1)
            {
                command.CommandText = "PR_State_Country_wise";
                command.Parameters.AddWithValue("@CountryID", CountryId);
            }
            else
            {
                command.CommandText = "PR_State_SelectAll";
            }
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    StateDropDownModel stateDropDown = new StateDropDownModel()
                    {
                        StateID = Convert.ToInt32(dataReader["StateID"]),
                        StateName = dataReader["StateName"].ToString() ?? ""
                    };
                    list.Add(stateDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            return list;
        }
    
    }
}
