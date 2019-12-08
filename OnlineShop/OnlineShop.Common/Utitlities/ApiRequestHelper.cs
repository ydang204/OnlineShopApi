using Flurl.Http;
using Flurl;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineShop.Common.Models.Common.ResModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Common.Exceptions;

namespace OnlineShop.Common.Utitlities
{
    public class ApiRequestHelper
    {
        private readonly ILogger _logger;

        public ApiRequestHelper(ILogger<ApiRequestHelper> logger)
        {
            _logger = logger;
        }

        #region GET
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = await ProcessGetAsync<T>(url, headers, requestParams, accessToken, timeout, isThrowException);
            return result;
        }

        public async Task GetAsync(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            await ProcessGetAsync<object>(url, headers, requestParams, accessToken, timeout, isThrowException);
        }

        private async Task<T> ProcessGetAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = new RestApiResponse<T>();
            try
            {
                var _request = new Url(url);
                var _flurlRequest = new FlurlRequest(_request);

                if (!string.IsNullOrEmpty(accessToken))
                    _flurlRequest = _flurlRequest.WithHeader("Authorization", accessToken);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        _flurlRequest = _flurlRequest.WithHeader(item.Key, item.Value);
                    }
                }

                var response = requestParams != null ? await _flurlRequest.SetQueryParams(requestParams).WithTimeout(timeout).GetAsync() : await _flurlRequest.WithTimeout(timeout).GetAsync();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }

            }
            catch (FlurlHttpTimeoutException timeoutex)
            {
                result.IsSuccess = false;
                result.ExceptionMessage = "Time out";
                result.Exception = timeoutex;
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();

                result.Exception = ex;
                result.ExceptionMessage = ex.Message;
                result.IsSuccess = false;
                //Try parse response to Custom Exception
                try
                {
                    var customException = JsonConvert.DeserializeObject<CustomExceptionResponse>(errorResponse);
                    if (customException != null)
                    {
                        result.IsCustomException = true;
                        result.CustomException = customException;
                        result.IsSuccess = false;
                    }
                }
                catch
                {
                    // KhuongDang : No content
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, isThrowException);

            return result.Result;
        }
        #endregion

        #region POST
        public async Task<T> PostAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = await ProcessPostAsync<T>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException);
            return result;
        }

        public async Task PostAsync(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            await ProcessPostAsync<object>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException);
        }

        public async Task<T> ProcessPostAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = new RestApiResponse<T>();

            try
            {
                var _request = new Url(url);
                var _flurlRequest = _request.WithHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(accessToken))
                    _flurlRequest = _request.WithHeader("Authorization", accessToken);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        _flurlRequest = _flurlRequest.WithHeader(item.Key, item.Value);
                    }

                }
                if (requestParams != null)
                    _flurlRequest = _flurlRequest.SetQueryParams(requestParams);

                var response = await _flurlRequest.WithTimeout(timeout).PostJsonAsync(requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();

                result.Exception = ex;
                result.ExceptionMessage = ex.Message;
                result.IsSuccess = false;
                //Try parse response to Custom Exception
                try
                {
                    var customException = JsonConvert.DeserializeObject<CustomExceptionResponse>(errorResponse);
                    if (customException != null)
                    {
                        result.IsCustomException = true;
                        result.CustomException = customException;
                        result.IsSuccess = false;
                    }
                }
                catch
                {
                    // KhuongDang : No content
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, requestBody, isThrowException);

            return result.Result;
        }
        #endregion

        #region PUT
        public async Task<T> PutAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = await ProcessPutAsync<T>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException);
            return result;
        }

        public async Task PutAsync(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            await ProcessPutAsync<object>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException);
        }

        public async Task<T> ProcessPutAsync<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, object requestBody = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = new RestApiResponse<T>();

            try
            {
                var _request = new Url(url);
                var _flurlRequest = _request.WithHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(accessToken))
                    _flurlRequest = _request.WithHeader("Authorization", accessToken);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        _flurlRequest = _flurlRequest.WithHeader(item.Key, item.Value);
                    }

                }
                if (requestParams != null)
                    _flurlRequest = _flurlRequest.SetQueryParams(requestParams);

                var response = await _flurlRequest.WithTimeout(timeout).PutJsonAsync(requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();

                result.Exception = ex;
                result.ExceptionMessage = ex.Message;
                result.IsSuccess = false;
                //Try parse response to Custom Exception
                try
                {
                    var customException = JsonConvert.DeserializeObject<CustomExceptionResponse>(errorResponse);
                    if (customException != null)
                    {
                        result.IsCustomException = true;
                        result.CustomException = customException;
                        result.IsSuccess = false;
                    }
                }
                catch
                {
                    // KhuongDang : No content
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, requestBody, isThrowException);

            return result.Result;
        }
        #endregion

        #region DELETE
        public async Task<T> DeleteAsyc<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = await ProcessDeleteAsyc<T>(url, headers, requestParams, accessToken, timeout, isThrowException);
            return result;
        }

        public async Task DeleteAsyc(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            await ProcessDeleteAsyc<object>(url, headers, requestParams, accessToken, timeout, isThrowException);
        }

        public async Task<T> ProcessDeleteAsyc<T>(string url, Dictionary<string, string> headers = null, object requestParams = null, string accessToken = "", int timeout = 10, bool isThrowException = true)
        {
            var result = new RestApiResponse<T>();

            try
            {
                var _request = new Url(url);
                var _flurlRequest = _request.WithHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(accessToken))
                    _flurlRequest = _request.WithHeader("Authorization", accessToken);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        _flurlRequest = _flurlRequest.WithHeader(item.Key, item.Value);
                    }
                }
                if (requestParams != null)
                    _flurlRequest = _flurlRequest.SetQueryParams(requestParams);

                var response = await _flurlRequest.WithTimeout(timeout).DeleteAsync();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();

                result.Exception = ex;
                result.ExceptionMessage = ex.Message;
                result.IsSuccess = false;
                //Try parse response to Custom Exception
                try
                {
                    var customException = JsonConvert.DeserializeObject<CustomExceptionResponse>(errorResponse);
                    if (customException != null)
                    {
                        result.IsCustomException = true;
                        result.CustomException = customException;
                        result.IsSuccess = false;
                    }
                }
                catch
                {
                    // KhuongDang : No content
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, isThrowException);

            return result.Result;
        }
        #endregion

        #region Log
        private void WriteLog<T>(string url, RestApiResponse<T> result, object requestParams = null, object requestBody = null, bool isThrowException = true)
        {
            if (result.IsSuccess) return;

            var message = result.IsCustomException ? result.CustomException.Message
                                                   : !string.IsNullOrEmpty(result.ExceptionMessage) ? result.ExceptionMessage : result.Exception.Message;
            var stackTrace = result.IsCustomException ? string.Empty : result.Exception.StackTrace;
            var param = requestParams != null ? JsonConvert.SerializeObject(requestParams) : "";
            var body = requestBody != null ? JsonConvert.SerializeObject(requestBody) : "";

            _logger.LogError($"[{DateTime.Now}] [ERR] Request API: {url}\r\nRequest params: {param}\r\nRequest body: {body}\r\nError message: {message}\r\n{stackTrace}");

            var code = result.IsCustomException ? result.CustomException.Code : "UN_HANDLING_ERROR";

            if (isThrowException) throw new CustomException(code, message);
        }
        #endregion

    }
}