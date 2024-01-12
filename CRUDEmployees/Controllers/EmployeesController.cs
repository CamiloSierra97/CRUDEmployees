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
                    List<EmployeeViewModel> lst = (from e in dBEMPLOYEES.Employees
                                                   select new EmployeeViewModel
                                                   {
                                                       FirstName = e.FirstName,
                                                       Surname = e.Surname,
                                                       Email = e.Email,
                                                       DocumentType = e.DocumentType,
                                                       DocumentNumber = e.DocumentNumber,
                                                       DateOfHire = e.DateOfHire,
                                                       Phone = e.Phone,
                                                   }).ToList();

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

            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Employees oEmployee = new Employees();
                    oEmployee.FirstName = model.FirstName;
                    oEmployee.Surname = model.Surname;
                    oEmployee.Email = model.Email;
                    oEmployee.DocumentType = model.DocumentType;
                    oEmployee.DocumentType = model.DocumentType;
                    oEmployee.DateOfHire = model.DateOfHire;
                    oEmployee.Phone = model.Phone;

                    //Validate Area and Subarea
                    var subArea = dBEMPLOYEES.SubArea.Find(model.SubAreaId);
                    var country = dBEMPLOYEES.Country.Find(model.CountryId);

                    if (subArea != null && country != null)
                    {
                        oEmployee.SubAreaId = model.SubAreaId;
                        oEmployee.CountryId = model.CountryId;

                        dBEMPLOYEES.Employees.Add(oEmployee);
                        dBEMPLOYEES.SaveChanges();

                        oR.result = 1;
                        oR.Message = "Employee added succesfully";

                        List<EmployeeViewModel> lst = (from e in dBEMPLOYEES.Employees
                                                       select new EmployeeViewModel
                                                       {
                                                           FirstName = e.FirstName,
                                                           Surname = e.Surname,
                                                           Email = e.Email,
                                                           DocumentType = e.DocumentType,
                                                           DocumentNumber = e.DocumentNumber,
                                                           DateOfHire = e.DateOfHire,
                                                           Phone = e.Phone,
                                                       }).ToList();
                        oR.data = lst;
                    }
                    else
                    {
                        oR.Message = "Invalid SubAreaId or CountryId provided.";
                    }

                }
                
            }
            catch
            {
                oR.Message = "Server error, try again later";
            }

            return oR;
        }

        //#region
    }
}
