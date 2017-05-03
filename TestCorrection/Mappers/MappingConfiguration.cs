using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Mappers
{
    public class MappingConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {

            cfg.CreateMap<Candidate, CandidateVM>();
            cfg.CreateMap<CandidateVM, Candidate>();

            cfg.CreateMap<Grade, GradeVM>();
            cfg.CreateMap<GradeVM, Grade>();

            cfg.CreateMap<ImageCandidate, ImageCandidateVM>();
            cfg.CreateMap<ImageCandidateVM, ImageCandidate>();

            cfg.CreateMap<Log, LogVM>();
            cfg.CreateMap<LogVM, Log>();

            cfg.CreateMap<Question, QuestionVM>();
            cfg.CreateMap<QuestionVM, Question>();

            cfg.CreateMap<QuestionGrade, QuestionGradeVM>();
            cfg.CreateMap<QuestionGradeVM, QuestionGrade>();

            cfg.CreateMap<QuestionResult, QuestionResultVM>();
            cfg.CreateMap<QuestionResultVM, QuestionResult>();

            cfg.CreateMap<Role, RoleVM>();
            cfg.CreateMap<RoleVM, Role>();

            cfg.CreateMap<Teacher, TeacherVM>();
            cfg.CreateMap<TeacherVM, Teacher>();

            cfg.CreateMap<User, UserVM>();
            cfg.CreateMap<UserVM, User>();
        }
    }
}