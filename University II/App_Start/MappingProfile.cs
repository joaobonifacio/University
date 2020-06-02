using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_II.DTOs;
using University_II.Models;
using University_II.Models.API;

namespace University_II.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Course, CourseDTO>();
            Mapper.CreateMap<CourseDTO, Course>();

            Mapper.CreateMap<Teacher, TeacherDTO>();
            Mapper.CreateMap<TeacherDTO, Teacher>();

            Mapper.CreateMap<StaffMember, StaffMemberDTO>();
            Mapper.CreateMap<StaffMemberDTO, StaffMember>();

            Mapper.CreateMap<Subject, SubjectDTO>();
            Mapper.CreateMap<SubjectDTO, Subject>();

            Mapper.CreateMap<UniversityStudentsList, UniversityStudentListDTO>();
            Mapper.CreateMap<UniversityStudentListDTO, UniversityStudentsList>();
        }
    }
}