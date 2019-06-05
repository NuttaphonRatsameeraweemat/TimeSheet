﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TimeSheet.Helper.Interfaces;
using System;
using System.Threading.Tasks;
using TimeSheet.Bll.Models;

namespace TimeSheet.API
{
    public class Middleware
    {

        #region [Fields]

        /// <summary>
        /// The RequesyDelegate.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Logger.
        /// </summary>
        private readonly ILoggerManager _logger;

        #endregion

        #region [Constructors]

        /// <summary>
        ///  Initializes a new instance of the <see cref="Middleware" /> class.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public Middleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Custom middleware when request begin and end.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            BeginInvoke(httpContext);
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Errors Message : ");
                await HandleExceptionAsync(httpContext);
            }
            EndInvoke(httpContext);
        }

        /// <summary>
        /// Custom request begin.
        /// </summary>
        /// <param name="httpContext"></param>
        private void BeginInvoke(HttpContext httpContext)
        {
            _logger.CreateNewSession(httpContext);
            _logger.LogInfo($"About to start {httpContext.Request.Method} {httpContext.Request.GetDisplayUrl()} request");
        }

        /// <summary>
        /// Custom request end.
        /// </summary>
        /// <param name="httpContext"></param>
        private void EndInvoke(HttpContext httpContext)
        {
            _logger.LogInfo($"Request completed with status code: {httpContext.Response.StatusCode} ");
        }

        /// <summary>
        /// Custom when request exception.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            var model = new ResultViewModel { IsError = true , Message = $"{httpContext.Response.StatusCode}" };

            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return httpContext.Response.WriteAsync(json);
        }

        #endregion

    }
}
