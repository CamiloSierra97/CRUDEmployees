using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUDSubareas.Models;
using CRUDEmployees.Models;
using CRUDEmployees.Models.WS;

namespace CRUDEmployees.Controllers
{
    public class SubareasController : ApiController
    {
        [HttpGet]
        public Reply GetSubareas(SubareasViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    List<SubareasViewModels> lst = (from s in dBEMPLOYEES.SubArea
                                                 select new SubareasViewModels
                                                 {
                                                     SubareaId = s.SubAreaId,
                                                     SubareaName = s.SubAreaName,
                                                     AreaId = s.AreaId,
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
        public Reply GetSubareaById(SubareasViewModels model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            try
            {
                using (DBEMPLOYEESEntities dBEMPLOYEES = new DBEMPLOYEESEntities())
                {
                    SubArea oSubarea = dBEMPLOYEES.SubArea.Find(model.SubareaId);
                    if (string.IsNullOrWhiteSpace(oSubarea.SubAreaId.ToString()))
                    {
                        oR.result = 0;
                        oR.Message = "Area not found";
                    }
                    else
                    {
                        List<SubareasViewModels> lst = (from s in dBEMPLOYEES.SubArea
                                                        where s.SubAreaId == model.SubareaId
                                                        select new SubareasViewModels
                                                     {
                                                         SubareaId = s.SubAreaId,
                                                         SubareaName = s.SubAreaName,
                                                         AreaId = s.AreaId,
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
