using System;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Http;

namespace Helpers
{
    public static class Extensions
    {
        public static void AddPaginationHeaders(this HttpResponse response, PaginationHeaders paginationHeaders)
        {
            response.Headers.Add("Page", paginationHeaders.Page.ToString());
            response.Headers.Add("PerPage", paginationHeaders.PerPage.ToString());
            response.Headers.Add("TotalItems", paginationHeaders.TotalItems.ToString());
            response.Headers.Add("TotalPages", paginationHeaders.TotalPages.ToString());

        }
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}