using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using RealRMS.CacheKeyConstants;
using RealRMS.Models;
using RealRMS.Utility;

namespace RealRMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        const string CONNECTION_STRING = "ProductionConnection";
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        protected void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();
            string requestedUrl = HttpContext.Current.Request.RawUrl;
            Uri referrerUri = HttpContext.Current.Request.UrlReferrer;
            string referrer = referrerUri == null ? "Unknown" : referrerUri.AbsoluteUri;

            string reqStr = null;
            try {
                reqStr = string.Format(@"{{
                                            ""Request"" : {{ 
                                                            ""Request.Form"" : {0}, 
                                                            ""Request.Querystring"" : {1}, 
                                                            ""Request.ServerVariables"" : {2} 
                                                            }} 
                                            }}",
                    JsonConvert.SerializeObject(HttpContext.Current.Request.Form, Formatting.Indented),
                    JsonConvert.SerializeObject(HttpContext.Current.Request.QueryString),
                    JsonConvert.SerializeObject(HttpContext.Current.Request.ServerVariables));
            } catch (Exception ex1) {
                
            }

            SqlWorker worker = new SqlWorker(DatabaseConnectionFactory.Instance.GetConnection(CONNECTION_STRING));
            worker.SetupCommand("error_insert", System.Data.CommandType.StoredProcedure);
            worker.AddInputParameter("@msg", exc.Message);
            worker.AddInputParameter("@requestJson", reqStr);
            worker.AddInputParameter("@url", requestedUrl);
            worker.AddInputParameter("@urlReferrer", referrer);
            worker.AddInputParameter("@stackTrace", exc.StackTrace);
            var employeeId = GetEmployeeId();
            if (employeeId != null)
                worker.AddInputParameter("@EmployeeId", employeeId);
            worker.ExecuteNonQuery();
    
            Response.Redirect("~/Home/Error");
            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException)) {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<h2>Global Page Error</h2>\n");
            Response.Write(
                "<p>" + exc.Message + "</p>\n");
            Response.Write("Return to the <a href='Default.aspx'>" +
                "Default Page</a>\n");

            // Clear the error from the server
            Server.ClearError();
        }

        protected void Application_BeginRequest() {
            var stopwatch = new Stopwatch();
            HttpContext.Current.Items["Stopwatch"] = stopwatch;
            HttpContext.Current.Items["NumberOfQueries"] = 0;
            HttpContext.Current.Items["QueryTime"] = 0.0;
            stopwatch.Start();
        }

        protected void Application_EndRequest() {
            var stopwatch = (Stopwatch)HttpContext.Current.Items["Stopwatch"];
            stopwatch.Stop();
            double responseTime = stopwatch.Elapsed.TotalSeconds;
            string requestedUrl = HttpContext.Current.Request.RawUrl;
            Uri referrerUri = HttpContext.Current.Request.UrlReferrer;
            string referrer = referrerUri == null ? "Unknown" : referrerUri.AbsoluteUri;
            SqlWorker worker = new SqlWorker(DatabaseConnectionFactory.Instance.GetConnection(CONNECTION_STRING));
            worker.SetupCommand("requests_insert", System.Data.CommandType.StoredProcedure);
            worker.AddInputParameter("@url", requestedUrl);
            worker.AddInputParameter("@urlReferrer", referrer);
            worker.AddInputParameter("@timeElapsed", responseTime);
            var employeeId = GetEmployeeId();
            if (employeeId != null)
                worker.AddInputParameter("@EmployeeId", employeeId);
            worker.ExecuteNonQuery();

        }

        private int? GetEmployeeId() {
            if (HttpContext.Current == null)
                return null;
            if (HttpContext.Current.Session == null)
                return null;
            var userInfo =  HttpContext.Current.Session[SessionCacheConstants.ME] as UserInfoModel;
            if (userInfo == null)
                return null;
            return userInfo.Id;
        }
    }
}