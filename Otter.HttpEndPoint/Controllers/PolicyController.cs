﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otter.Business.Definitions.Services;
using Otter.Business.Dtos;
using Otter.Common.Exceptions;
using Otter.Common.Tools;

namespace Otter.HttpEndPoint.Controllers
{
    [Route("api/policies")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private IPolicyService _policyService;
        private ILogger<PolicyController> _logger;

        public PolicyController(IPolicyService policyService, ILogger<PolicyController> logger)
        {
            _policyService = policyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{guid}")]
        public ActionResult<PolicyDto> PostBasicInformation(Guid guid)
        {
            try
            {
                var result = _policyService.Get(guid);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpPost]
        [Route("basic-information")]
        public ActionResult<Guid> PostBasicInformation(BasicInformationRequestDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _policyService.InsertBasicInformation(dto);
                    return Ok(result);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpPut]
        [Route("{guid}/mobile-confirmation/{otp}")]
        public ActionResult<PolicyDto> MobileConfirmation(Guid guid, string otp)
        {
            try
            {
                var result = _policyService.MobileConfirmByOtp(guid, otp);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BusinessViolatedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpPut]
        [Route("{guid}/imei/{imei}")]
        public ActionResult<PolicyDto> Imei(Guid guid, string imei)
        {
            try
            {
                var result = _policyService.AddImeiFile(guid, imei);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpPut]
        [Route("{guid}/files/imei/{base64Imei}")]
        public ActionResult<PolicyDto> AddImeiFile(Guid guid, FilePolicyInsertDto dto)
        {
            try
            {
                var result = _policyService.AddImeiFile(guid, dto.Base64Image);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpGet]
        [Route("{guid}/files/imei")]
        public ActionResult<PolicyDto> GetImeiFile(Guid guid)
        {
            try
            {
                var result = _policyService.GetImeiFile(guid);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpPut]
        [Route("{guid}/files/box-image/{base64Imei}")]
        public ActionResult<PolicyDto> AddBoxImageFile(Guid guid, FilePolicyInsertDto dto)
        {
            try
            {
                var result = _policyService.AddBoxImageFile(guid, dto.Base64Image);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }

        [HttpGet]
        [Route("{guid}/files/box-image")]
        public ActionResult<PolicyDto> GetBoxImageFile(Guid guid)
        {
            try
            {
                var result = _policyService.GetBoxImageFile(guid);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("خطای غیر منتظره رخ داده است");
            }
        }
    }
}