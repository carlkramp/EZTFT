﻿using System.Web;
using System.Web.Optimization;

namespace EZTFT
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                         "~/Scripts/datatables/datatables.bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        

            bundles.Add(new Bundle("~/Content/css").Include(
                        "~/Content/bootstrap-pulse.css",
                        "~/Content/bootstrap-theme.css",
                      "~/content/datatables/css/datatables.bootstrap.css",
                      "~/Content/Site.css"));

        }
    }
}