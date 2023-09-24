using Microsoft.AspNetCore.Mvc;
using MyProject.Areas.Country.Models;
using MyProject.DAL;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Areas.Country.Controllers
{
    [Area("Country")]
    [Route("Country/{controller}")]
    public class LOC_CountryController : Controller
    {
        public IConfiguration Configuration;
        public LOC_CountryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region SelectAllCountry
        [Route("Index")]
        public IActionResult Index(LOC_CountryModel? countryModel)
        {
            TempData["Message"] = "";
            DataTable dt = new DataTable();
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (countryModel.CountryName == null && countryModel.CountryCode == null)
            {
                dt = locDal.GetAllData(conString, "PR_Country_SelectAll");
            }
            else
            {
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                data.Add(nameof(countryModel.CountryName), countryModel.CountryName);
                data.Add(nameof(countryModel.CountryCode), countryModel.CountryCode);
                dt = locDal.GetSelectedData(conString, "PR_Country_Search", data);
            }
            ViewData["Table"] = dt;
            return View("Index");
        }
        #endregion

        #region AddEditCountry
        [Route("add")]
        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("myConnectionString"));
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Country_SelectByPK";
                command.Parameters.AddWithValue("@CountryID", CountryID);
                SqlDataReader dataReader = command.ExecuteReader();
                LOC_CountryModel countryModel = new LOC_CountryModel();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        countryModel.CountryName = dataReader["CountryName"].ToString();
                        countryModel.CountryCode = dataReader["CountryCode"].ToString();
                    }
                }
                conn.Close();
                return View("CountryAddEdit", countryModel);
            }
            return View("CountryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CountryModel countryModel)
        {
            if (ModelState.IsValid)
            {
                var dictionary = new Dictionary<string, object?>();
                var properties = countryModel.GetType().GetProperties();
                foreach (var item in properties)
                {
                    dictionary.Add(item.Name, item.GetValue(countryModel));
                }
                string conString = this.Configuration.GetConnectionString("myConnectionString");
                string procedureName = "";
                LOC_DAL locDal = new LOC_DAL();
                if (countryModel.CountryID == null)
                {
                    procedureName = "PR_Country_Insert";
                }
                else
                {
                    procedureName = "PR_Country_UpdateByPk";
                }                
                if (locDal.InsertOrUpdateData(conString, procedureName, dictionary))
                {
                    if (countryModel.CountryID == null)
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
        public IActionResult Delete(int CountryID)
        {
            string conString = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locDal = new LOC_DAL();
            if (locDal.DeleteData(conString, "PR_Country_DeleteByPK", "CountryID", CountryID))
            {
                TempData["Message"] = "";
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
