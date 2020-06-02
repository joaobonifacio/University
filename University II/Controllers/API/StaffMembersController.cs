using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University_II.DTOs;
using University_II.Models;
using University_II.Services.API;

namespace University_II.Controllers.API
{
    public class StaffMembersController : ApiController
    {
        StaffsService staffsService;

        // GET /api/staffmembers/
        public IHttpActionResult GetStaffMembers()
        {
            staffsService = new StaffsService();

            List<StaffMember> staff = staffsService.GetAllStaffMembers();

            if (staff.Count() == 0)
                return NotFound();

            return Ok(staff.Select(Mapper.Map<StaffMember, StaffMemberDTO>));
        }

        // GET /api/staffmembers/id
        public IHttpActionResult GetStaffMembers(int id)
        {
            staffsService = new StaffsService();

            StaffMember staff = staffsService.GetStaffMemberByID(id);

            if (staff == null)
                return NotFound();

            return Ok(Mapper.Map<StaffMember, StaffMemberDTO>(staff));
        }
    }
}
