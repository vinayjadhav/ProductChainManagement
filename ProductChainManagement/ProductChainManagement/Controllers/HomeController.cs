using ProductChainManagement.Models;
using ProductChainManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductChainManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register(int id=0)
        {
            try
            {
                if (id==0)
                {
                    TempData["OperationType"] = "Insert";
                }
                else
                {
                    TempData["OperationType"] = "Update";
                }

                PCMConnection db = new PCMConnection();
                //ClientRegistrationEditViewModel clientReg = new ClientRegistrationEditViewModel();
                var clientReg = new ClientRegistrationEditViewModel();

                //Populating the view bags for all drop downs
                ViewBag.AccountType = new SelectList(db.Account_Type, "Id", "Description", "-Select Account Type-").ToList();
                ViewBag.Gender = new SelectList(db.Genders, "Id", "Description", null).ToList();
                ViewBag.Membership = new SelectList(db.Membership_Type, "Id", "Description", "-Select Membership Type-").ToList();
                if (id == 0)
                {
                    return View();
                }
                else
                {
                    TempData["ClientId"] = id;                    
                    clientReg = db.Database.SqlQuery<ClientRegistrationEditViewModel>("Exec GetClientDetails {0} ", id).SingleOrDefault();
                    return View(clientReg);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Register(ClientRegistrationEditViewModel clientData)
        {
            int ClientId;
            if (TempData["OperationType"].ToString() == "Insert")
            {
                ClientId = clientData.Client_Id;
            }
            else
            {
                ClientId = (int)TempData["ClientId"];
            }
            try
            {
                string sqlQuery;
                using (var db = new PCMConnection())
                    {
                    sqlQuery = "Exec SaveClientDetails " +
                               "@Client_Id={0},@Name={1},@Address={2},@City={3},@State={4},@Pin_Code={5},@DOB={6},@Gender={7},@Phone_No={8},@Ref_Customer_code={9}," +
                               "@Membership_Type={10},@Bank_Name={11},@Bank_Branch={12},@Bank_City={13},@Bank_State={14},@Bank_Pincode={15},@Accout_Type={16},@Account_No={17}," +
                               "@IFSC_Code={18}";

                    var Client_data_after_save = db.Database.SqlQuery<ClientRegistrationDisplayViewModel>(sqlQuery, ClientId, 
                                                 clientData.Client_Name, clientData.Client_Address, clientData.Client_City, clientData.Client_State,clientData.Bank_Pincode,
                                                 clientData.DOB, clientData.Client_Gender, clientData.Client_PhoneNo, clientData.Ref_Cust_Code,
                                                 clientData.Membership_Type, clientData.Bank_Name, clientData.Bank_Branch, clientData.Bank_City,
                                                 clientData.Bank_State, clientData.Bank_Pincode, clientData.Bank_AccountType, clientData.Bank_AccountNo,
                                                 clientData.Bank_IFSC).SingleOrDefault();

                    //Populate the registration form again after saving.
                    //Populating the view bags for all drop downs
                    //ViewBag.AccountType = new SelectList(db.Account_Type, "Id", "Description", Client_data_after_save.Bank_AccountType);
                    //ViewBag.Gender = new SelectList(db.Genders, "Id", "Description", Client_data_after_save.Client_Gender);
                    //ViewBag.Membership = new SelectList(db.Membership_Type, "Id", "Description", Client_data_after_save.Membership_Type);

                    ViewBag.AccountType = new SelectList(db.Account_Type, "Id", "Description").ToList();
                    ViewBag.Gender = new SelectList(db.Genders, "Id", "Description").ToList();
                    ViewBag.Membership = new SelectList(db.Membership_Type, "Id", "Description").ToList();

                    return View("~/Views/Home/ClientRegisterationDisplay.cshtml", Client_data_after_save);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            
            
        }

        [HttpGet]
        public ActionResult Profile()
        {
            ClientSearchViewModel clientSearch = new ClientSearchViewModel();
            using (var db = new PCMConnection())
            {
                clientSearch.Clients = db.ClientMasters.Where(c => c.Active == true).ToList();
            }
            return View("ClientList", clientSearch);
        }

        [HttpPost]
        public ActionResult Profile(ClientSearchViewModel searchName)
        {
            ClientSearchViewModel clientSearch = new ClientSearchViewModel();
            using (var db = new PCMConnection())
            {
                if (searchName.NameSearch == null)
                {
                    clientSearch.Clients = db.ClientMasters.Where(c => c.Active == true).ToList();
                }
                else
                {
                    clientSearch.Clients = db.ClientMasters.Where(c => c.Name.Contains(searchName.NameSearch)).ToList();
                }
            }
            return View("ClientList", clientSearch);
        }

        public ActionResult ViewClientInfo(int id)
        {
            //var clientReg = new ClientRegistrationDisplayViewModel();
            PCMConnection db = new PCMConnection();
            var clientReg = db.Database.SqlQuery<ClientRegistrationDisplayViewModel>("Exec ViewClientDetails {0} ", id).SingleOrDefault();
            return View("~/Views/Home/ClientRegisterationDisplay.cshtml", clientReg);
        }

        public ActionResult ViewClientChain(int id)
        {
            //List <ClientChainViewModel> chain = new List<ClientChainViewModel>();
            var chain = new List<ClientChainViewModel>();
            using (PCMConnection db = new PCMConnection())
            {
                chain = db.Database.SqlQuery<ClientChainViewModel>("Exec GetClientHierarchy {0} ", id).ToList();
                //return View("~/Views/Home/ClientChainView.cshtml", chain);
                return PartialView("~/Views/Home/ClientChainView.cshtml", chain);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TermsConditions()
        {
            return PartialView("~/Views/Home/TermsConditionsView.cshtml");
        }
    }
}