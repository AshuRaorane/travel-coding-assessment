using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace IntuitiveTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<TravelContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            async Task<IResult> GetAllCountries(TravelContext context)
            {
                var countries = await context.Countries
                    .Select(c => new { c.GeographyLevel1ID, c.Name })
                    .ToListAsync();

                return Results.Ok(countries);
            }

            async Task<IResult> AddRoute(TravelContext context, RouteInputModel routeInputModel)
            {
                var isRouteExist = await context.Routes.AnyAsync(r => r.DepartureAirportID == routeInputModel.DepartureAirportID && r.ArrivalAirportID == routeInputModel.ArrivalAirportID);
                if (isRouteExist)
                {
                    return Results.BadRequest("Route already exists for specified airports.");
                }
                else
                {
                    var isDepartureAirportExist = await context.Airports.AnyAsync(a => a.AirportID == routeInputModel.DepartureAirportID);
                    var isArrivalAirportExist = await context.Airports.AnyAsync(a => a.AirportID == routeInputModel.ArrivalAirportID);

                    if (!isDepartureAirportExist)
                        return Results.BadRequest("Departure airport could not be found");
                    else if (!isArrivalAirportExist)
                        return Results.BadRequest("Arrival airport could not be found");
                    else
                    {
                        var isValidDepartureAirportChoice = await context.Airports.AnyAsync(a => a.AirportID == routeInputModel.DepartureAirportID && a.Type.Contains("Departure") && routeInputModel.DepartureAirportID!= routeInputModel.ArrivalAirportID);
                        var isValidArrivalAirportChoice = await context.Airports.AnyAsync(a => a.AirportID == routeInputModel.ArrivalAirportID && a.Type.Contains("Arrival") && routeInputModel.DepartureAirportID != routeInputModel.ArrivalAirportID);

                        if (isValidDepartureAirportChoice && isValidArrivalAirportChoice)
                        {
                            var route = new Route { DepartureAirportID = routeInputModel.DepartureAirportID, ArrivalAirportID = routeInputModel.ArrivalAirportID };
                            context.Routes.Add(route);
                            await context.SaveChangesAsync();
                            return Results.Created($"/{route.RouteID}", new { route.RouteID, route.DepartureAirportID, route.ArrivalAirportID });
                        }                                
                        else
                            return Results.BadRequest("Route is invalid");
                    }
                }
            }

            async Task<IResult> AddRouteWithGroups(TravelContext context, RouteInputModel routeInputModel)
            {
                var isRouteWithGroupExist = await context.RouteWithGroup.AnyAsync(r => r.DepartureAirportGroupID == routeInputModel.DepartureAirportGroupID && r.ArrivalAirportGroupID == routeInputModel.ArrivalAirportGroupID);
                if (isRouteWithGroupExist)
                {
                    return Results.BadRequest("Route already exists for specified airport groups.");
                }
                else
                {
                    var isDepartureAirportGroupExist = await context.AirportGroups.AnyAsync(a => a.AirportGroupID == routeInputModel.DepartureAirportGroupID);
                    var isArrivalAirportGroupExist = await context.AirportGroups.AnyAsync(a => a.AirportGroupID == routeInputModel.ArrivalAirportGroupID);

                    if (!isDepartureAirportGroupExist)
                        return Results.BadRequest("Departure airport group could not be found");
                    else if (!isArrivalAirportGroupExist)
                        return Results.BadRequest("Arrival airport group could not be found");
                    else
                    {
                        bool isValidDepartureAirportGrpChoice = await context.AirportInAirportGroups
                          .Join(
                              context.AirportGroups,
                              ag => ag.AirportGroupID,
                              g => g.AirportGroupID,
                              (ag, g) => new { ag, g }
                           )
                          .Join(
                              context.Airports,
                              sr => sr.ag.AirportID,
                              a => a.AirportID,
                              (sr, a) => new { sr.ag, sr.g, a }
                          )
                          .AnyAsync(r => r.ag.AirportGroupID == routeInputModel.DepartureAirportGroupID && r.a.Type.Contains("Departure"));

                        bool isValidArrivalAirportGrpChoice = await context.AirportInAirportGroups
                          .Join(
                              context.AirportGroups,
                              ag => ag.AirportGroupID,
                              g => g.AirportGroupID,
                              (ag, g) => new { ag, g }
                           )
                          .Join(
                              context.Airports,
                              sr => sr.ag.AirportID,
                              a => a.AirportID,
                              (sr, a) => new { sr.ag, sr.g, a }
                          )
                          .AnyAsync(r => r.ag.AirportGroupID == routeInputModel.ArrivalAirportGroupID && r.a.Type.Contains("Arrival"));


                        if (isValidDepartureAirportGrpChoice && isValidArrivalAirportGrpChoice)
                        {
                            var routeWithGrp = new RouteWithGroup { DepartureAirportGroupID = routeInputModel.DepartureAirportGroupID, ArrivalAirportGroupID = routeInputModel.ArrivalAirportGroupID };
                            context.RouteWithGroup.Add(routeWithGrp);
                            await context.SaveChangesAsync();
                            return Results.Created($"/{routeWithGrp.RouteWithGroupID}", new { routeWithGrp.RouteWithGroupID, routeWithGrp.DepartureAirportGroupID, routeWithGrp.ArrivalAirportGroupID });
                        }
                        else
                            return Results.BadRequest("Gropus for route are invalid");
                    }
                }
            }

            //Countries Endpoints
            app.MapGet("/api/countries", async (TravelContext context) => await GetAllCountries(context));

            app.MapPost("/api/countries", async (TravelContext context, Country country) => {

                if (!string.IsNullOrWhiteSpace(country.Name))
                {
                    var isCountryExist = await context.Countries.AnyAsync(c => c.Name.ToLower() == country.Name.ToLower());

                    if (!isCountryExist)
                    {
                        context.Countries.Add(country);
                        await context.SaveChangesAsync();
                        return Results.Created($"/{country.GeographyLevel1ID}", new {country.GeographyLevel1ID, country.Name});
                    }
                    else
                    {
                        return Results.BadRequest("Country already exists");
                    }
                }
                else
                {
                    return Results.BadRequest("Invalid country name.");
                }
                
            });

            app.MapDelete("api/countries", async(TravelContext context, [FromQuery]int ID) => { 
                var countryToDelete= await context.Countries.FindAsync(ID);

                if (countryToDelete != null)                    
                {
                    bool isInUse = await context.Airports.AnyAsync(a => a.GeographyLevel1ID == ID);
                    if (!isInUse)
                    {
                        context.Countries.Remove(countryToDelete);
                        await context.SaveChangesAsync();
                        return await GetAllCountries(context);
                    }
                    else
                    {
                        return Results.BadRequest("A country is in use for an airport and cannot be deleted.");
                    }
                }                
                else
                {
                    return Results.NotFound("Country could not be found.");
                }
            });


            //Airports Endpoints
            app.MapGet("api/airports", async (TravelContext context) => await context.Airports.Select(a=>new {a.AirportID,a.IATACode }).ToListAsync());

            app.MapGet("api/airports/{ID:int}", async (TravelContext context, [FromRoute]int ID) =>
            {
                var airport = await context.Airports
                                .Where(a => a.AirportID == ID)
                                .Select(a => new 
                                { 
                                    a.AirportID,
                                    a.IATACode,
                                    a.GeographyLevel1ID,
                                    CountryName=a.Country.Name,
                                    a.Type
                                })
                                .FirstOrDefaultAsync();

                if (airport == null)
                {
                    return Results.NotFound("Airport could not be found");
                }
                else
                    return Results.Ok(airport);
         
             });


            //Routes Endpoints
            app.MapGet("api/routes", async(TravelContext context) => {
                var routes = await context.Routes.Select(r => new RouteInputModel { Id = r.RouteID, DepartureAirportID = r.DepartureAirportID, ArrivalAirportID = r.ArrivalAirportID }).ToListAsync();

                var routeWithGroups = await context.RouteWithGroup.Select(r => new RouteInputModel { Id= r.RouteWithGroupID, DepartureAirportGroupID= r.DepartureAirportGroupID, ArrivalAirportGroupID=r.ArrivalAirportGroupID }).ToListAsync();

                routes.AddRange(routeWithGroups);

                return Results.Ok(routes);
            });

            app.MapPost("api/routes", async (TravelContext context, RouteInputModel routeInputModel) => {
                //route with airport id
                if(routeInputModel.DepartureAirportID!=0 && routeInputModel.ArrivalAirportID != 0)
                {
                   return await AddRoute(context, routeInputModel);
                }
                //route with airport grp id
                else if (routeInputModel.DepartureAirportGroupID != 0  && routeInputModel.ArrivalAirportGroupID != 0)
                {
                    return await AddRouteWithGroups(context, routeInputModel);
                }
                else
                    return Results.BadRequest("Route is invalid.");
            });


            app.Run();
        }
    }
}