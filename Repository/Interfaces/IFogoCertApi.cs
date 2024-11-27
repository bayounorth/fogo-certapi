using FogoCertApi.Paging;
using FogoCertApi.Repository.Models;
using FogoCertApi.Resources;
using Insight.Database;
using System.Data;
using System.Reflection;

namespace FogoCertApi.Repository.Interfaces
{
    [Sql(Schema = "dbo")]
    public interface IFogoCertApi
    {
        //Users

        [Sql("PortalUserLogin", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserLogin(string user_name, string password);

        [Sql("PortalUserGetAll", CommandType.StoredProcedure)]
        Task<List<PortalUser>> PortalUserGetAll();

        [Sql("PortalUserGetById", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserGetById(int id);

        [Sql("PortalUserDeleteById", CommandType.StoredProcedure)]
        Task<List<PortalUser>> PortalUserDeleteById(int id);

        [Sql("ManagePortalUser", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserAdd(PortalUserAdd portalUserAdd);

        [Sql("ManagePortalUser", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserChange(PortalUserUpdate portalUserUpdate);

        [Sql("PortalUserForgotPassword", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserForgotPassword(string user_name);

        [Sql("PortalUserResetPassword", CommandType.StoredProcedure)]
        Task<PortalUser> PortalUserResetPassword(string user_name, string token, string new_password);

    }
}
