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
        // Route https://localhost:xxxxx/api/Countries Method GET
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

        // Route https://localhost:xxxxx/api/Countries Method POST so we can search by Id
        [HttpPost]
        public Reply GetCountryById(CountriesViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Country oCountry = dBEMPLOYEES.Country.Find(model.CountryId);
                    if (string.IsNullOrWhiteSpace(oCountry.CountryId.ToString()))
                    {
                        oR.result = 0;
                        oR.Message = "Country not found";
                    }
                    else
                    {
                        List<CountriesViewModels> lst = (from a in dBEMPLOYEES.Country
                                                     where a.CountryId == model.CountryId
                                                     select new CountriesViewModels
                                                     {
                                                         CountryId = a.CountryId,
                                                         CountryName = a.CountryName,
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
