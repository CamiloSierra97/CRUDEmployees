using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUDCountries.Models.WS;
using CRUDEmployees.Models;
using CRUDEmployees.Models.WS;

namespace CRUDEmployees.Controllers
{
    public class CountriesController : ApiController
    {
        [HttpGet]
        public Reply GetCountries(CountriesViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    List<CountriesViewModels> lst = (from c in dBEMPLOYEES.Country
                                                 select new CountriesViewModels
                                                 {
                                                     CountryId = c.CountryId,
                                                     CountryName = c.CountryName,
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

        [HttpPost]
        public Reply GetCountryById(CountriesViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Area oArea = dBEMPLOYEES.Area.Find(model.CountryId);
                    if (string.IsNullOrWhiteSpace(oArea.AreaId.ToString()))
                    {
                        oR.result = 0;
                        oR.Message = "Area not found";
                    }
                    else
                    {
                        List<CountriesViewModels> lst = (from c in dBEMPLOYEES.Country
                                                     where c.CountryId == model.CountryId
                                                     select new CountriesViewModels
                                                     {
                                                         CountryId = c.CountryId,
                                                         CountryName = c.CountryName,
                                                     }).ToList();
                        oR.data = lst;
                        oR.result = 1;
                    }

                }

            }
            catch (Exception ex)
            {
                oR.Message = "Server error, please try again later.";
                oR.data = ex;
            }
            return oR;
        }
    }
}
