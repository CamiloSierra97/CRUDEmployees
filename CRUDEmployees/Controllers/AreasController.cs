using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUDAreas.Models.WS;
using CRUDEmployees.Models;
using CRUDEmployees.Models.WS;

namespace CRUDEmployees.Controllers
{
    public class AreasController : ApiController
    {
        [HttpGet]
        public Reply GetAreas(AreasViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    List<AreasViewModels> lst = (from a in dBEMPLOYEES.Area
                                                 select new AreasViewModels
                                                 {
                                                     AreaId = a.AreaId,
                                                     AreaName = a.AreaName,
                                                     AreaDescription = a.AreaDescription,
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
        public Reply GetAreaById(AreasViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    Area oArea = dBEMPLOYEES.Area.Find(model.AreaId);
                    if(string.IsNullOrWhiteSpace(oArea.AreaId.ToString()))
                    {
                        oR.result = 0;
                        oR.Message = "Area not found";
                    }
                    else
                    {
                        List<AreasViewModels> lst = (from a in dBEMPLOYEES.Area
                                                     where a.AreaId == model.AreaId
                                                     select new AreasViewModels
                                                     {
                                                         AreaId = a.AreaId,
                                                         AreaName = a.AreaName,
                                                         AreaDescription = a.AreaDescription,
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
