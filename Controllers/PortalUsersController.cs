using AutoMapper;
using FogoCertApi.Repository.Interfaces;
using FogoCertApi.Repository.Models;
using FogoCertApi.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace FogoCertApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PortalUsersController : ControllerBase
    {
        private readonly IFogoCertApi _portalUsersRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        ClaimsPrincipal currentUser;
        PortalUser authenticationClaim;

        public PortalUsersController(IFogoCertApi portalUsersRepository, IMapper mapper, IEmailService emailService)
        {
            _portalUsersRepository = portalUsersRepository;
            _mapper = mapper;
            _emailService = emailService;
        }
        private bool Authorized()
        {
            bool ret = false;

            if (authenticationClaim == null)
            {
                currentUser = HttpContext.User;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.UserData))
                {
                    authenticationClaim = JsonConvert.DeserializeObject<PortalUser>(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);
                }
            }
            if (authenticationClaim != null)
            {
                if (authenticationClaim.isAdministrator == true)
                {
                    ret = true;
                }
            }
            return ret;
        }



        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> PortalUserGetById(int id)
        {
            PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
            portalUserViewModel.Result = null;
            portalUserViewModel.Error = null;
            if (!Authorized())
            {
                portalUserViewModel.UnAuthorizedRequest = true;
                portalUserViewModel.Success = false;
                portalUserViewModel.Message = $"You need administrator rights to access the Get EndPoint";
                return Unauthorized(portalUserViewModel);
            }
            portalUserViewModel.UnAuthorizedRequest = false;


            var portalUserModel = await _portalUsersRepository.PortalUserGetById(id);
            if (portalUserModel == null)
            {
                portalUserViewModel.Success = false;
                portalUserViewModel.Message = "No Records Found";
                return NotFound(portalUserViewModel);
            }

            portalUserViewModel.Success = true;
            portalUserViewModel.Message = "success";
            portalUserViewModel.Result = portalUserModel;
            return Ok(portalUserViewModel);
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUsersViewModel))]
        [HttpGet()]
        public async Task<IActionResult> PortalUserGetAll()
        {
            PortalUsersViewModel portalUsersViewModel = new PortalUsersViewModel();
            portalUsersViewModel.Result = null;
            portalUsersViewModel.Error = null;
            if (!Authorized())
            {
                portalUsersViewModel.UnAuthorizedRequest = true;
                portalUsersViewModel.Success = false;
                portalUsersViewModel.Message = $"You need administrator rights to access the GetAll EndPoint";

                return Unauthorized(portalUsersViewModel);
            }
            portalUsersViewModel.UnAuthorizedRequest = false;
            portalUsersViewModel.Success = true;

            var portalUsersModel = await _portalUsersRepository.PortalUserGetAll();
            portalUsersViewModel.Result = portalUsersModel;
            return Ok(portalUsersViewModel);
        }
        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpPost()]
        public async Task<IActionResult> PortalUserAdd(PortalUserAdd portalUserAdd)
        {
            try
            {
                PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
                portalUserViewModel.Result = null;
                portalUserViewModel.Error = null;
                if (!Authorized())
                {
                    portalUserViewModel.UnAuthorizedRequest = true;
                    portalUserViewModel.Success = false;
                    portalUserViewModel.Message = $"You need administrator rights to access the PortalAddUser EndPoint";
                    return Unauthorized(portalUserViewModel);
                }
                portalUserViewModel.UnAuthorizedRequest = false;

                var portalUserModel = await _portalUsersRepository.PortalUserAdd(portalUserAdd);
                portalUserViewModel.Success = true;
                portalUserViewModel.Message = "Added";
                portalUserViewModel.Result = portalUserModel;

                string body = $"<p>Dear {portalUserModel.name},</p><p>Welcome to the Fogo Certification Portal.&nbsp; Please login to the portal by clicking&nbsp;<a href='https://fogocertportal.bigholler.com'>here</a>. with the password that you were given.</p><p>Thank you.</p><p>&nbsp;</p>";
                await _emailService.SendAsync(portalUserModel.user_name, "Welcome to the Fogo Certification Portal!", body, true);
                return Ok(portalUserViewModel);

            }
            catch (Exception ex)
            {
                ApiResponseBase apiResponseBase = new ApiResponseBase();
                apiResponseBase.Success = false;
                apiResponseBase.Message = ex.Message;
                apiResponseBase.UnAuthorizedRequest = false;

                if (ex.Message.Contains("Cannot insert duplicate key row"))
                {
                    return Conflict(apiResponseBase);
                }
                else
                {
                    return BadRequest(apiResponseBase);
                }
            }
        }
        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpPut()]
        public async Task<IActionResult> PortalUserUpdate(PortalUserUpdate portalUserUpdate)
        {
            try
            {
                PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
                portalUserViewModel.Result = null;
                portalUserViewModel.Error = null;
                if (!Authorized())
                {
                    portalUserViewModel.UnAuthorizedRequest = true;
                    portalUserViewModel.Success = false;
                    portalUserViewModel.Message = $"You need administrator rights to access the PortalUserUpdate EndPoint";
                    return Unauthorized(portalUserViewModel);
                }
                portalUserViewModel.UnAuthorizedRequest = false;

                var portalUserModel = await _portalUsersRepository.PortalUserChange(portalUserUpdate);
                portalUserViewModel.Success = true;
                portalUserViewModel.Message = "Updated";
                portalUserViewModel.Result = portalUserModel;

                return Ok(portalUserViewModel);

            }
            catch (Exception ex)
            {
                ApiResponseBase apiResponseBase = new ApiResponseBase();
                apiResponseBase.Success = false;
                apiResponseBase.Message = ex.Message;
                apiResponseBase.UnAuthorizedRequest = false;


                if (ex.Message.Contains("Cannot insert duplicate key row"))
                {
                    return Conflict(apiResponseBase);
                }
                else
                {
                    return BadRequest(apiResponseBase);
                }
            }
        }

        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> PortalUserForgotPassword(string user_name)
        {
            try
            {
                PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
                portalUserViewModel.Result = null;
                portalUserViewModel.Error = null;
                portalUserViewModel.Success = true;

                var portalUser = await _portalUsersRepository.PortalUserForgotPassword(user_name);
                if (portalUser == null)
                {
                    portalUserViewModel.Error = new ApiError();
                    portalUserViewModel.Error.message = "Username not Found!";
                    portalUserViewModel.Message = "Username not Found!";
                    portalUserViewModel.UnAuthorizedRequest = false;
                    return NotFound(portalUserViewModel);
                }

                string body = $"<p>Dear {portalUser.name},</p><p>Please click&nbsp;<a href='https://fogocertportal.bigholler.com/Account/resetpassword?token={portalUser.forgot_password_token}'>here</a>. to reset your password.  This link is only valid for 10 minutes.</p><p>Thank you.</p><p>&nbsp;</p>";
                await _emailService.SendAsync(portalUser.user_name, "Forgot Password requested to the Fogo Certification Portal", body, true);

                portalUserViewModel.Result = portalUser;

                return Ok(portalUserViewModel);

            }
            catch (Exception ex)
            {
                ApiResponseBase apiResponseBase = new ApiResponseBase();
                apiResponseBase.Success = false;
                apiResponseBase.Message = ex.Message;
                apiResponseBase.UnAuthorizedRequest = false;
                return BadRequest(apiResponseBase);
            }
        }

        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> PortalUserResetPassword(PortalUserResetPassword portalUserResetPassword)
        {
            try
            {
                PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
                portalUserViewModel.Result = null;
                portalUserViewModel.Error = null;
                portalUserViewModel.Success = true;
                portalUserViewModel.UnAuthorizedRequest = false;

                var portalUser = await _portalUsersRepository.PortalUserResetPassword(portalUserResetPassword.user_name, portalUserResetPassword.token, portalUserResetPassword.new_password);
                if (portalUser == null)
                {
                    portalUserViewModel.Error = new ApiError();
                    portalUserViewModel.Error.message = "Password NOT reset!";
                    portalUserViewModel.Message = "Password NOT reset!";
                    return Ok(portalUserViewModel);
                }
                string body = $"<p>Dear {portalUser.name},</p><p>Your password has been reset. Please login to the portal by clicking&nbsp;<a href='https://fogocertportal.bigholler.com'>here</a>. with your new password.</p><p>Thank you.</p><p>&nbsp;</p>";
                await _emailService.SendAsync(portalUser.user_name, "Reset Password Successful to the Fogo Certification Portal", body, true);

                portalUserViewModel.Result = portalUser;

                return Ok(portalUserViewModel);

            }
            catch (Exception ex)
            {
                ApiResponseBase apiResponseBase = new ApiResponseBase();
                apiResponseBase.Success = false;
                apiResponseBase.Message = ex.Message;
                apiResponseBase.UnAuthorizedRequest = false;
                return BadRequest(apiResponseBase);
            }
        }

        [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PortalUserViewModel))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> PortalDeletePortalUserById(int id)
        {
            PortalUserViewModel portalUserViewModel = new PortalUserViewModel();
            portalUserViewModel.Result = null;
            portalUserViewModel.Error = null;
            if (!Authorized())
            {
                portalUserViewModel.UnAuthorizedRequest = true;
                portalUserViewModel.Success = false;
                portalUserViewModel.Message = $"You need administrator rights to access the DeletePortalUser EndPoint";
                return Unauthorized(portalUserViewModel);
            }
            portalUserViewModel.UnAuthorizedRequest = false;


            var portalUserModel = await _portalUsersRepository.PortalUserDeleteById(id);
            if (portalUserModel.Count == 0)
            {
                portalUserViewModel.Success = false;
                portalUserViewModel.Message = "Portal User Not Deleted!";
                return NotFound(portalUserViewModel);
            }

            portalUserViewModel.Success = true;
            portalUserViewModel.Message = "Deleted";
            return Ok(portalUserViewModel);
        }
    }
}

