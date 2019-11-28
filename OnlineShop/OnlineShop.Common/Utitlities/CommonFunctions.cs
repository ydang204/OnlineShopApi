using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Models.Common.ReqModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OnlineShop.Common.Utitlities
{
    public static class CommonFunctions
    {
        public static string GetCurrentCommit(IHostingEnvironment env)
        {
            var currentCommitPath = Path.Combine(env.ContentRootPath, "..", "Common", "CurrentCommit.txt");
            return File.ReadAllText(currentCommitPath).Trim();
        }

        public static void Filter3rdPartyControllers(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c, string currentAssemblyName) // for swagger
        {
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                // Filter out 3rd party controllers
                var assemblyName = ((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerTypeInfo.Assembly.GetName().Name;
                return currentAssemblyName == assemblyName;
            });
        }

        //public static void AddSharedSetting(WebHostBuilderContext hostingContext, IConfigurationBuilder config)
        //{
        //    var env = hostingContext.HostingEnvironment.EnvironmentName;
        //    var sharedFolder = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, "..", "Common");
        //    config.AddJsonFile(Path.Combine(sharedFolder, $"SharedSettings.json"));
        //    config.AddJsonFile(Path.Combine(sharedFolder, $"SharedSettings.{env}.json"), optional: true);
        //}
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static bool IsDifference(List<int> listId, List<int> dbList)
        {
            listId = _newListIfNull(listId);
            dbList = _newListIfNull(dbList);
            return dbList.Any(d => !listId.Contains(d)) || listId.Any(d => !dbList.Contains(d));
        }

        public static bool IsDifference(List<string> listId, List<string> dbList)
        {
            listId = _newListIfNull(listId);
            dbList = _newListIfNull(dbList);
            return dbList.Any(d => !listId.Contains(d)) || listId.Any(d => !dbList.Contains(d));
        }

        public static string GetErrorFromModel(this ModelStateDictionary modelState)
        {
            var message = "";
            var errors = modelState.ToList().SelectMany(t => t.Value.Errors.Select(s => s.ErrorMessage + "\n")).ToList();
            message = string.Join(", ", errors);
            return message;
        }

        public static bool IsNullOrEmpty(this string str, bool isJSON = false)
        {
            return string.IsNullOrEmpty(str) || (isJSON && str == "[]");
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || !list.Any();
        }

        public static string RandomString(int length, bool isDigit = false, bool includeCaseSensitive = false)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            if (isDigit)
            {
                chars = "0123456789";
            }
            if (includeCaseSensitive)
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10 && phoneNumber.StartsWith('0')) return true;

            return false;
        }

        public static bool ValidateEmail(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                // return true if match and false if not match
                return match.Success;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string EnumGetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>().Name;
        }

        public static void ValidatePhoneNumberVietNam(string phoneNumber)
        {
            Regex regex = new Regex("^0+[0-9]{9}$");
            Match match = regex.Match(phoneNumber);
            if (!match.Success)
            {
                //throw new CustomException(Errors.PHONE_NUMBER_IS_NOT_VALID, Errors.PHONE_NUMBER_IS_NOT_VALID_MSG);
            }
        }

        public static string DecimalToMoney(decimal model)
        {
            return model.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
        }

        public static IQueryable<T> SortQuery<T>(BasePagingRequest model, IQueryable<T> query, Boolean isSearchByCreatedAtDesc = false) where T : class
        {
            if (!query.HasElement()) return query;

            if (model.SortNames.HasElement() && !model.SortNames.Any(s => s.IsNullOrEmpty()) && model.SortDirections.HasElement() && model.SortNames.Count == model.SortDirections.Count)
            {
                for (int i = 0; i < model.SortNames.Count; i++)
                {
                    string direction = model.SortDirections[i].ToString();
                    if (i != 0)
                    {
                        direction = direction.Replace("Order", "Then");
                    }
                    var exp = LinqHelper.GenerateMethodCall<T>(query, direction, model.SortNames[i]);
                    query = query.Provider.CreateQuery<T>(exp);
                }
            }
            else
            {
                // if class default has not field createdAt, it not do any thing
                var propertyInfo = query.First().GetType().GetProperty("CreatedAt", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    if (isSearchByCreatedAtDesc)
                    {
                        query = query.OrderByDescending(e => propertyInfo.GetValue(e, null));
                    }
                    else
                    {
                        query = query.OrderBy(e => propertyInfo.GetValue(e, null));
                    }
                }
            }

            return query;
        }

        public static bool ValidatePromotionCode(string code)
        {
            try
            {
                Regex regex = new Regex(@"^[a-zA-Z0-9]*$");
                Match match = regex.Match(code);
                // return true if match and false if not match
                return match.Success;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string ReadFile(this string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                return json;
            }
        }

        public static TResponse ReadJsonFile<TResponse>(this string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
        }

        //public static string GetCellValue(this ExcelWorksheet worksheet, int rowIndex, int colIndex)
        //{
        //    var obj = worksheet.Cells[rowIndex, colIndex].Value;
        //    return obj != null ? obj.ToString().Trim() : string.Empty;
        //}

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T Convert<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        #region ExportExcel

        //public static byte[] GenerateFileExcel(ExportExcelModel excel)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        package.Workbook.Worksheets.Add(excel.WorkSheetName); // dynamic work sheet name
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

        //        Font fontTitle = new Font(new FontFamily("Times New Roman"), 18, FontStyle.Bold); // new one font custom
        //        Font fontHeader = new Font(new FontFamily("Times New Roman"), 13, FontStyle.Bold); // new one font custom

        //        worksheet.Cells[1, 1].Value = excel.Title; // dynamic Title export
        //        worksheet.SelectedRange[1, 1, 2, excel.RowDatas.FirstOrDefault().Value.Count].Merge = true; // custom title : custom more row
        //        worksheet = Border(worksheet, 1, 1, 2, excel.RowDatas.FirstOrDefault().Value.Count, ExcelBorderStyle.Thin);
        //        worksheet = MiddleAlign(worksheet, 1, 1, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);
        //        worksheet.Cells[1, 1].Style.Font.SetFromFont(fontTitle); // set style for text

        //        worksheet.Cells[3, 1].Value = excel.DateFromTo; // dynamic date from to
        //        worksheet.SelectedRange[3, 1, 3, excel.RowDatas.FirstOrDefault().Value.Count].Merge = true; // custom title : custom one row
        //        worksheet = Border(worksheet, 3, 1, 3, excel.RowDatas.FirstOrDefault().Value.Count, ExcelBorderStyle.Thin);
        //        worksheet = MiddleAlign(worksheet, 3, 1, null, ExcelVerticalAlignment.Center);

        //        int row = 4; // default row = 3, can change dynamic if have features

        //        for (int i = 0; i < excel.RowDatas.Count; i++)
        //        {
        //            var data = excel.RowDatas[i];

        //            for (int j = 0; j < data.Value.Count; j++)
        //            {
        //                var column = j + 1;
        //                worksheet.Cells[row, column].Value = data.Value[j]; // generate data

        //                if (i == 0)
        //                {
        //                    worksheet.Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid; // use set background color, hasn't this can't set background color
        //                    worksheet.Cells[row, column].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // set color background header
        //                    worksheet.Cells[row, column].Style.Font.SetFromFont(fontHeader); // bold header text
        //                }

        //                worksheet = Border(worksheet, row, column, null, null, ExcelBorderStyle.Thin); // draw boder style is thin
        //            }
        //            row++;
        //        }

        //        worksheet.Cells.AutoFitColumns();

        //        return package.GetAsByteArray();

        //    }
        //}

        //private static ExcelWorksheet MiddleAlign(ExcelWorksheet worksheet, int row, int column, ExcelHorizontalAlignment? horizontalAlignment, ExcelVerticalAlignment? verticalAlignment)
        //{
        //    if (horizontalAlignment.HasValue)
        //    {
        //        worksheet.Cells[row, column].Style.HorizontalAlignment = horizontalAlignment.Value; // custom center horizontal
        //    }

        //    if (verticalAlignment.HasValue)
        //    {
        //        worksheet.Cells[row, column].Style.VerticalAlignment = verticalAlignment.Value; // custom center vertical
        //    }

        //    return worksheet;
        //}

        //private static ExcelWorksheet Border(ExcelWorksheet worksheet, int rowFrom, int columnFrom, int? rowTo, int? columnTo, ExcelBorderStyle borderStyle)
        //{
        //    if (rowTo.HasValue && columnTo.HasValue)
        //    {
        //        worksheet.SelectedRange[rowFrom, columnFrom, rowTo.Value, columnTo.Value].Style.Border.BorderAround(borderStyle);
        //    }
        //    else
        //    {
        //        worksheet.Cells[rowFrom, columnFrom].Style.Border.BorderAround(borderStyle);
        //    }

        //    return worksheet;
        //}

        #endregion ExportExcel

        #region Private

        private static List<int> _newListIfNull(List<int> list)
        {
            if (list == null)
            {
                list = new List<int>();
            }

            return list;
        }

        private static List<string> _newListIfNull(List<string> list)
        {
            if (list == null)
            {
                list = new List<string>();
            }

            return list;
        }

        #endregion Private
    }
}