using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUDEmployees.Models;
using CRUDEmployees.Models.WS;



namespace CRUDEmployees.Controllers
{
    public class EmployeesController : ApiController
    {
        //Authentication Method
        /*    [HttpPost]
            public Reply Login(AccessViewModel model)
            {
                Reply oR = new Reply();
                oR.result = 0;
                try
                {
                    using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                    {
                        var lst = dBEMPLOYEES.Employees.Where(e => e.Email == model.email && e.FirstName == model.password && e.EmployeeId == 1);

                        if (lst.Count() > 0)
                        {
                            oR.result = 1;
                            oR.data = Guid.NewGuid().ToString();

                            Employees oUser = lst.First();
                            oUser.DocumentType = oR.data.ToString();
                            dBEMPLOYEES.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                            dBEMPLOYEES.SaveChanges();

                        }
                        else
                        {
                        oR.Message = "Wrong credentials";
                        }
                    }
                }
                catch (Exception ex)
                {
                    oR.result = 0;
                    oR.Message = "An unexpected error has ocurred";
                }
                  return oR;
            } */

        //Get all Empployees
        [HttpGet]
        public Reply GetAllEmployees(EmployeeViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    List<EmployeeViewModel> lst = List(dBEMPLOYEES);

                    oR.data = lst;
                    oR.result = 1;
                }

            }
            catch (Exception ex)
            {
                oR.Message = "Server error, please try again later.";
                oR.data = ex;
            }
            return oR;
        }

        //Create Employee
        [HttpPost]
        public Reply CreateEmployee(EmployeeViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            //Validations
            if (!Validate(model))
            {
                oR.Message = error;
                return oR;
            }

            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Employees oEmployee = new Employees();
                    oEmployee.EmployeeId = model.EmployeeId;
                    oEmployee.FirstName = model.FirstName;
                    oEmployee.Surname = model.Surname;
                    oEmployee.Email = model.Email;
                    oEmployee.DocumentType = model.DocumentType;
                    oEmployee.DocumentNumber = model.DocumentNumber;
                    oEmployee.DateOfHire = model.DateOfHire;
                    oEmployee.Phone = model.Phone;
                    oEmployee.SubAreaId = model.SubAreaId;
                    oEmployee.CountryId = model.CountryId;
                    oEmployee.IsActive = 1;


                    dBEMPLOYEES.Employees.Add(oEmployee);
                    dBEMPLOYEES.SaveChanges();

                    oR.result = 1;
                    oR.Message = "Employee added succesfully";

                    List<EmployeeViewModel> lst = List(dBEMPLOYEES);
                    oR.data = lst;
                    oR.result = 1;
                    
                }
                
            }
            catch
            {
                oR.Message = "Server error, try again later";
            }

            return oR;
        }

        //Edit Employee
        [HttpPut]
        public Reply UpdateEmployee(EmployeeViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            //Validations
            if (!Validate(model))
            {
                oR.Message = error;
                return oR;
            }
         
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Employees oEmployee = dBEMPLOYEES.Employees.Find(model.EmployeeId);
                    oEmployee.EmployeeId = model.EmployeeId;
                    oEmployee.FirstName = model.FirstName;
                    oEmployee.Surname = model.Surname;
                    oEmployee.Email = model.Email;
                    oEmployee.DocumentType = model.DocumentType;
                    oEmployee.DocumentNumber = model.DocumentNumber;
                    oEmployee.DateOfHire = model.DateOfHire;
                    oEmployee.Phone = model.Phone;
                    oEmployee.SubAreaId = model.SubAreaId;
                    oEmployee.CountryId = model.CountryId;
                    oEmployee.IsActive = model.IsActive;

                    dBEMPLOYEES.Entry(oEmployee).State = System.Data.Entity.EntityState.Modified;
                    dBEMPLOYEES.SaveChanges();

                    oR.result = 1;
                    oR.Message = "Employee edited succesfully";

                    List<EmployeeViewModel> lst = List(dBEMPLOYEES);
                    oR.data = lst;
                    oR.result = 1;

                }

            }
            catch
            {
                oR.Message = "Server error, try again later";
            }

            return oR;
        }

        [HttpPatch]
        public Reply DeleteEmployee(EmployeeViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            //Validations
/*            if (!Validate(model))
            {
                oR.Message = error;
                return oR;
            } */

            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Employees oEmployee = dBEMPLOYEES.Employees.Find(model.EmployeeId);
                    oEmployee.IsActive = 0;

                    dBEMPLOYEES.Entry(oEmployee).State = System.Data.Entity.EntityState.Modified;
                    dBEMPLOYEES.SaveChanges();

                    oR.result = 1;
                    oR.Message = "Employee deleted succesfully";

                    List<EmployeeViewModel> lst = List(dBEMPLOYEES);
                    oR.data = lst;
                    oR.result = 1;

                }

            }
            catch
            {
                oR.Message = "Server error, try again later";
            }

            return oR;
        }

        #region HELPERS

        public string error = "";
        private bool Validate(EmployeeViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.Surname) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.DocumentType) ||
                string.IsNullOrWhiteSpace(model.DateOfHire.ToString()) ||
                string.IsNullOrWhiteSpace(model.SubAreaId.ToString()) ||
                string.IsNullOrWhiteSpace(model.CountryId.ToString()))
            {
                error = "Missing Data";
                return false;
            }
            return true;
        }
        private List<EmployeeViewModel> List(DBEMPLOYEESEntities dBEMPLOYEES)
        {
            List<EmployeeViewModel> lst = (from e in dBEMPLOYEES.Employees
                                           where e.IsActive == 1
                                           select new EmployeeViewModel
                                           {
                                               EmployeeId = e.EmployeeId,
                                               FirstName = e.FirstName,
                                               Surname = e.Surname,
                                               Email = e.Email,
                                               DocumentType = e.DocumentType,
                                               DocumentNumber = e.DocumentNumber,
                                               DateOfHire = e.DateOfHire,
                                               Phone = e.Phone,
                                               SubAreaId = e.SubAreaId,
                                               CountryId = e.CountryId,
                                               IsActive = e.IsActive,
                                           }).ToList();
            return lst;
        }
        #endregion
    }
}
