using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-yeah")]
    public class PaginationTagHelper : TagHelper
    {
        //Tag helpers for pagination
        private IUrlHelperFactory uhf;
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageYeah { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        //To make buttons different that are selected
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
       
        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");
            
            //If there is only one page
            if (PageYeah.TotalPages == 1)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.AddCssClass(PageClass);
                tb.AddCssClass(PageClassSelected);
                tb.InnerHtml.Append("1");
                final.InnerHtml.AppendHtml(tb);
            }

            //If there are two pages
            else if (PageYeah.TotalPages == 2)
            {
                for (int i = 1; i < 3; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new
                    {
                        //These are all used for filtering
                        pageNum = i,
                        severity = PageYeah.SeverityFilter,
                        theRoute = PageYeah.RouteFilter,
                        county = PageYeah.CountyFilter,
                        city = PageYeah.CityFilter,
                        month = PageYeah.MonthFilter,
                        year = PageYeah.YearFilter,
                        workzone = PageYeah.WorkZoneFilter,
                        milepoint = PageYeah.MilePointFilter,
                        road = PageYeah.RoadFilter,
                        latitude = PageYeah.LatitudeFilter,
                        longitude = PageYeah.LongitudeFilter,
                        pedestrian = PageYeah.PedestrianFilter,
                        bicyclist = PageYeah.BicyclistFilter,
                        impropperrestraint = PageYeah.ImpropperRestraintFilter,
                        unrestrained = PageYeah.UnrestrainedFilter,
                        dui = PageYeah.DUIFilter,
                        intersection = PageYeah.IntersectionFilter,
                        wildanimal = PageYeah.WildAnimalFilter,
                        domesticanimal = PageYeah.DomesticAnimalFilter,
                        rollover = PageYeah.RolloverFilter,
                        commercial = PageYeah.CommercialFilter,
                        teenager = PageYeah.TeenagerFilter,
                        older = PageYeah.OlderFilter,
                        night = PageYeah.NightFilter,
                        single = PageYeah.SingleFilter,
                        drowsy = PageYeah.DrowsyFilter,
                        departure = PageYeah.DepartureFilter

                    });

                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageYeah.CurrentPage ? PageClassSelected : PageClassNormal);

                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }

            //If it's the first page
            else if ((PageYeah.CurrentPage == 1) && (PageYeah.TotalPages > 2))
            {
                for (int i = 1; i < 4; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new
                    {
                        //These are all used for filtering
                        pageNum = i,
                        severity = PageYeah.SeverityFilter,
                        theRoute = PageYeah.RouteFilter,
                        county = PageYeah.CountyFilter,
                        city = PageYeah.CityFilter,
                        month = PageYeah.MonthFilter,
                        year = PageYeah.YearFilter,
                        workzone = PageYeah.WorkZoneFilter,
                        milepoint = PageYeah.MilePointFilter,
                        road = PageYeah.RoadFilter,
                        latitude = PageYeah.LatitudeFilter,
                        longitude = PageYeah.LongitudeFilter,
                        pedestrian = PageYeah.PedestrianFilter,
                        bicyclist = PageYeah.BicyclistFilter,
                        impropperrestraint = PageYeah.ImpropperRestraintFilter,
                        unrestrained = PageYeah.UnrestrainedFilter,
                        dui = PageYeah.DUIFilter,
                        intersection = PageYeah.IntersectionFilter,
                        wildanimal = PageYeah.WildAnimalFilter,
                        domesticanimal = PageYeah.DomesticAnimalFilter,
                        rollover = PageYeah.RolloverFilter,
                        commercial = PageYeah.CommercialFilter,
                        teenager = PageYeah.TeenagerFilter,
                        older = PageYeah.OlderFilter,
                        night = PageYeah.NightFilter,
                        single = PageYeah.SingleFilter,
                        drowsy = PageYeah.DrowsyFilter,
                        departure = PageYeah.DepartureFilter

                    });

                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageYeah.CurrentPage ? PageClassSelected : PageClassNormal);

                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);

                }

                //3 dots
                TagBuilder tbdot = new TagBuilder("a");
                tbdot.AddCssClass("btn");
                tbdot.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(tbdot);

                //Button with final page
                TagBuilder tb1 = new TagBuilder("a");
                tb1.Attributes["href"] = uh.Action(PageAction, new
                {
                    pageNum = PageYeah.TotalPages,
                    severity = PageYeah.SeverityFilter,
                    county = PageYeah.CountyFilter,
                    theRoute = PageYeah.RouteFilter,
                    city = PageYeah.CityFilter,
                    month = PageYeah.MonthFilter,
                    year = PageYeah.YearFilter,
                    workzone = PageYeah.WorkZoneFilter,
                    milepoint = PageYeah.MilePointFilter,
                    road = PageYeah.RoadFilter,
                    latitude = PageYeah.LatitudeFilter,
                    longitude = PageYeah.LongitudeFilter,
                    pedestrian = PageYeah.PedestrianFilter,
                    bicyclist = PageYeah.BicyclistFilter,
                    impropperrestraint = PageYeah.ImpropperRestraintFilter,
                    unrestrained = PageYeah.UnrestrainedFilter,
                    dui = PageYeah.DUIFilter,
                    intersection = PageYeah.IntersectionFilter,
                    wildanimal = PageYeah.WildAnimalFilter,
                    domesticanimal = PageYeah.DomesticAnimalFilter,
                    rollover = PageYeah.RolloverFilter,
                    commercial = PageYeah.CommercialFilter,
                    teenager = PageYeah.TeenagerFilter,
                    older = PageYeah.OlderFilter,
                    night = PageYeah.NightFilter,
                    single = PageYeah.SingleFilter,
                    drowsy = PageYeah.DrowsyFilter,
                    departure = PageYeah.DepartureFilter
                });

                tb1.AddCssClass(PageClass);
                tb1.AddCssClass(PageClassNormal);


                tb1.InnerHtml.Append(PageYeah.TotalPages.ToString());

                final.InnerHtml.AppendHtml(tb1);
            }

            //If it's the last page
            else if ((PageYeah.CurrentPage != 1) && (PageYeah.CurrentPage == PageYeah.TotalPages))
            {
                //Button with link to first page
                TagBuilder tb1 = new TagBuilder("a");
                tb1.Attributes["href"] = uh.Action(PageAction, new
                {
                    //These are all used for filtering 
                    pageNum = 1,
                    severity = PageYeah.SeverityFilter,
                    county = PageYeah.CountyFilter,
                    theRoute = PageYeah.RouteFilter,
                    city = PageYeah.CityFilter,
                    month = PageYeah.MonthFilter,
                    year = PageYeah.YearFilter,
                    workzone = PageYeah.WorkZoneFilter,
                    milepoint = PageYeah.MilePointFilter,
                    road = PageYeah.RoadFilter,
                    latitude = PageYeah.LatitudeFilter,
                    longitude = PageYeah.LongitudeFilter,
                    pedestrian = PageYeah.PedestrianFilter,
                    bicyclist = PageYeah.BicyclistFilter,
                    impropperrestraint = PageYeah.ImpropperRestraintFilter,
                    unrestrained = PageYeah.UnrestrainedFilter,
                    dui = PageYeah.DUIFilter,
                    intersection = PageYeah.IntersectionFilter,
                    wildanimal = PageYeah.WildAnimalFilter,
                    domesticanimal = PageYeah.DomesticAnimalFilter,
                    rollover = PageYeah.RolloverFilter,
                    commercial = PageYeah.CommercialFilter,
                    teenager = PageYeah.TeenagerFilter,
                    older = PageYeah.OlderFilter,
                    night = PageYeah.NightFilter,
                    single = PageYeah.SingleFilter,
                    drowsy = PageYeah.DrowsyFilter,
                    departure = PageYeah.DepartureFilter
                });

                tb1.AddCssClass(PageClass);
                tb1.AddCssClass(PageClassNormal);

                tb1.InnerHtml.Append("First");

                final.InnerHtml.AppendHtml(tb1);


                //3 dots
                TagBuilder tbdot = new TagBuilder("a");
                tbdot.AddCssClass("btn");
                tbdot.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(tbdot);

                for (int i = (PageYeah.TotalPages - 2); i <= PageYeah.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new
                    {
                        //These are all used for filtering 
                        pageNum = i,
                        severity = PageYeah.SeverityFilter,
                        county = PageYeah.CountyFilter,
                        theRoute = PageYeah.RouteFilter,
                        city = PageYeah.CityFilter,
                        month = PageYeah.MonthFilter,
                        year = PageYeah.YearFilter,
                        workzone = PageYeah.WorkZoneFilter,
                        milepoint = PageYeah.MilePointFilter,
                        road = PageYeah.RoadFilter,
                        latitude = PageYeah.LatitudeFilter,
                        longitude = PageYeah.LongitudeFilter,
                        pedestrian = PageYeah.PedestrianFilter,
                        bicyclist = PageYeah.BicyclistFilter,
                        impropperrestraint = PageYeah.ImpropperRestraintFilter,
                        unrestrained = PageYeah.UnrestrainedFilter,
                        dui = PageYeah.DUIFilter,
                        intersection = PageYeah.IntersectionFilter,
                        wildanimal = PageYeah.WildAnimalFilter,
                        domesticanimal = PageYeah.DomesticAnimalFilter,
                        rollover = PageYeah.RolloverFilter,
                        commercial = PageYeah.CommercialFilter,
                        teenager = PageYeah.TeenagerFilter,
                        older = PageYeah.OlderFilter,
                        night = PageYeah.NightFilter,
                        single = PageYeah.SingleFilter,
                        drowsy = PageYeah.DrowsyFilter,
                        departure = PageYeah.DepartureFilter
                    });

                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageYeah.CurrentPage ? PageClassSelected : PageClassNormal);

                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);

                }

            }

            //
            else
            {
                TagBuilder tb1 = new TagBuilder("a");
                tb1.Attributes["href"] = uh.Action(PageAction, new
                {
                    pageNum = 1,
                    severity = PageYeah.SeverityFilter,
                    county = PageYeah.CountyFilter,
                    theRoute = PageYeah.RouteFilter,
                    city = PageYeah.CityFilter,
                    month = PageYeah.MonthFilter,
                    year = PageYeah.YearFilter,
                    workzone = PageYeah.WorkZoneFilter,
                    milepoint = PageYeah.MilePointFilter,
                    road = PageYeah.RoadFilter,
                    latitude = PageYeah.LatitudeFilter,
                    longitude = PageYeah.LongitudeFilter,
                    pedestrian = PageYeah.PedestrianFilter,
                    bicyclist = PageYeah.BicyclistFilter,
                    impropperrestraint = PageYeah.ImpropperRestraintFilter,
                    unrestrained = PageYeah.UnrestrainedFilter,
                    dui = PageYeah.DUIFilter,
                    intersection = PageYeah.IntersectionFilter,
                    wildanimal = PageYeah.WildAnimalFilter,
                    domesticanimal = PageYeah.DomesticAnimalFilter,
                    rollover = PageYeah.RolloverFilter,
                    commercial = PageYeah.CommercialFilter,
                    teenager = PageYeah.TeenagerFilter,
                    older = PageYeah.OlderFilter,
                    night = PageYeah.NightFilter,
                    single = PageYeah.SingleFilter,
                    drowsy = PageYeah.DrowsyFilter,
                    departure = PageYeah.DepartureFilter
                });

                tb1.AddCssClass(PageClass);
                tb1.AddCssClass(PageClassNormal);


                tb1.InnerHtml.Append("First");

                final.InnerHtml.AppendHtml(tb1);

                //3 dots
                TagBuilder tbdot = new TagBuilder("a");
                tbdot.AddCssClass("btn");
                tbdot.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(tbdot);

                for (int i = (PageYeah.CurrentPage - 1); i < (PageYeah.CurrentPage + 2); i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new
                    {
                        pageNum = i,
                        severity = PageYeah.SeverityFilter,
                        county = PageYeah.CountyFilter,
                        theRoute = PageYeah.RouteFilter,
                        city = PageYeah.CityFilter,
                        month = PageYeah.MonthFilter,
                        year = PageYeah.YearFilter,
                        workzone = PageYeah.WorkZoneFilter,
                        milepoint = PageYeah.MilePointFilter,
                        road = PageYeah.RoadFilter,
                        latitude = PageYeah.LatitudeFilter,
                        longitude = PageYeah.LongitudeFilter,
                        pedestrian = PageYeah.PedestrianFilter,
                        bicyclist = PageYeah.BicyclistFilter,
                        impropperrestraint = PageYeah.ImpropperRestraintFilter,
                        unrestrained = PageYeah.UnrestrainedFilter,
                        dui = PageYeah.DUIFilter,
                        intersection = PageYeah.IntersectionFilter,
                        wildanimal = PageYeah.WildAnimalFilter,
                        domesticanimal = PageYeah.DomesticAnimalFilter,
                        rollover = PageYeah.RolloverFilter,
                        commercial = PageYeah.CommercialFilter,
                        teenager = PageYeah.TeenagerFilter,
                        older = PageYeah.OlderFilter,
                        night = PageYeah.NightFilter,
                        single = PageYeah.SingleFilter,
                        drowsy = PageYeah.DrowsyFilter,
                        departure = PageYeah.DepartureFilter
                    });

                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageYeah.CurrentPage ? PageClassSelected : PageClassNormal);
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);

                }

                //3dots
                TagBuilder tbdot1 = new TagBuilder("a");
                tbdot1.AddCssClass("btn");
                tbdot1.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(tbdot1);

                TagBuilder tb2 = new TagBuilder("a");
                tb2.Attributes["href"] = uh.Action(PageAction, new
                {
                    pageNum = PageYeah.TotalPages,
                    severity = PageYeah.SeverityFilter,
                    county = PageYeah.CountyFilter,
                    theRoute = PageYeah.RouteFilter,
                    city = PageYeah.CityFilter,
                    month = PageYeah.MonthFilter,
                    year = PageYeah.YearFilter,
                    workzone = PageYeah.WorkZoneFilter,
                    milepoint = PageYeah.MilePointFilter,
                    road = PageYeah.RoadFilter,
                    latitude = PageYeah.LatitudeFilter,
                    longitude = PageYeah.LongitudeFilter,
                    pedestrian = PageYeah.PedestrianFilter,
                    bicyclist = PageYeah.BicyclistFilter,
                    impropperrestraint = PageYeah.ImpropperRestraintFilter,
                    unrestrained = PageYeah.UnrestrainedFilter,
                    dui = PageYeah.DUIFilter,
                    intersection = PageYeah.IntersectionFilter,
                    wildanimal = PageYeah.WildAnimalFilter,
                    domesticanimal = PageYeah.DomesticAnimalFilter,
                    rollover = PageYeah.RolloverFilter,
                    commercial = PageYeah.CommercialFilter,
                    teenager = PageYeah.TeenagerFilter,
                    older = PageYeah.OlderFilter,
                    night = PageYeah.NightFilter,
                    single = PageYeah.SingleFilter,
                    drowsy = PageYeah.DrowsyFilter,
                    departure = PageYeah.DepartureFilter
                });

                tb2.AddCssClass(PageClass);
                tb2.AddCssClass(PageClassNormal);


                tb2.InnerHtml.Append("Last");

                final.InnerHtml.AppendHtml(tb2);

            }
            

            tho.Content.AppendHtml(final.InnerHtml);


        }
    }
}
